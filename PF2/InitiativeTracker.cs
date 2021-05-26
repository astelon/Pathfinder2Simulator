using System.Collections.Generic;

namespace PF2
{
    public class InitiativeTracker: Comparer<(int,ICreatureInstance,int)>, IObservable
    {
        List<IObserver> observers;
        List<(int, ICreatureInstance, int)> creatureInstancesInCombat;
        LinkedList<int> indexOrder; //Initiative number, index
        Dictionary<int, List<ICreatureInstance>> creatureInstanceByTeam;
        public InitiativeTracker()
        {
            creatureInstancesInCombat = new List<(int, ICreatureInstance, int)>();
            observers = new List<IObserver>();
            indexOrder = new LinkedList<int>();
            creatureInstanceByTeam = new Dictionary<int, List<ICreatureInstance>>();
        }
        public void Clear()
        {
            creatureInstancesInCombat.Clear();
            indexOrder.Clear();
            creatureInstanceByTeam.Clear();
        }
        public void AddObserver(IObserver observer)
        {
            observers.Add(observer);
        }
        public void AddTurn(ICreatureInstance instance, int team)
        {
            int roll = instance.RollIniitative();
            creatureInstancesInCombat.Add((roll, instance, team));
            if (creatureInstanceByTeam.ContainsKey(team) != true) creatureInstanceByTeam[team] = new List<ICreatureInstance>();
            creatureInstanceByTeam[team].Add(instance);
        }
        public override int Compare((int, ICreatureInstance,int) x, (int, ICreatureInstance,int) y)
        {
            return x.Item1.CompareTo(y.Item1);
        }
        public void StartInitiativeOrder()
        {
            creatureInstancesInCombat.Sort(this);
            creatureInstancesInCombat.Reverse();
            int index = 0;
            //Add them in order
            foreach (var pair in creatureInstancesInCombat)
            {
                Message("Adding : " + pair.Item1 + " to initiative order...");
                indexOrder.AddLast(index);
                index += 1;
            }
        }
        public List<ICreatureInstance> GetTeam(int team)
        {
            return creatureInstanceByTeam[team];
        }
        public IEnumerable<(ICreatureInstance, int)> DoRound()
        {
            //Get working copy
            LinkedList<int> order = new LinkedList<int>(indexOrder);
            while(order.Count > 0)
            {
                //TODO: Need to fix the iteration order
                LinkedListNode<int> node = order.First;
                int index = node.Value;
                order.RemoveFirst();
                (int roll, ICreatureInstance creature, int group) = creatureInstancesInCombat[index];
                if (creature.IsActive())
                {
                    Message("<" + group + "> [" + roll + "] : " + creature.GetName() + "'s turn");
                    yield return (creature, group);
                }
            }
        }
        private void Message(string text)
        {
            if (observers == null) return;
            foreach (IObserver observer in observers)
            {
                observer.Message(text);
            }
        }
    }
}