﻿using UnityEngine;

namespace Archero.States
{
    public partial class ControlPlayerState : OwneringState<IPlayer>, IFixedUpdatableState
    {
        public override void Enter()
        {
            Debug.Log("player enter on moving");
        }

        public override void Exit()
        {
            Owner.StopMove();
        }

        public void FixedUpdate()
        {
            Owner.Move();
            Owner.Rotate();
        }
    }
}
