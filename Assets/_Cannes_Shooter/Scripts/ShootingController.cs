using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cannes_Shooter
{
    /*
     * Temporary bit of code to substitute for the hardware shooting.
     * - Kyle Tugwell 27/04/2022
     */

    public class ShootingController : MonoBehaviour
    {
        public Transform pointToShootFrom;
        public Transform gunObject;
        public GameObject objToShoot; //Can easily be changed to an array to support multiple prefabs.
        public float firingCooldown = 1.0f;
        public float firingPower = 20f;
        private bool canFire = true;

        // Start is called before the first frame update
        void Start()
        {

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

            GameObject _cannonball;
            _cannonball = Instantiate(objToShoot, pointToShootFrom.transform.position, pointToShootFrom.transform.rotation);
            _cannonball.GetComponent<Rigidbody>().AddForce(transform.forward * firingPower, ForceMode.Impulse);
        }

        public IEnumerator startCooldown()
        {
            canFire = false;
            yield return new WaitForSeconds(firingCooldown);
            canFire = true;
        }
    }
}
