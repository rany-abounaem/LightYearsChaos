using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LightYearsChaos
{
    [Serializable]
    public class UnitStateManager : MonoBehaviour
    {
        private Unit unit;
        private UnitState currentState;
        private List<UnitState> existingStates = new List<UnitState>();

        public UnitState CurrentState { get { return currentState; } }


        public void Setup(Unit unit, UnitState state)
        {
            this.unit = unit;
            SetState(state);
        }


        public void Tick(float delta)
        {
            currentState.Update();
        }


        public UnitState GetExistingState<T>()
        {
            foreach (var state in existingStates)
            {
                if (state is T)
                {
                    return state;
                }
            }
            return null;
        }


        public void SetState(UnitState state)
        {
            Debug.Log(state);
            if (!existingStates.Contains(state))
            {
                existingStates.Add(state);
            }
                
            if (currentState != null)
            {
                currentState.Exit();
            }
            currentState = state;

            currentState.Enter();
            
        }
    }
}

