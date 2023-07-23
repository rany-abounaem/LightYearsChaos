using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

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
            unit.Weapon.ActivateAttacking(target);
            unit.Anim.SetBool("IsAttacking", true);
            target.OnDeath += HandleTargetDeath;
        }


        public override void Update()
        {
            base.Update();
            if (target.Agent.hasPath && !target.Agent.isStopped)
            {
                unit.Movement.Rotate(target.transform.position, true);
            }

            if (Vector3.Distance(target.transform.position, unit.transform.position) > 10)
            {
                var chaseState = stateManager.GetExistingState<ChaseState>();
                if (chaseState == null)
                {
                    chaseState = new ChaseState(unit, stateManager, target);
                }
                else
                {
                    ((ChaseState)chaseState).Target = target;
                }
                stateManager.SetState(chaseState);
            }
        }


        public override void Exit()
        {
            base.Exit();
            unit.Weapon.DeactivateFiring();
            unit.Anim.SetBool("IsAttacking", false);
        }


        private void HandleTargetDeath()
        {
            var idleState = stateManager.GetExistingState<IdleState>();
            if (idleState == null)
            {
                idleState = new IdleState(unit, stateManager);
            }
            stateManager.SetState(idleState);
        }
    }
}