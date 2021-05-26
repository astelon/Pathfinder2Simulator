
using System.Collections.Generic;

namespace PF2 {

    class Skill
    {
        public Skill(string n, AbilityType a, string d, IAction[] aarray)
        {
            name = n; description = d; mainAbility = a; actions = aarray;
        }
        public string name { get; set; }
        public string description { get; set; }
        public IAction[] actions;
        public AbilityType mainAbility;
        static Skill[] skills = new Skill[] {
            new Skill("Acrobatics", AbilityType.Dex, "Ability to move skillfully", new IAction[]{ new BalanceAction() })
        };
    }

    class CreatureSkill
    {
        public Skill data;
        public Proficiency proficiency;
    }
}