using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cannes_Shooter
{
    public class ShotItemController : MonoBehaviour
    {
        public float destroyAfterSeconds = 2f;

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(destroyMe());
        }

        //Destroy object after set time.
        public IEnumerator destroyMe()
        {
            yield return new WaitForSeconds(destroyAfterSeconds);
            Destroy(gameObject);
        }
    }
}
