namespace PF2 {
    class BalanceAction : IAction
    {
        public void AddObserver(IObserver observer)
        {
            throw new System.NotImplementedException();
        }
        public ActionResult Execute(ICreatureInstance other, int modifier = 0)
        {
            throw new System.NotImplementedException();
        }
        public ActionType GetActionType()
        {
            throw new System.NotImplementedException();
        }
        public uint GetCost()
        {
            throw new System.NotImplementedException();
        }
        public string GetName()
        {
            return "Balance";
        }
        public bool HasTrait(Traits trait)
        {
            throw new System.NotImplementedException();
        }
        public Proficiency MinProficiency()
        {
            return Proficiency.Untrained;
        }
    }
}