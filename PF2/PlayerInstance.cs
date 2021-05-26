using System;
using System.Collections.Generic;

namespace PF2
{
    class PlayerInstance : ICreatureInstance, IObservable, ICreatureData
    {
        public string name;
        public uint currentHp = 0;
        public uint maxHp = 0;
        public int ac = 0;
        public bool isAlive = true;
        public int perception = 0;
        List<IObserver> observers = null;
        public List<IAction> actions = null;
        public ExplorationActivityType currentExplorationActivity;
        public uint[] abilityScores = new uint[(int)(AbilityType.AbilityCount)];
        uint availableActions = 0;
        bool reactioinAvailable = false;

        public PlayerInstance()
        {
            actions = new List<IAction>();
        }
        public int AC()
        {
            return ac;
        }

        public void AddObserver(IObserver observer)
        {
            if (observers == null) observers = new List<IObserver>();
            observers.Add(observer);
        }
        public void Message(string message)
        {
            if (observers == null) return;
            foreach (IObserver observer in observers)
            {
                observer.Message(message);
            }
        }
        public List<IAction> GetActions()
        {
            return actions;
        }

        public ICreatureData GetCreatureInfo()
        {
            return this;
        }

        public uint GetCurrentHP()
        {
            return currentHp;
        }

        public uint GetMaxHP()
        {
            return maxHp;
        }

        public string GetName()
        {
            return name;
        }

        public bool ImmuneTo(Traits trait)
        {
            return false;
        }

        public bool IsActive()
        {
            return isAlive;
        }

        public void ReduceHitPoints(uint dmg)
        {
            if (currentHp <= dmg)
            {
                currentHp = 0;
            }
            else
            {
                currentHp -= dmg;
            }
            Message(GetName() + "'s current HP: " + currentHp);
            isAlive = (currentHp > 0);
        }

        public uint ResistantTo(DamageType type)
        {
            return 0;
        }

        public uint WeakTo(DamageType type)
        {
            return 0;
        }

        public ExplorationActivityType GetCurrentExplorationActivity()
        {
            return currentExplorationActivity;
        }

        public bool InCombatMode()
        {
            return true;
        }

        public int PerceptionDC()
        {
            return 10 + perception;
        }

        public int RollIniitative()
        {
            return DieRoll.Roll(DieSource.d20) + perception;
        }
        public void ResetCombatActions()
        {
            availableActions = 3;
            reactioinAvailable = true;
        }

        public void ConsumeActions(uint count)
        {
            if (count <= availableActions)
            {
                availableActions -= count;
            }
        }

        public void ConsumeReaction()
        {
            reactioinAvailable = false;
        }

        public uint AvailableActions()
        {
            return availableActions;
        }

        public bool ReactionAvailable()
        {
            return reactioinAvailable;
        }

        public int GetAbilityModifier(AbilityType abilityType)
        {
            return ((int)abilityScores[(int)abilityType] / 2) - 5;
        }

        public IBattleFigure GetBattleFigure()
        {
            return null;
        }
    }
}