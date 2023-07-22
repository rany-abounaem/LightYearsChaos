using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;


namespace LightYearsChaos
{
    public class PlayerController
    {
        private InputManager input;
        private Unit currentSelection;  

        public Unit CurrentSelection { get { return currentSelection; } }


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
                }
                else
                {
                    if (currentSelection != null)
                    {
                        currentSelection.SelectionObject.SetActive(false);
                        currentSelection = null;
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

                    stateManager.SetState(movementState);
                }
                else if (hit.collider.TryGetComponent(out Unit unit) && unit.TeamId != currentSelection.TeamId)
                {
                    var stateManager = currentSelection.StateManager;
                    if (stateManager.CurrentState is IdleState || stateManager.CurrentState is MovementState)
                    {
                        var closestPoint = currentSelection.Movement.GetClosestPoint(unit.transform.position, currentSelection.Weapon.MaxFiringRange);
                        Debug.Log(closestPoint);
                        var movementState = stateManager.GetExistingState<MovementState>();
                        if (movementState == null)
                        {
                            movementState = new MovementState(currentSelection, stateManager, closestPoint);
                        }
                        else
                        {
                            ((MovementState)movementState).Destination = closestPoint;
                        }

                        stateManager.SetState(movementState);
                    }
                }
            }
        }
    }
}

