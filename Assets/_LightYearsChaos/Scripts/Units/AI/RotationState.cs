//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;


//namespace LightYearsChaos
//{
//    public class RotationState : UnitState
//    {
//        private Vector3 target;

//        public Vector3 Target { get { return target; } set { target = value; } }


//        public RotationState(Unit unit, UnitStateManager stateManager, Vector3 target) : base(unit, stateManager)
//        {
//            this.target = target;
//        }


//        public override void Enter()
//        {
//            base.Enter();
//            var toRotation = Quaternion.LookRotation((target - unit.transform.position).normalized, unit.transform.up);
//            unit.Movement.Rotate(toRotation);
//            unit.Movement.OnRotationUpdate += HandleRotationUpdate;
//        }


//        public override void Update()
//        {
//            base.Update();
//        }


//        public override void Exit()
//        {
//            base.Exit();
//            unit.Movement.OnRotationUpdate -= HandleRotationUpdate;
//        }


//        private void HandleRotationUpdate(bool rotationState)
//        {
//            if (!rotationState)
//            {
//                var idleState = stateManager.GetExistingState<IdleState>();
//                stateManager.SetState((IdleState)idleState);
//            }
//        }
//    }
//}

