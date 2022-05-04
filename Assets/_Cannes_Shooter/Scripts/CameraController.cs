using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cannes_Shooter
{
    public class CameraController : MonoBehaviour
    {
        [Header("Parameters")]
        [Tooltip("The sensitivity of the turning of the camera using the mouse.")]
        public float mouseSensitivity = 100f;
        public Vector2 verticalRestrictions = new Vector2(-90f, 90f);
        public Vector2 horizontalRestrictions = new Vector2(-50f, 50f);

        [Header("Physical Parameters")]
        public Transform playerBody;
        public GameObject cannon;
        float camRotation = 0f;

        [Header("Camera Effects")]
        public AnimationCurve cameraShakeCurve;

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
            camRotation = Mathf.Clamp(camRotation, verticalRestrictions.x, verticalRestrictions.y); //Clamp vertical movement.

            transform.localRotation = Quaternion.Euler(camRotation, 0f, 0f); //Vertical movement.
            playerBody.Rotate(Vector3.up * _mouseX); //Horizontal movement.

            Quaternion _rot = transform.localRotation;
            _rot.eulerAngles = new Vector3(0f, Mathf.Clamp(transform.eulerAngles.y, horizontalRestrictions.x, horizontalRestrictions.y), 0f);
            playerBody.localRotation = _rot;
        }

        public IEnumerator cameraShake (float duration, float magnitude)
        {
            Vector3 _originalPos = transform.localPosition;
            float _elapsed = 0.0f;

            while (_elapsed < duration)
            {
                _elapsed += Time.deltaTime;
                float _strength = cameraShakeCurve.Evaluate(_elapsed / duration);
                transform.localPosition = _originalPos + Random.insideUnitSphere * _strength;

                yield return null;
            }

            transform.localPosition = _originalPos;
        }
    }
}