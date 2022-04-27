using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cannes_Shooter
{
    [CreateAssetMenu(fileName = "New Wave", menuName = "Cannes_Shooter/New Wave")]
    public class Wave : ScriptableObject
    {
        public GameObject[] enemiesInWave;
        public float numberToSpawn;
    }
}
