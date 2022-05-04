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
                StartCoroutine(startCooldown());
            }
        }

        public void Shoot()
        {
            Debug.Log("Shooting.");

            RaycastHit hit;
            if (Physics.Raycast(pointToShootFrom.transform.position, pointToShootFrom.transform.forward, out hit, firingRange))
            {
                Debug.DrawRay(pointToShootFrom.transform.position, pointToShootFrom.transform.forward * firingRange, Color.green, 10f);
                Debug.Log(hit.transform.name);

                //If it hits the ships...
                if (hit.transform.tag == "Ship")
                {
                    hit.transform.gameObject.GetComponent<ShipController>().shipIsHit();
                }
                else if (hit.transform.tag == "Lootbox")
                {
                    hit.transform.gameObject.GetComponentInParent<LootboxController>().lootboxIsHit();
                }
                //If the ray hits something that isn't the ships.
                else
                {
                    scoreManager.setMultiplierTo(1);
                    Debug.Log("Missed!");
                }
            }
            //If the raycast hits nothing.
            else
            {
                scoreManager.setMultiplierTo(1);
                Debug.Log("Missed!");
            }

            GameObject _cannonball;
            _cannonball = Instantiate(objToShoot, pointToShootFrom.transform.position, pointToShootFrom.transform.rotation);
            firingCannon.Play();
            StartCoroutine(camController.cameraShake(.3f, .3f));
        }

        private IEnumerator startCooldown()
        {
            canFire = false;
            yield return new WaitForSeconds(firingCooldown);
            canFire = true;
        }
    }
}
