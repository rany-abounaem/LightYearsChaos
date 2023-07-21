using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LightYearsChaos
{
    public class AggroState : UnitState
    {
        public AggroState(Unit unit, UnitStateManager stateManager) : base(unit, stateManager)
        {

        }

        public override void Enter()
        {
            Debug.Log("Entered Aggro State");
            base.Enter();
        }


        public override void Update()
        {
            base.Update();
        }


        public override void Exit()
        {
            base.Exit();
        }
    }
}

