namespace PF2
{
    public enum ActionResult
    {
        Success,
        Failure,
        Error
    }

    public interface IAction : IObservable
    {
        public ActionType GetActionType();
        public Proficiency MinProficiency();
        public uint GetCost();
        public string GetName();
        public ActionResult Execute(ICreatureInstance other, int modifier=0);
        public bool HasTrait(Traits trait);
    }
}