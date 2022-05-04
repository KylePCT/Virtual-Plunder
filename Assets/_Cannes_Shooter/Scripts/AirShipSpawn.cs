using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cannes_Shooter
{
    public class AirShipSpawn : MonoBehaviour
    {
        public Transform[] spawnPoints;
        private bool shipCanSpawn = true;

        private void Start()
        {
            shipCanSpawn = true;
        }

        public void spawnAirShip(ShipController ship)
        {
            if (shipCanSpawn != true) return;

            int _spawnIndex = Random.Range(0, spawnPoints.Length);
            Transform _randomSpawn = spawnPoints[_spawnIndex];

            GameObject _airship = Instantiate(ship.gameObject, _randomSpawn.position, _randomSpawn.rotation, null);
            StartCoroutine(destroyMe(_airship, 15));
        }

        private IEnumerator destroyMe(GameObject ship, int seconds)
        {
            yield return new WaitForSeconds(seconds - 1);

            ship.GetComponentInChildren<ParticleSystem>().Stop();

            yield return new WaitForSeconds(seconds);
            Destroy(ship);
        }
    }
}
