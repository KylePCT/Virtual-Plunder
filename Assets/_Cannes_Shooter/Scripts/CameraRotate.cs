using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cannes_Shooter
{
    /*
     * Temporary bit of code to substitute for the hardware camera movement.
     * Just uses the mouse to move the camera of the character.
     * - Kyle Tugwell 27/04/2022
     */

    public class CameraRotate : MonoBehaviour
    {
        [Header("Parameters")]
        [Tooltip("The sensitivity of the turning of the camera using the mouse.")]
        public float mouseSensitivity = 100f;
        public Transform body;
        float camRotation = 0f;

        void Start()
        {
            //Remove cursor.
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        void Update()
        {
            //Move camera based on axis input.
            float _mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float _mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            camRotation -= _mouseY; //Stops flipping the camera when using +=.
            camRotation = Mathf.Clamp(camRotation, -90f, 90f); //Clamp vertical movement.

            transform.localRotation = Quaternion.Euler(camRotation, 0f, 0f); //Vertical movement.
            body.Rotate(Vector3.up * _mouseX); //Horizontal movement.

        }
    }
}