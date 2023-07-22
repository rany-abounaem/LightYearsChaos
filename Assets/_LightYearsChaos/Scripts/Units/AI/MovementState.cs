using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LightYearsChaos
{
    public class MovementState : UnitState
    {
        private Vector3 destination;

        public Vector3 Destination { get { return destination; } set { destination = value; } }


        public MovementState(Unit unit, UnitStateManager stateManager, Vector3 destination) : base(unit, stateManager)
        {
            this.destination = destination;
        }


        public override void Enter()
        {
            base.Enter();
            unit.Movement.Move(destination);
            unit.Movement.OnMovementUpdate += HandleMovementUpdate;
        }


        public override void Update()
        {
            base.Update();
        }


        public override void Exit()
        {
            base.Exit();
            unit.Movement.OnMovementUpdate -= HandleMovementUpdate;
        }


        public void HandleMovementUpdate(bool state)
        {
            if (!state)
            {
                var idleState = stateManager.GetExistingState<IdleState>();
                if (idleState == null)
                {
                    idleState = new IdleState(unit, stateManager);
                }
                stateManager.SetState((IdleState)idleState);
            }
        }
    }
}

