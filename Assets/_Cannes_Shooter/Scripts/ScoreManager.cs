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

        private int scoreValue;
        private int multiplierValue;

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
            StartCoroutine(reduceMultiplier());
        }

        public void addPoints(int amount)
        {
            Debug.Log("Adding " + amount + " points!");
            scoreValue = scoreValue + (amount * multiplierValue);
            scoreText.text = "Score: " + scoreValue;
        }

        public void addOntoMultiplier(int amount)
        {
            multiplierValue = multiplierValue + amount;
            multiplierText.text = "x" + multiplierValue;
        }

        public IEnumerator reduceMultiplier()
        {
            yield return new WaitForSeconds(multiplierDecreasesAfterSeconds);
            
            if (multiplierValue != 1)
            {
                multiplierValue = multiplierValue - 1;
                StartCoroutine(reduceMultiplier());
            }
        }
    }
}
