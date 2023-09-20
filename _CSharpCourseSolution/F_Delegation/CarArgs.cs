using System;

namespace F_Delegation
{
    public class CarArgs : EventArgs
    {
        public CarArgs(int currentSpeed)
        {
            CurrentSpeed = currentSpeed;
        }

        public int CurrentSpeed { get; }
    }
}
