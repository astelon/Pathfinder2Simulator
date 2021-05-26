namespace Bestiary
{
    public class Root
    {
        public string name { get; set; }
        public string date { get; set; }
        public Monster[] monsters { get; set; }
    }

    public class Monster
    {
        public string name { get; set; }
        public string level { get; set; }
        public string link { get; set; }
        public string family { get; set; }
        public string source { get; set; }
        public string[] traits { get; set; }
        public string alignment { get; set; }
        public string size { get; set; }
        public string type { get; set; }
        public string[] specials { get; set; }
        public string[] senses { get; set; }
        public string[] languages { get; set; }
        public string[] skills { get; set; }
        public string[] abilities { get; set; }
        public string[] items { get; set; }
        public string ac { get; set; }
        public string[] saves { get; set; }
        public string[] hp { get; set; }
        public string[] immunities { get; set; }
        public string[] weaknesses { get; set; }
        public string[] resistances { get; set; }
        public string[] speed { get; set; }
        public string[] actions { get; set; }
        public InnateSpells innateSpells { get; set; }
        public string[] monsterText { get; set; }
        public Rituals rituals { get; set; }
        public string frequency { get; set; }
        public PrimalInnateSpells primalInnateSpells { get; set; }
        public PrimalPreparedSpells primalPreparedSpells { get; set; }
        public Spells spells { get; set; }
        public PreparedSpells preparedSpells { get; set; }
        public string[] breathWeapon { get; set; }
        public SpontaneousSpells spontaneousSpells { get; set; }
    }

    public class InnateSpells
    {
        public string dc { get; set; }
        public string[] spells { get; set; }
        public string attack { get; set; }
    }

    public class Rituals
    {
        public string dc { get; set; }
        public string[] rituals { get; set; }
    }

    public class PrimalInnateSpells
    {
        public string dc { get; set; }
        public string[] spells { get; set; }
        public string attack { get; set; }
    }

    public class PrimalPreparedSpells
    {
        public string dc { get; set; }
        public string attack { get; set; }
        public string[] spells { get; set; }
    }

    public class Spells
    {
        public string dc { get; set; }
        public string attack { get; set; }
        public string[] spells { get; set; }
    }

    public class PreparedSpells
    {
        public string dc { get; set; }
        public string attack { get; set; }
        public string note { get; set; }
        public string[] spells { get; set; }
    }

    public class SpontaneousSpells
    {
        public string dc { get; set; }
        public string attack { get; set; }
        public string[] spells { get; set; }
    }

}