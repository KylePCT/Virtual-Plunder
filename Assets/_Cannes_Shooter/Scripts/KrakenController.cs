using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KrakenController : MonoBehaviour
{
    public GameObject tentacle;
    public GameObject krakenBoss;

    private GameObject spawnedTentacle;
    private GameObject spawnedKrakenBoss;

    public void summonTentacle(Transform position)
    {
        spawnedTentacle = Instantiate(tentacle, position);
    }

    public void removeTentacle()
    {
        Destroy(spawnedTentacle);
    }

    public void summonKrakenBoss(Transform position)
    {
        spawnedKrakenBoss = Instantiate(krakenBoss, position);
    }

    public void removeKrakenBoss()
    {
        Destroy(spawnedKrakenBoss);
    }
}
