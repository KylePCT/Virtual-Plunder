using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cannes_Shooter
{
    [CreateAssetMenu(fileName = "New Ship", menuName = "Cannes_Shooter/New Ship")]
    public class Ship : ScriptableObject
    {
        public new string name = "New Ship";
        public GameObject model;
        public enum shipType { onWater, inAir }
        public shipType type = shipType.onWater;
        public enum shipSpawnLocation { Left, Middle, Right }
        public shipSpawnLocation location = shipSpawnLocation.Left;

        public float health = 100f;
        public float speed = 1f;

        [Header("Points")]
        public float pointsWhenHit = 10f;
        public float pointsWhenKilled = 100f;
        public float damageOnHit = 50f;
    }
}