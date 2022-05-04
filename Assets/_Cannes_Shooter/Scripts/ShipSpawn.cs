using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cannes_Shooter
{
    public class ShipSpawn : MonoBehaviour
    {
        public Transform[] spawnPoints;
        private bool shipCanSpawn = true;

        private void Start()
        {
            shipCanSpawn = true;
        }

        public void spawnShip(ShipController ship)
        {
            if (shipCanSpawn != true) return;

            int _spawnIndex = Random.Range(0, spawnPoints.Length);
            Transform _randomSpawn = spawnPoints[_spawnIndex];

            Instantiate(ship, _randomSpawn);
        }

        //Transform _spawnLoc = GetSpawnPoint(ship.GetComponent<ShipController>().ship.location);
        //Transform GetSpawnPoint(Ship.shipSpawnLocation loc)
        //{
        //    switch (loc)
        //    {
        //        case Ship.shipSpawnLocation.Left_Front:
        //            return spawnPoints[0];
        //        case Ship.shipSpawnLocation.Left_Middle:
        //            return spawnPoints[1];
        //        case Ship.shipSpawnLocation.Left_Back:
        //            return spawnPoints[2];
        //        case Ship.shipSpawnLocation.Right_Front:
        //            return spawnPoints[3];
        //        case Ship.shipSpawnLocation.Right_Middle:
        //            return spawnPoints[4];
        //        case Ship.shipSpawnLocation.Right_Back:
        //            return spawnPoints[5];
        //        default:
        //            return spawnPoints[0];
        //    
        //}

    }
}
