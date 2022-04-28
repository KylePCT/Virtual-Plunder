using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cannes_Shooter
{
    [CreateAssetMenu(fileName = "New Droppable", menuName = "Cannes_Shooter/New Droppable")]
    public class Droppable : ScriptableObject
    {
        public string dropName = "New Droppable";
        public GameObject droppableModel;

        public int health = 10;
        public int pointsOnDestruction = 50;

        public enum dropEffect { DoublePoints, DoubleBalls, SlowMotion, ChainedCannonBalls, FireBalls, IceBalls }
        public dropEffect effect = dropEffect.DoublePoints;
    }
}