﻿using Unity.VisualScripting;

namespace Core
{
    public interface IObserver
    {
        public void OnNotify(float value);
    }
}