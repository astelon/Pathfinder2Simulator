namespace PF2
{
    public enum AbilityType
    {
        Str, Dex, Con, Int, Wis, Cha, AbilityCount
    }
    public enum DamageType
    {
        //Pysical Damage
        Piercing,
        Bludgeoning,
        Slashing,
        //Mind Damage
        Mental,
        //Energy Damage
        Acid,
        Electricity,
        Force,
        Fire,
        Cold,
        Sonic,
        Positive,
        Negative
    }
    public enum DieType
    {
        D100 = 100,
        D20 = 20,
        D12 = 12,
        D10 = 10,
        D8 = 8,
        D6 = 6,
        D4 = 4,
        None = 0
    }
    public enum ActionType
    {
        Free,
        Reaction,
        Action
    }
    public enum CheckResultType
    {
        CriticalSuccess = 3,
        Success = 2,
        Failure = 1,
        CriticalFailure = 0
    }
    public enum SavingThrowType
    {
        Fort,
        Ref,
        Will,
    }
    public enum RangeType
    {
        Touch = 1,
        Melee = 1,
        Range30 = 6,
        Range60 = 12,
        Range120 = 24
    }
    public enum ExplorationActivityType
    {
        AvoidNotice,
        Defend,
        DetectMagic,
        FollowTheExpert,
        Hustle,
        Investigate,
        RepeatASpell,
        Scout,
        Search
    }
    public enum Rarity
    {
        Common,
        Uncommon,
        Rare,
        Unique
    }
    public enum LawChaosAlignment
    {
        Lawful,
        Neutral,
        Evil
    }
    public enum GoodEvilAlignment
    {
        Good,
        Neutral,
        Evil
    }

    public enum CreatureSize
    {
        Diminute,
        Tiny,
        Small,
        Medium,
        Large,
        Huge,
        Gargantuan
    }
    public enum Proficiency
    {
        Untrained = 0,
        Trained,
        Expert,
        Master,
        Legendary
    }
}