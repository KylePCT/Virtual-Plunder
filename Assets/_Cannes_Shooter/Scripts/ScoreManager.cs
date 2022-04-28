using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Cannes_Shooter
{
    public class ScoreManager : MonoBehaviour
    {
        public TextMeshProUGUI scoreText;
        public TextMeshProUGUI multiplierText;

        [Header("Parameters")]
        public float multiplierDecreasesAfterSeconds = 20f;

        [HideInInspector] public int scoreValue;
        [HideInInspector] public int multiplierValue;

        // Start is called before the first frame update
        void Start()
        {
            scoreValue = 0;
            multiplierValue = 1;

            scoreText.text = "Score: " + scoreValue;
            multiplierText.text = "x" + multiplierValue;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void addPoints(int amount)
        {
            scoreValue = scoreValue + (amount * multiplierValue);
            scoreText.text = "Score: " + scoreValue;
            Debug.Log("Adding " + amount + " points!");
        }

        public void addOntoMultiplier(int amount)
        {
            multiplierValue = multiplierValue + amount;
            multiplierText.text = "x" + multiplierValue;
            Debug.Log("Adding " + amount + " to multiplier! It is now " + multiplierValue + "!");
        }

        public void setMultiplierTo(int amount)
        {
            multiplierValue = amount;
            multiplierText.text = "x" + multiplierValue;
        }
    }
}
