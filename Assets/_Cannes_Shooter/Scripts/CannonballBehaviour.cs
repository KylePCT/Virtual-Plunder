using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cannes_Shooter
{
    public class CannonballBehaviour : MonoBehaviour
    {
        public float speed = 100f;
        private ScoreManager scoreManager;

        private void Start()
        {
            scoreManager = FindObjectOfType<ScoreManager>();
        }

        void Update()
        {
            transform.position += transform.forward * speed * Time.deltaTime;
            StartCoroutine(destroyMe(3));
        }

        private IEnumerator destroyMe(int seconds)
        {
            yield return new WaitForSeconds(seconds);
            Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider col)
        {
            //If it hits the ships...
            if (col.gameObject.CompareTag("Ship"))
            {
                col.transform.gameObject.GetComponent<ShipController>().shipIsHit();
            }

            else if (col.gameObject.CompareTag("Lootbox"))
            {
                col.transform.gameObject.GetComponentInParent<LootboxController>().lootboxIsHit();
            }
            //If the ray hits something that isn't the ships.
            else
            {
                scoreManager.setMultiplierTo(1);
                Debug.Log("Missed!");
            }
        }
    }
}
