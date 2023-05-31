using Unity.VisualScripting;

namespace Core
{
    public interface IObserver
    {
        public void OnNotify(string action);
    }
}