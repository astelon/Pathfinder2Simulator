using System.Collections.Generic;

namespace PF2
{
    public enum BasicActionType
    {
        Aid,
        Crawl,
        Delay,
        DropProne,
        Escape,
        Interact,
        InvestAnItem,
        Leap,
        Ready,
        Release,
        Seek,
        SenseMotive,
        Stand,
        Step,
        Stride,
        Strike,
        TakeCover,
        //SpecialtyBased
        ActivateAnItem,
        ArrestAFall,
        AvertGaze,
        Burrow,
        CastASpell,
        Dismiss,
        Fly,
        GrabAnEdge,
        Mount,
        PointOut,
        RaiseAShield,
        SustainASpell,
        SustainAnActivation
    }

    public abstract class BasicAction : IAction {
        private static List<IObserver> observers = new List<IObserver>();
        TraitsBitmap traits;
        protected string Name { get; set; }
        protected BasicActionType Type { get; set; }

        public void AddObserver(IObserver observer)
        {
            observers.Add(observer);
        }

        public void Message(string message)
        {
            foreach (IObserver obs in observers)
            {
                obs.Message(message);
            }
        }

        public static void SetObserver(IObserver obs)
        {
            observers.Add(obs);
        }

        public abstract ActionResult Execute(ICreatureInstance other, int modifier = 0);

        public abstract ActionType GetActionType();

        public abstract uint GetCost();

        public string GetName()
        {
            return Name;
        }

        public bool HasTrait(Traits trait)
        {
            return traits.Test(trait);
        }
        protected void SetTraits(Traits[] t)
        {
            foreach (Traits trait in t)
            {
                traits.Add(trait);
            }
        }

        public abstract Proficiency MinProficiency();

        public BasicActionType GetBasicActionType()
        {
            return Type;
        }
    }

    public class Stride : BasicAction
    {
        BattleMap.IBattleMapProvider battleMapProvider;
        public Stride()
        {
            Name = "Stride";
            SetTraits(new Traits[] { Traits.Move });
        }
        public override ActionResult Execute(ICreatureInstance other, int modifier = 0)
        {
            if (battleMapProvider == null) return ActionResult.Error;
            if (other.GetBattleFigure() == null) return ActionResult.Error;
            battleMapProvider.GetCurrentBattleMap().Move(other.GetBattleFigure(),1,0);
            return ActionResult.Success;
        }

        public override ActionType GetActionType()
        {
            return ActionType.Action;
        }

        public override uint GetCost()
        {
            return 1;
        }

        public override Proficiency MinProficiency()
        {
            return Proficiency.Untrained;
        }
    }
    public static class BasicActions
    {
        public static Dictionary<BasicActionType,BasicAction> actions = new Dictionary<BasicActionType, BasicAction>()
        {
            { BasicActionType.Stride, new Stride() }
        };
        public static void AddObserver(IObserver obs)
        {
            BasicAction.SetObserver(obs);
        }
    }
}