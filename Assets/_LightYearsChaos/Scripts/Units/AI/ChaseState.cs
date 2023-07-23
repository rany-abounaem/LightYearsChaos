using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LightYearsChaos
{
    public class ChaseState : UnitState
    {
        private Unit target;

        public Unit Target { get { return target; } set { target = value; } }


        public ChaseState(Unit unit, UnitStateManager stateManager, Unit target) : base(unit, stateManager)
        {
            this.target = target;
        }


        public override void Enter()
        {
            base.Enter();
            unit.Movement.Move(target.transform.position);
            unit.Anim.SetBool("IsMoving", true);
            unit.Movement.OnMovementUpdate += HandleMovementUpdate;
            unit.Sensor.Activate();
            unit.Sensor.OnEnemyDetected += HandleEnemyDetected;
        }


        public override void Update()
        {
            base.Update();
            unit.Anim.SetFloat("Speed", unit.Agent.velocity.magnitude / unit.Agent.speed);
        }


        public override void Exit()
        {
            base.Exit();
            unit.Movement.OnMovementUpdate -= HandleMovementUpdate;
            unit.Agent.isStopped = true;
            unit.Anim.SetBool("IsMoving", false);
            unit.Sensor.Deactivate();
            unit.Sensor.OnEnemyDetected -= HandleEnemyDetected;
        }


        private void HandleMovementUpdate(bool state)
        {
            if (!state)
            {
                unit.Movement.Move(target.transform.position);
            }
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

