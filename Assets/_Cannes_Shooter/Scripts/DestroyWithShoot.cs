using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cannes_Shooter
{
    public class DestroyWithShoot : MonoBehaviour
    {
        private void OnCollisionEnter(Collision col)
        {
            if (col.gameObject.CompareTag("Coconut"))
            {

            }
        }
    }
}
