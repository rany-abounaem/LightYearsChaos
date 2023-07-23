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
            base.Enter();
            unit.Sensor.OnEnemyDetected += HandleEnemyDetected;
            unit.Sensor.Activate();
        }


        public override void Update()
        {
            base.Update();
        }


        public override void Exit()
        {
            base.Exit();
            unit.Sensor.Deactivate();
            unit.Sensor.OnEnemyDetected -= HandleEnemyDetected;
        }


        private void HandleEnemyDetected(Unit enemy)
        {
            var aggroState = stateManager.GetExistingState<AggroState>();

            if (aggroState == null)
            {
                aggroState = new AggroState(unit, stateManager, enemy);
            }
            else
            {
                ((AggroState)aggroState).Target = enemy;
            }

            stateManager.SetState(aggroState);
        }
    }
}