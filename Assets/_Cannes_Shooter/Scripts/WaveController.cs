using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cannes_Shooter
{
    public class WaveController : MonoBehaviour
    {
        public Wave[] waves;
        public Transform[] spawnPoints;

        private bool waveCanRun = true;
        [SerializeField] private Wave currentWave;


        private void Start()
        {
            waveCanRun = true;
        }

        public void runWave(int waveNumber)
        {
            if (waveCanRun != true) return;

            currentWave = waves[waveNumber];

            for (int i = 0; i < currentWave.numberToSpawn; i++)
            {
                int _enemyToSpawn = Random.Range(0, currentWave.enemiesInWave.Length);
                Transform spawnLoc = GetSpawnPoint(currentWave.enemiesInWave[_enemyToSpawn].gameObject.GetComponent<ShipController>().ship.location);
                Instantiate(currentWave.enemiesInWave[_enemyToSpawn], spawnLoc);
            }
        }

        public void stopWave()
        {
            waveCanRun = false;
        }

        Transform GetSpawnPoint(Ship.shipSpawnLocation loc)
        {
            switch(loc)
            {
                case Ship.shipSpawnLocation.Left_Front:
                    return spawnPoints[0];
                case Ship.shipSpawnLocation.Left_Middle:
                    return spawnPoints[1];
                case Ship.shipSpawnLocation.Left_Back:
                    return spawnPoints[2];
                case Ship.shipSpawnLocation.Right_Front:
                    return spawnPoints[3];
                case Ship.shipSpawnLocation.Right_Middle:
                    return spawnPoints[4];
                case Ship.shipSpawnLocation.Right_Back:
                    return spawnPoints[5];
                default:
                    return spawnPoints[0];
            }

        }

    }
}
