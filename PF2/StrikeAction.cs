using System.Collections.Generic;

namespace PF2
{
    class StrikeAction : IAction, IObservable
    {
        public RangeType Range { get; set; }
        private string Name;
        public Check attackRoll { get; set; }
        public DamageRoll damageRoll { get; set; }
        private List<IObserver> observers;
        ActionType actionType = ActionType.Action;
        uint actionCost = 0;

        public StrikeAction(RangeType range, string name, int modifier, List<DamageSource> damageSources, List<IAction> followUpSkills, List<Traits> additionalTraits, ActionType type=ActionType.Action, uint actionCount=1)
        {
            Range = range;
            Name = char.ToUpper(name[0]) + name.Substring(1);
            actionType = type;
            actionCost = actionCount;
            attackRoll = new Check();
            attackRoll.AddDieSource(new DieSource(1, DieType.D20, modifier));
            attackRoll.AddTrait(Traits.Attack);
            damageRoll = new DamageRoll();
            if (additionalTraits != null)
            {
                foreach (Traits trait in additionalTraits)
                {
                    attackRoll.AddTrait(trait);
                }
            }
            if (damageSources != null)
            {
                foreach (var damageSrc in damageSources)
                {
                    damageRoll.AddSource(damageSrc);
                }
            }
        }
        public uint GetCost()
        {
            return actionCost;
        }
        public bool HasTrait(Traits trait)
        {
            return attackRoll.HasTrait(trait);
        }
        public ActionResult Execute(ICreatureInstance other, int modifier=0)
        {
            Check.TestResult res = attackRoll.Test(other.GetCreatureInfo().AC(),modifier);
            Message(Name + ": Attack roll = " + res.value + " => " + res.type);
            if (!res.Succeeded())
            {
                return ActionResult.Failure;
            }
            foreach (Traits t in attackRoll.GetTraitsBitmap().Iterate())
            {
                if (other.GetCreatureInfo().ImmuneTo(t))
                {
                    return ActionResult.Failure;
                }
            }
            foreach (Damage d in damageRoll.Roll())
            {
                uint points = d.value;
                if (res.type == CheckResultType.CriticalSuccess)
                {
                    points *= 2;
                }
                Message("Original damage:" + points);
                points -= other.GetCreatureInfo().ResistantTo(d.type);
                points += other.GetCreatureInfo().WeakTo(d.type);
                Message(Name + ": " + other.GetName() + " received " + points + " points of " + d.type.ToString() + " damage.");
                other.ReduceHitPoints(points);
            }
            return ActionResult.Success;
        }

        public string GetName()
        {
            return Name;
        }

        public Proficiency MinProficiency()
        {
            return Proficiency.Untrained;
        }

        public ActionType GetActionType()
        {
            return actionType;
        }

        public void AddObserver(IObserver observer)
        {
            if (observers == null) observers = new List<IObserver>();
            observers.Add(observer);
        }
        private void Message(string message)
        {
            if (observers == null) return;
            foreach (IObserver observer in observers)
            {
                observer.Message(message);
            }
        }
    }
}