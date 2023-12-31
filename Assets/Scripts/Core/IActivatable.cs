﻿namespace Archero
{
    public interface IActivatable
    {
        bool IsActive { get; }

        void Activate();
        void Deactivate();
    }
}
