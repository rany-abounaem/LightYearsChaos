using System;


namespace LightYearsChaos
{
    [Serializable]
    public abstract class UnitState
    {
        protected Unit unit;
        protected UnitStateManager stateManager;

        public UnitState(Unit unit, UnitStateManager stateManager)
        {
            this.unit = unit;
            this.stateManager = stateManager;
        }

        public virtual void Enter()
        {

        }


        public virtual void Update()
        {

        }


        public virtual void Exit()
        {

        }
    }
}

