using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Cannes_Shooter
{
    [CreateAssetMenu(fileName = "New Ship", menuName = "Cannes_Shooter/New Ship")]
    public class Ship : ScriptableObject
    {
        public new string name = "New Ship";
        public enum shipType { onWater, inAir }
        public shipType type = shipType.onWater;
        public enum shipSpawnLocation { Left_Front, Left_Middle, Left_Back, Right_Front, Right_Middle, Right_Back }
        public shipSpawnLocation location = shipSpawnLocation.Left_Front;

        public float health = 100f;

        public float minSpeed = .1f;
        public float maxSpeed = 1f;

        [Header("Points")]
        public float pointsWhenHit = 10f;
        public float pointsWhenKilled = 100f;
        public float damageOnHit = 50f;
    }
} 