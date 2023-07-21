using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LightYearsChaos
{
    public class IdleState : UnitState
    {
        public IdleState (Unit unit, UnitStateManager stateManager ): base(unit, stateManager)
        {

        }

        public override void Enter()
        {
            Debug.Log("Entered Idle State");
            base.Enter();
            unit.Sensor.OnEnemyDetected += HandleEnemyDetected;
        }


        public override void Update()
        {
            base.Update();
        }


        public override void Exit()
        {
            base.Exit();
            unit.Sensor.OnEnemyDetected -= HandleEnemyDetected;
        }


        private void HandleEnemyDetected(Unit unit)
        {
            var aggroState = stateManager.GetExistingState<AggroState>();

            if (aggroState == null)
            {
                aggroState = new AggroState(unit, stateManager);
            }

            stateManager.SetState(aggroState);
        }
    }
}
