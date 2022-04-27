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

            Transform spawnLoc = GetSpawnPoint(ship.GetComponent<ShipController>().ship.location);
            Instantiate(ship, spawnLoc);
        }

        Transform GetSpawnPoint(Ship.shipSpawnLocation loc)
        {
            switch (loc)
            {
                case Ship.shipSpawnLocation.Left:
                    return spawnPoints[0];
                case Ship.shipSpawnLocation.Right:
                    return spawnPoints[1];
                case Ship.shipSpawnLocation.Middle:
                    return spawnPoints[2];
                default:
                    return spawnPoints[0];
            }

        }

    }
}
