using System;
using System.Linq;
using System.Collections.Generic;
using PF2.Extensions;

namespace Bestiary
{
    static class Parser
    {
        public static (int,string[]) ParseSenses(string[] senses)
        {
            int modifier = 0;
            int count = 0;
            foreach (string sense_str in senses)
            {
                string[] data = sense_str.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                if(data[0] == "Perception")
                {
                    modifier = int.Parse(data[1]);
                    break;
                }
                count++;
            }
            return (modifier, senses.Take(count).Concat(senses.Skip(count + 1)).ToArray());
        }
        public static bool ParseAction(string action, out PF2.IAction output)
        {
            uint actionCount = 1;
            PF2.ActionType actionType = PF2.ActionType.Action;
            bool ready = false;
            bool readTraits = false;
            PF2.RangeType range = PF2.RangeType.Melee;
            string name = "";
            int modifier = 0;
            PF2.DamageType damageType = PF2.DamageType.Bludgeoning;
            List<PF2.DamageSource> damageSources = new List<PF2.DamageSource>();
            List<PF2.Traits> additionalTraits = null;
            string damageFunction = "";
            string[] action_data = action.Split(")", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            if (action_data.Length < 2)
            {
                action_data = action.Split(";", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                if (action_data.Length < 2)
                {
                    action_data = action.Split(",", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                }
            }

            string attack = action_data[0];
            string damage = action_data[1];
            if (damage[0] == ',') damage = damage.Substring(1).Trim();
            string[] attack_data = attack.Split("(", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            if (attack_data.Length < 2) attack_data = attack.Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            else attack_data = attack_data[0].Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Concat(attack_data.Skip(1)).ToArray();
            string[] damage_data = damage.Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            foreach (string word in damage_data)
            {
                string token = word.ToLower();
                if (token == "damage")
                {
                    ready = true;
                    continue;
                }
                if (token == "plus")
                {
                    break;
                    //Add here support for followUp skills
                }
                if (char.IsDigit(token[0]))
                {
                    //Read damage expression
                    damageFunction = token;
                    continue;
                }
                token = char.ToUpper(token[0]) + token.Substring(1);
                if (Enum.TryParse<PF2.DamageType>(token, out damageType) == true)
                {
                    continue;
                }
            }
            if (ready)
            {
                if (damageFunction.Length > 0) damageSources.Add(new PF2.DamageSource(new PF2.DieSource(damageFunction), damageType));

                ready = false;
                for (int index = 0; index < attack_data.Length; index++)
                {
                    string token = attack_data[index].ToLower();
                    if (token == "melee")
                    {
                        ready = true;
                        range = PF2.RangeType.Melee;
                        continue;
                    }
                    if (token == "ranged")
                    {
                        ready = false;
                        break;
                    }
                    if (readTraits)
                    {
                        //Attempt to parse traits
                        string[] traits = token.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                        foreach (string t in traits)
                        {
                            PF2.Traits trait;
                            string type = char.ToUpper(t[0]) + t.Substring(1);
                            if (type.TryParseTrait(out trait))
                            {
                                if (additionalTraits == null) additionalTraits = new List<PF2.Traits>();
                                additionalTraits.Add(trait);
                            }
                        }
                        continue;
                    }
                    if (char.IsDigit(token[0]))
                    {
                        modifier = int.Parse(token);
                        readTraits = true;
                    }
                    else
                    {
                        switch (token[0])
                        {
                            case '[':
                                //Read action type and cost
                                token = token.Skip(1).SkipLast(1).ToString().ToLower().Trim();
                                if(token == "one-action")
                                {
                                    actionType = PF2.ActionType.Action;
                                    actionCount = 1;
                                } else if(token == "two-actions")
                                {
                                    actionType = PF2.ActionType.Action;
                                    actionCount = 2;
                                } else if(token == "three-actions")
                                {
                                    actionType = PF2.ActionType.Action;
                                    actionCount = 3;
                                } else if(token == "reaction")
                                {
                                    actionType = PF2.ActionType.Reaction;
                                    actionCount = 1;
                                } else if (token == "free-action")
                                {
                                    actionType = PF2.ActionType.Free;
                                    actionCount = 1;
                                }
                                break;
                            case '+':
                            case '-':
                                if (int.TryParse(token, out modifier))
                                {
                                    readTraits = true;
                                }
                                break;
                            default:
                                name += " " + attack_data[index];
                                break;
                        }
                    }
                }
            }
            if (ready)
            {
                output = new PF2.StrikeAction(range, name.Trim(), modifier, damageSources, null, additionalTraits,actionType,actionCount);
            }
            else
            {
                output = null;
            }
            return ready;
        }
        public static Tuple<uint, string[]> ParseHp(string[] hp_str)
        {
            uint hp = 0;
            string[] abilities = new string[0];
            string[] hp_line = hp_str[0].Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            hp = uint.Parse(hp_line[0]);
            foreach (string val in hp_line.Skip(1).Concat(hp_str.Skip(1)))
            {
                abilities = abilities.Append(val.Trim()).ToArray();
            }
            return new Tuple<uint, string[]>(hp, abilities);
        }
        public static IEnumerable<Tuple<PF2.DamageType, uint>> ParseWeaknessesAndResistances(string[] attributes)
        {
            foreach (string str in attributes)
            {
                string clean_str = str.Trim();
                if (clean_str.Length == 0) continue; //Ignore empty entries
                string[] data = clean_str.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

                string type_str = char.ToUpper(data[0][0]) + data[0].Substring(1);

                if (char.IsDigit(type_str[0]))
                {
                    if (data[1] == "(except")
                    {
                        type_str = data[2].Replace(")", "");
                        type_str = char.ToUpper(type_str[0]) + type_str.Substring(1);
                        uint value = uint.Parse(data[0]);
                        foreach (PF2.DamageType type in Enum.GetValues<PF2.DamageType>())
                        {
                            if (type.ToString() == type_str) continue;
                            yield return new Tuple<PF2.DamageType, uint>(type, value);
                        }
                    }
                }
                PF2.DamageType damageType;
                if (Enum.TryParse<PF2.DamageType>(type_str, out damageType) == false)
                {

                }
                else
                {
                    if (char.IsDigit(data[1][0]))
                    {
                        yield return new Tuple<PF2.DamageType, uint>(damageType, uint.Parse(data[1]));
                    }
                    else
                    {

                    }
                }
            }
        }
    }
}