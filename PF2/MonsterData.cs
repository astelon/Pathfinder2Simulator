using System.Collections.Generic;

namespace PF2
{
    public class MonsterData : ICreatureData
    {
        public string name;
        public int level;
        LawChaosAlignment lcAlignment;
        GoodEvilAlignment geAlignment;
        Rarity rarity;
        CreatureSize size;
        TraitsBitmap traits;
        public int perception;
        List<int> languages;
        List<CreatureSkill> skills;
        int[] abilityModifiers = new int[(int)AbilityType.AbilityCount];
        List<string> items;
        List<string> interactionAbilities;
        public int ac = 0;
        uint[] savingThrows;
        public uint hp;
        public string[] senses;
        public List<string> hpAutoAbilities;
        public TraitsBitmap immunities;
        public Dictionary<DamageType, uint> weaknesses;
        public Dictionary<DamageType, uint> resistances;
        List<string> autoAbilities;
        List<string> reactiveAbilities;
        uint speed;
        public List<IAction> actions;
        List<ISpell> spells;
        List<ISpell> innateSpells;
        List<IFocusSpell> focusSpells;
        public int AC()
        {
            return ac;
        }

        public string GetName()
        {
            return name;
        }

        public uint WeakTo(DamageType type)
        {
            uint value = 0;
            if (weaknesses == null) return 0;
            if (weaknesses.ContainsKey(type))
            {
                value = weaknesses[type];
            }
            return value;
        }

        public uint ResistantTo(DamageType type)
        {
            uint value = 0;
            if (resistances == null) return 0;
            if (resistances.ContainsKey(type))
            {
                value = resistances[type];
            }
            return value;
        }

        public bool ImmuneTo(Traits trait)
        {
            if (immunities == null) return false;
            return immunities.Test(trait);
        }

        public uint GetMaxHP()
        {
            return hp;
        }

        public List<IAction> GetActions()
        {
            return actions;
        }

        public int GetAbilityModifier(AbilityType ability)
        {
            return abilityModifiers[(int)ability];
        }
    }
}