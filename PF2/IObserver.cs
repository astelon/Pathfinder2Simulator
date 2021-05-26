namespace PF2
{
    public interface IObserver
    {
        public void Message(string message);
    }
    public interface IObservable
    {
        public void AddObserver(IObserver observer);
    }
}