using System.Collections.Generic;

namespace PF2
{
    public interface ICreatureData
    {
        public string GetName();
        public int AC();
        public uint WeakTo(DamageType type);
        public uint ResistantTo(DamageType type);
        public bool ImmuneTo(Traits trait);
        public uint GetMaxHP();
        public List<IAction> GetActions();
    }
    public enum SkillType
    {
        Perception = -1,
        Acrobatics = 0, // (Dex)
        Arcana, // (Int)
        Athletics, // (Str)
        Crafting, // (Int)
        Deception, // (Cha)
        Diplomacy, // (Cha)
        Intimidation, // (Cha)
        Lore, // (Int)
        Medicine, // (Wis)
        Nature, // (Wis)
        Occultism, // (Int)
        Performance, // (Cha)
        Religion, // (Wis)
        Society, // (Int)
        Stealth, // (Dex)
        Survival, // (Wis)
        Thievery, // (Dex)
    }
    public interface ICreatureInstance
    {
        public string GetName();
        public uint GetCurrentHP();
        public void ReduceHitPoints(uint dmg);
        public ICreatureData GetCreatureInfo();
        public bool IsActive();
        public ExplorationActivityType GetCurrentExplorationActivity();
        public bool InCombatMode();
        public int PerceptionDC();
        public int RollIniitative();
        public void ResetCombatActions();
        public void ConsumeActions(uint count);
        public void ConsumeReaction();
        public uint AvailableActions();
        public bool ReactionAvailable();
        public int GetAbilityModifier(AbilityType abilityType);
        public IBattleFigure GetBattleFigure();
    }
}