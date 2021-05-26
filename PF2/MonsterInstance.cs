using System.Collections.Generic;

namespace PF2
{
    public class MonsterInstance : ICreatureInstance, IObservable
    {
        string name;
        MonsterData monsterInformation;
        uint hp;
        List<IObserver> observers;
        ExplorationActivityType currentExplorationActivity;
        uint availableActions = 0;
        bool reactioinAvailable = false;

        public MonsterInstance(MonsterData m, string n = null)
        {
            hp = m.GetMaxHP();
            monsterInformation = m;
            name = n;
        }

        public void AddObserver(IObserver observer)
        {
            if (observers == null) observers = new List<IObserver>();
            observers.Add(observer);
        }

        private void Message(string message)
        {
            foreach (IObserver observer in observers)
            {
                observer.Message(message);
            }
        }

        public ICreatureData GetCreatureInfo()
        {
            return monsterInformation;
        }

        public uint GetCurrentHP()
        {
            return hp;
        }

        public string GetName()
        {
            if (name == null) return monsterInformation.GetName();
            return name;
        }

        public void ReduceHitPoints(uint dmg)
        {
            if (hp < dmg)
            {
                hp = 0;
            }
            else
            {
                hp -= dmg;
            }

            Message(GetName() + "'s current HP: " + hp);
        }

        public bool IsActive()
        {
            return (hp > 0);
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
            return (10 + monsterInformation.perception);
        }

        public int RollIniitative()
        {
            return DieRoll.Roll(DieSource.d20) + monsterInformation.perception;
        }

        public void ResetCombatActions()
        {
            availableActions = 3;
            reactioinAvailable = true;
        }

        public void ConsumeActions(uint count)
        {
            if(count <= availableActions)
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
            throw new System.NotImplementedException();
        }

        public IBattleFigure GetBattleFigure()
        {
            return null;
        }
    }
}