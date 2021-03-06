using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Cannes_Shooter
{
    public class Timer : MonoBehaviour
    {
        public float timeRemaining = 120f;
        public bool timerIsRunning = false;
        public TextMeshProUGUI timeText;
        public GameObject endgameUI;
        private MainMenu menu;

        private void Start()
        {
            timerIsRunning = false;
            menu = FindObjectOfType<MainMenu>();
        }

        void Update()
        {
            if (timerIsRunning)
            {
                if (timeRemaining > 0)
                {
                    timeRemaining -= Time.deltaTime;
                    DisplayTime(timeRemaining);
                }
                else
                {
                    Debug.Log("Time has run out!");
                    timeRemaining = 0;
                    timerIsRunning = false;
                    menu.showEndGameUI();
                }
            }
        }
        void DisplayTime(float timeToDisplay)
        {
            timeToDisplay += 1;
            float minutes = Mathf.FloorToInt(timeToDisplay / 60);
            float seconds = Mathf.FloorToInt(timeToDisplay % 60);
            timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}