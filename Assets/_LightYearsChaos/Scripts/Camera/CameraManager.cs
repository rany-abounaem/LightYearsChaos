using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LightYearsChaos
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private float minZoom;
        [SerializeField] private float maxZoom;
        [SerializeField] private float scrollSpeed;
        [SerializeField] private float panSpeed;
        private float initialZoom;
        private Vector2 panValue;


        public void Setup(InputManager input)
        {
            input.OnCameraZoom += UpdateCameraZoom;
            input.OnCameraPan += PanCamera;
            initialZoom = transform.localPosition.z;
            Debug.Log(initialZoom);
        }

        
        private void UpdateCameraZoom(int scrollDir)
        {
            if ((scrollDir < 0 && transform.localPosition.z <= initialZoom - minZoom) || (scrollDir > 0 && transform.localPosition.z >= initialZoom + maxZoom))
            {
                return;
            }
            Debug.Log("Z before zooming: " + transform.localPosition.z);
            transform.Translate(new Vector3(0, 0, scrollSpeed * Time.deltaTime * scrollDir));
            Debug.Log("Zoom value: " + scrollSpeed * Time.deltaTime * scrollDir);
            Debug.Log("Z after zooming: " + transform.localPosition.z);
        }


        private void PanCamera(Vector2 mousePos)
        {
            if (mousePos.x >= Screen.width - 2)
            {
                panValue.x = 1;
            }
            else if (mousePos.x <= 2)
            {
                panValue.x = -1;
            }
            else
            {
                panValue.x = 0;
            }

            if (mousePos.y >= Screen.height - 2)
            {
                panValue.y = 1;
            }
            else if (mousePos.y <= 2)
            {
                panValue.y = -1;
            }
            else
            {
                panValue.y = 0;
            }
        }


        private void Update()
        {
            if (panValue.magnitude > 0)
            {
                transform.Translate(new Vector3(panSpeed * panValue.x * Time.deltaTime, 0));

                var dir = Vector3.ProjectOnPlane(transform.up, Vector3.up);
                transform.Translate(dir * panSpeed * panValue.y * Time.deltaTime, Space.World);
            }
        }
    }
}

