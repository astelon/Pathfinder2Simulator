using System;
using System.Collections.Generic;
using System.Text.Json;

namespace PF2
{
    public class MonsterFactory
    {
        private static Bestiary.Root bestiaryData;
        private static Dictionary<string, MonsterData> monsterLibraryByName = new Dictionary<string, MonsterData>();
        private static Dictionary<int, List<MonsterData>> monsterLibraryByLevel = new Dictionary<int, List<MonsterData>>();
        public static void LoadData(string jsonData)
        {
            bestiaryData = JsonSerializer.Deserialize<Bestiary.Root>(jsonData);
            foreach (Bestiary.Monster monsterData in bestiaryData.monsters)
            {
                MonsterData monster = new MonsterData();
                monster.level = int.Parse(monsterData.level);
                monster.name = monsterData.name;
                string[] abilities;
                (monster.hp, abilities) = Bestiary.Parser.ParseHp(monsterData.hp); //Hp array has information on hp abilities
                if ((abilities != null) && (abilities.Length > 0))
                {
                    monster.hpAutoAbilities = new List<string>(abilities);
                }

                monster.ac = int.Parse(monsterData.ac.Split(' ')[0]);
                if (monsterData.resistances != null)
                {
                    monster.resistances = new Dictionary<DamageType, uint>();
                    foreach (Tuple<DamageType, uint> resistance in Bestiary.Parser.ParseWeaknessesAndResistances(monsterData.resistances))
                    {
                        monster.resistances[resistance.Item1] = resistance.Item2;
                    }
                }

                if (monsterData.weaknesses != null)
                {
                    monster.weaknesses = new Dictionary<DamageType, uint>();
                    foreach (Tuple<DamageType, uint> weakness in Bestiary.Parser.ParseWeaknessesAndResistances(monsterData.weaknesses))
                    {
                        monster.weaknesses[weakness.Item1] = weakness.Item2;
                    }
                }

                if ((monsterData.actions != null) && (monsterData.actions.Length > 0))
                {
                    monster.actions = new List<IAction>();
                    foreach (string action_str in monsterData.actions)
                    {
                        PF2.IAction action;
                        if (Bestiary.Parser.ParseAction(action_str, out action))
                        {
                            monster.actions.Add(action);
                        }
                        else
                        {
                            //TODO: Log an error here
                        }
                    }
                }

                monsterLibraryByName[monsterData.name] = monster;

                if (!monsterLibraryByLevel.ContainsKey(monster.level))
                {
                    monsterLibraryByLevel[monster.level] = new List<MonsterData>();
                }
                monsterLibraryByLevel[monster.level].Add(monster);
            }
        }
        public static MonsterData GetDataByName(string name)
        {
            return monsterLibraryByName[name];
        }
        public static List<MonsterData> GetDataByLevel(int level)
        {
            return monsterLibraryByLevel[level];
        }
        public static string[] GetNames()
        {
            int size = 0;
            if (monsterLibraryByName != null) size = monsterLibraryByName.Count;
            string[] array = new string[size];
            if (size > 0) monsterLibraryByName.Keys.CopyTo(array, 0);
            return array;
        }
        public static int GetCount()
        {
            return monsterLibraryByName.Count;
        }
        public static MonsterInstance CreateInstanceByName(string name)
        {
            return new MonsterInstance(monsterLibraryByName[name]);
        }
        public static bool Contains(string name)
        {
            return monsterLibraryByName.ContainsKey(name);
        }
        public static bool Contains(int level)
        {
            return monsterLibraryByLevel.ContainsKey(level);
        }
    }
}