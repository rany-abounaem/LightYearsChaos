using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LightYearsChaos
{
    public class AggroState : UnitState
    {
        private Unit target;

        public Unit Target { get { return target; } set { target = value; } }


        public AggroState(Unit unit, UnitStateManager stateManager, Unit target) : base(unit, stateManager)
        {
            this.target = target;
        }


        public override void Enter()
        {
            base.Enter();
            unit.Movement.Rotate(target.transform.position);
            target.Movement.OnMovementUpdate += HandleTargetMovement;
        }


        public override void Update()
        {
            base.Update();
        }


        public override void Exit()
        {
            base.Exit();
        }


        private void HandleTargetMovement(bool state)
        {
            if (!state)
            {
                unit.Movement.Rotate(target.transform.position);
            }
        }
    }
}

