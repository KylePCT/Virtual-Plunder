using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cannes_Shooter
{
    public class KrakenBehaviour : MonoBehaviour
    {
        public int health;
        public int damageOnHit;
        private BoxCollider boxCol;
        private ScoreManager score;
        public ParticleSystem bloodSplat;

        [Header("Movement")]
        private Vector3 startPos;
        public float movementSpeed = 1;
        public float movementXScale = 1;
        public float movementZScale = 1;

        // Start is called before the first frame update
        void Start()
        {
            boxCol = GetComponent<BoxCollider>();
            score = FindObjectOfType<ScoreManager>();
            startPos = transform.position;
        }

        // Update is called once per frame
        void Update()
        { 

        }

        private void OnTriggerEnter(Collider col)
        {
            if (col.gameObject.CompareTag("Coconut")) krakenIsHit();
        }

        public void krakenIsHit()
        {
            StartCoroutine(delayBeforePoints(1));
        }

        private IEnumerator delayBeforePoints(int seconds)
        {
            yield return new WaitForSeconds(seconds);

            Debug.Log("Hit!" + gameObject.name);

            if (health > damageOnHit)
            {
                health = health - damageOnHit;
                score.addPoints(100);
                bloodSplat.Play();
            }
            else
            {
                score.addPoints(5000);
                bloodSplat.Play();
                Destroy(this.gameObject);
            }
        }
    }
}
