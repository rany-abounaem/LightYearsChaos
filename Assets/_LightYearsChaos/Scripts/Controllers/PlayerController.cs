using System;
using Unity.AI.Navigation;
using UnityEngine;


namespace LightYearsChaos
{
    public class PlayerController
    {
        private InputManager input;
        private Unit currentSelection;
        private Unit currentTarget;

        public Unit CurrentSelection { get { return currentSelection; } }
        public Unit CurrentTarget { get { return currentTarget; } }

        public event Action<Unit> OnSelectionUpdate;
        public event Action<Unit> OnTargetUpdate;


        public PlayerController(InputManager input)
        {
            this.input = input;
            Setup();
        }

        public void Setup()
        {
            input.OnSelect += TrySelectUnit;
            input.OnAction += TryCommandUnit;
        }


        private void TrySelectUnit(Vector2 mousePos)
        {
            var camera = Camera.main;
            var ray = camera.ScreenPointToRay(mousePos);
            var layerMask = 1 << 6;
            layerMask = ~layerMask;

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
            {
                if (hit.collider.TryGetComponent(out Unit unit) && unit.TeamId == 0)
                {
                    currentSelection = unit;
                    unit.SelectionObject.SetActive(true);
                    OnSelectionUpdate?.Invoke(currentSelection);
                }
                else
                {
                    if (currentSelection != null)
                    {
                        currentSelection.SelectionObject.SetActive(false);
                        currentSelection = null;
                        OnSelectionUpdate?.Invoke(currentSelection);
                    }
                    
                }
            }
            
        }

        
        private void TryCommandUnit(Vector2 mousePos)
        {
            if (currentSelection == null)
            {
                return;
            }
                
            var camera = Camera.main;
            var ray = camera.ScreenPointToRay(mousePos);
            var layerMask = 1 << 6;
            layerMask = ~layerMask;

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
            {
                if (hit.collider.TryGetComponent(out NavMeshSurface surface))
                {
                    if (currentTarget != null)
                    {
                        currentTarget = null;
                        OnTargetUpdate?.Invoke(null);
                    }
                    
                    var stateManager = currentSelection.StateManager;
                    var movementState = stateManager.GetExistingState<MovementState>();
                    if (movementState == null)
                    {
                        movementState = new MovementState(currentSelection, stateManager, hit.point);
                    }
                    else
                    {
                        ((MovementState)movementState).Destination = hit.point;
                    }

                    stateManager.SetState((MovementState)movementState);
                }
                else if (hit.collider.TryGetComponent(out Unit unit) && unit.TeamId != currentSelection.TeamId)
                {
                    currentTarget = unit;
                    OnTargetUpdate?.Invoke(unit);

                    var stateManager = currentSelection.StateManager;
                    var chaseState = stateManager.GetExistingState<ChaseState>();

                    if (chaseState == null)
                    {
                        chaseState = new ChaseState(currentSelection, stateManager, unit);
                    }
                    else
                    {
                        ((ChaseState)chaseState).Target = unit;
                    }

                    stateManager.SetState(chaseState);

                    //if (stateManager.CurrentState is IdleState || stateManager.CurrentState is MovementState)
                    //{
                    //    var closestPoint = currentSelection.Movement.GetClosestPoint(unit.transform.position, currentSelection.Weapon.MaxFiringRange);
                        
                    //    if (Vector3.Distance (currentSelection.transform.position, closestPoint) < 0.1f)
                    //    {
                    //        var rotationState = stateManager.GetExistingState<RotationState>();
                    //        if (rotationState == null)
                    //        {
                    //            rotationState = new RotationState(currentSelection, stateManager, hit.transform.position);
                    //        }
                    //        else
                    //        {
                    //            ((RotationState)rotationState).Target = hit.transform.position;
                    //        }

                    //        stateManager.SetState(rotationState);
                    //    }
                    //    else
                    //    {
                    //        var movementState = stateManager.GetExistingState<MovementState>();
                    //        if (movementState == null)
                    //        {
                    //            movementState = new MovementState(currentSelection, stateManager, closestPoint);
                    //        }
                    //        else
                    //        {
                    //            ((MovementState)movementState).Destination = closestPoint;
                    //        }

                    //        stateManager.SetState(movementState);
                    //    }
                        
                    //}
                }
            }
        }
    }
}