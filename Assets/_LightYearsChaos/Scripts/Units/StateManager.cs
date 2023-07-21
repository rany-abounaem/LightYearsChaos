using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LightYearsChaos
{
    public class StateManager : MonoBehaviour
    {
        private Unit unit;
        private State currentState;


        public void Setup(Unit unit, State state)
        {
            currentState = state;
            this.unit = unit;
        }


        public void Tick(float delta)
        {
            currentState.Update();
        }


        public void SetState(State state)
        {
            currentState.Exit();
            currentState = state;
            currentState.Enter();
        }
    }
}

