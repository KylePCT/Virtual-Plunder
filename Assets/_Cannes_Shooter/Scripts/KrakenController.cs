using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KrakenController : MonoBehaviour
{
    public GameObject krakenBoss;
    private GameObject spawnedKrakenBoss;

    public void summonKrakenBoss(Transform position)
    {
        spawnedKrakenBoss = Instantiate(krakenBoss, position);
    }

    public void removeKrakenBoss()
    {
        Destroy(spawnedKrakenBoss);
    }
}
