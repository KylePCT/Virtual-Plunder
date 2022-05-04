using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

namespace Cannes_Shooter
{
    public class MainMenu : MonoBehaviour
    {
        private bool allowCamMove;
        private ScoreManager score;
        private ShootingController shoot;

        [Header("Graphic Design")]
        public GameObject mainMenuGraphics_1;
        public GameObject mainMenuGraphics_2;
        public GameObject mainMenuGraphics_3;
        public GameObject titleText;
        public GameObject startButton;
        public GameObject highScores;

        public GameObject gameMenu;
        public GameObject endMenu;
        [HideInInspector] public int endScore;
        public TextMeshProUGUI endScoreText;

        [Header("Countdown")]
        public GameObject countdown_1;
        public GameObject countdown_2;
        public GameObject countdown_3;
        public GameObject countdown_Go;
        public GameObject timer;

        [Header("Player")]
        public GameObject cam;
        public Transform camTarget;
        public GameObject player;
        public GameObject playerFake;
        public PlayableDirector director;
        public GameObject cannonOnCam;

        [Header("Highscores")]
        public TextMeshProUGUI highscoreText;
        public GameObject newHighscore;
        private int highScore = 0;

        // Start is called before the first frame update
        void Start()
        {
            initGame();
        }

        private void initGame()
        {
            countdown_1.transform.localScale = Vector3.zero;
            countdown_2.transform.localScale = Vector3.zero;
            countdown_3.transform.localScale = Vector3.zero;
            countdown_Go.transform.localScale = Vector3.zero;
            gameMenu.GetComponent<CanvasGroup>().alpha = 0;
            cannonOnCam.SetActive(false);

            highScore = PlayerPrefs.GetInt("highScore");
            if (highScore <= 19999) highScore = 20000;
            Debug.Log(highScore);
            newHighscore.SetActive(false);
            highscoreText.text = highScore.ToString();

            score = FindObjectOfType<ScoreManager>();
        }

        private void LateUpdate()
        {
            if (allowCamMove)
            {
                //Lerp position
                cam.transform.position = Vector3.Lerp(cam.transform.position, camTarget.position, Time.deltaTime * 5f);
                cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, camTarget.rotation, Time.deltaTime * 5f);
            }
        }

        public void beginGame()
        {
            LeanTween.moveX(mainMenuGraphics_1, transform.localPosition.x + 5000f, 1f);
            LeanTween.moveX(highScores, transform.localPosition.x + 500f, 1f);
            LeanTween.moveX(mainMenuGraphics_2, transform.localPosition.x  + 8000f, 1f);
            LeanTween.moveX(mainMenuGraphics_3, transform.localPosition.x - 3000f, 1f);
            LeanTween.moveX(titleText, transform.localPosition.x + 5000f, 1f);
            LeanTween.moveX(startButton, transform.localPosition.x + 3000f, 1f);
            LeanTween.moveX(highScores, transform.localPosition.x - 3000f, 1f);

            showCountdown();
            StartCoroutine(gameStart(5));
            allowCamMove = true;
        }

        public void showCountdown()
        {
            LeanTween.scale(countdown_3, new Vector3(1, 1, 1), 1f).setDelay(.2f).setOnComplete(countdown_3To2);
            LeanTween.rotateZ(countdown_3, 360, 1f).setFrom(0);
        }

        private IEnumerator gameStart(int seconds)
        {
            yield return new WaitForSeconds(seconds);

            score.scoreValue = 0;
            score.multiplierValue = 1;
            timer.GetComponent<Timer>().timerIsRunning = true;
            timer.GetComponent<Timer>().timeRemaining = 120f;

            cannonOnCam.SetActive(true);
            playerFake.SetActive(false);
            director.Play();
            gameMenu.SetActive(true);

            shoot = FindObjectOfType<ShootingController>();
            shoot.canFire = true;

            LeanTween.alphaCanvas(gameMenu.GetComponent<CanvasGroup>(), 1f, .5f);
            cam.GetComponent<CameraController>().enabled = true;
        }

        public void restartGame()
        {
            LeanTween.moveY(endMenu, transform.position.y + 3000f, 1f);

            showCountdown();
            StartCoroutine(gameStart(5));
            allowCamMove = true;

            //Remove cursor.
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        public void resetGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void quitGame()
        {
            Application.Quit();
        }

        public void showEndGameUI()
        {
            cam.GetComponent<CameraController>().enabled = false;
            shoot.canFire = false;

            LeanTween.alphaCanvas(endMenu.GetComponent<CanvasGroup>(), 1f, .5f);
            LeanTween.moveY(endMenu, transform.position.y - 150f, 1f);

            endScore = score.scoreValue;
            endScoreText.text = endScore.ToString() + " Points!";

            if (endScore > highScore)
            {
                PlayerPrefs.SetInt("highScore", endScore);
                highscoreText.text = highScore.ToString();
                newHighscore.SetActive(true);
            }

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        private void countdown_3To2()
        {
            LeanTween.scale(countdown_3, Vector3.zero, .5f);
            LeanTween.rotateZ(countdown_3, 0, 1f).setFrom(0); 
            
            LeanTween.scale(countdown_2, new Vector3(1, 1, 1), 1f).setDelay(.2f).setOnComplete(countdown_2To1);
            LeanTween.rotateZ(countdown_2, 360, 1f).setFrom(0);
        }

        private void countdown_2To1()
        {
            LeanTween.scale(countdown_2, Vector3.zero, .5f);
            LeanTween.rotateZ(countdown_2, 0, 1f).setFrom(0);

            LeanTween.scale(countdown_1, new Vector3(1, 1, 1), 1f).setDelay(.2f).setOnComplete(countdown_1ToGo);
            LeanTween.rotateZ(countdown_1, 360, 1f).setFrom(0);
        }

        private void countdown_1ToGo()
        {
            LeanTween.scale(countdown_1, Vector3.zero, .5f);
            LeanTween.rotateZ(countdown_1, 0, 1f).setFrom(0);

            LeanTween.scale(countdown_Go, new Vector3(1, 1, 1), 1f).setDelay(.2f).setOnComplete(countdown_FadeGo);
            LeanTween.rotateZ(countdown_Go, 360, 1f).setFrom(0);
        }

        private void countdown_FadeGo()
        {
            LeanTween.scale(countdown_Go, Vector3.zero, .5f);
            LeanTween.rotateZ(countdown_Go, 0, 1f).setFrom(0);
            allowCamMove = false;
        }
    }
}
