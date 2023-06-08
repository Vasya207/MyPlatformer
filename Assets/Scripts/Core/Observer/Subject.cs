using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public abstract class Subject : MonoBehaviour
    {
        // a collection of all the observers of this subject
        private List<IObserver> observers = new List<IObserver>();

        public void AddObserver(IObserver observer)
        {
            observers.Add(observer);
        }

        public void RemoveObserver(IObserver observer)
        {
            observers.Remove(observer);
        }

        protected void NotifyObservers(float value)
        {
            observers.ForEach((observer) =>
            {
                observer.OnNotify(value);
            });
        }
    }
}


