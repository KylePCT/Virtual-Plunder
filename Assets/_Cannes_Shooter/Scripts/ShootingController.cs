using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cannes_Shooter
{
    public class ShootingController : MonoBehaviour
    {
        private ScoreManager scoreManager;
        private CameraController camController;

        [Header("Physical Parameters")]
        public Transform pointToShootFrom;
        public Transform gunObject;
        public GameObject objToShoot; //Can easily be changed to an array to support multiple prefabs.

        [Header("Gun Properties")]
        public float firingCooldown = 1.0f;
        public float firingPower = 200f;
        public float firingRange = 200f; //Distance for hit.
        [HideInInspector] public GameObject raycastHitObj;
        public bool canFire = true;

        [Header("Cannon Visuals")]
        public ParticleSystem firingCannon;

        void Start()
        {
            scoreManager = FindObjectOfType<ScoreManager>();
            camController = FindObjectOfType<CameraController>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!canFire) return;

                Shoot();
                camController.resetCamPos();
                StartCoroutine(startCooldown());
            }

            //Just so you can't spam life away.
            if (firingCooldown < .51f) firingCooldown = .5f;
        }

        public void Shoot()
        {
            Debug.Log("Shooting.");

            RaycastHit hit;
            if (Physics.Raycast(pointToShootFrom.transform.position, pointToShootFrom.transform.forward, out hit, firingRange))
            {
                Debug.DrawRay(pointToShootFrom.transform.position, pointToShootFrom.transform.forward * firingRange, Color.green, 10f);
                Debug.Log(hit.transform.name);

                GameObject _cannonball;
                _cannonball = Instantiate(objToShoot, pointToShootFrom.transform.position, pointToShootFrom.transform.rotation, null);
                firingCannon.Play();
                StartCoroutine(camController.cameraShake(.3f, .3f));
            }
            //If the raycast hits nothing.
            else
            {
                scoreManager.setMultiplierTo(1);
                Debug.Log("Missed!");
            }
        }

        private IEnumerator startCooldown()
        {
            canFire = false;
            yield return new WaitForSeconds(firingCooldown);
            canFire = true;
        }
    }
}
