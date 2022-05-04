using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnContact : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Ship") || collision.CompareTag("Coconut"))
        Destroy(collision.gameObject);
    }
}
