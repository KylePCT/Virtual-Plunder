using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cannes_Shooter
{
    public class DamageParticles : MonoBehaviour
    {
        public ParticleSystem particlesToPlayWhenHit;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnCollisionEnter(Collision col)
        {
            Debug.Log(col.gameObject.tag);
            if (col.gameObject.CompareTag("Coconut"))
            {
                Instantiate(particlesToPlayWhenHit, col.transform.position, col.transform.rotation, null);
                StartCoroutine(destroyCol(1, col.gameObject));
            }
        }

        private IEnumerator destroyCol(int seconds, GameObject col)
        {
            yield return new WaitForSeconds(seconds);
            Destroy(col);
        }
    }
}