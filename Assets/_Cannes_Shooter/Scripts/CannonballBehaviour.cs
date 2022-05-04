using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cannes_Shooter
{
    public class CannonballBehaviour : MonoBehaviour
    {
        public float speed = 100f;

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
    }
}
