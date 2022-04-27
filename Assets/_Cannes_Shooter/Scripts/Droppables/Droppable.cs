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

        public int droppableHealth = 10;
        public int pointsWhenDestroyed = 100;

    }
}