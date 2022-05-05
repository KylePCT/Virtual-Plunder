using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cannes_Shooter
{
    public class LootboxController : MonoBehaviour
    {
        private ScoreManager scoreManager;
        private ShootingController shootController;
        public Droppable droppable;

        private string boxName;
        private string dropEffect;
        private int boxHealth;
        private int boxPoints;

        private bool powerupAvailable = true;
        private int powerupCooldownDuration = 5;

        // Start is called before the first frame update
        void Start()
        {
            scoreManager = FindObjectOfType<ScoreManager>();
            shootController = FindObjectOfType<ShootingController>();

            //Get the ScriptableObj data.
            boxName = droppable.name;
            dropEffect = droppable.effect.ToString();
            boxHealth = droppable.health;
            boxPoints = droppable.pointsOnDestruction;

            StartCoroutine(lootboxDespawns(8));

            this.gameObject.transform.localScale = Vector3.zero;
            LeanTween.scale(this.gameObject, new Vector3(2, 2, 2), 1);
        }

        // Update is called once per frame
        void Update()
        {
            transform.Rotate(.1f, .1f, .1f * Time.fixedDeltaTime);
        }

        public void lootboxIsHit()
        {
            StartCoroutine(delayBeforePoints(1));
        }

        private IEnumerator delayBeforePoints(int seconds)
        {
            yield return new WaitForSeconds(seconds);

            Debug.Log("Hit!" + gameObject.name);

            scoreManager.addPoints(boxPoints);
            spawnLoot();
            gameObject.GetComponentInParent<Transform>().localScale = Vector3.zero; //Minimise it until destroyed.
        }

        public void spawnLoot()
        {
            GameObject _drop;
            _drop = Instantiate(droppable.droppableModel.gameObject, this.transform.position, this.transform.rotation);
            //_drop.AddComponent<Rigidbody>();
            //_drop.GetComponent<Rigidbody>().AddForce(Vector3.up * 500f);
            StartCoroutine(destroyDropVisual(_drop));
            
            activateDropEffect();
        }

        private IEnumerator lootboxDespawns(int seconds)
        {
            yield return new WaitForSeconds(seconds);
            LeanTween.scale(this.gameObject, new Vector3(0, 0, 0), .1f).setOnComplete(destroyMe);
        }

        private void destroyMe()
        {
            Destroy(gameObject);
        }

        private IEnumerator destroyDropVisual(GameObject drop)
        {
            yield return new WaitForSeconds(2f);
            Destroy(drop);
        }

        private void activateDropEffect()
        {
            if (powerupAvailable == false) return;

            StartCoroutine(powerupCooldown(powerupCooldownDuration));

            if (dropEffect == "DoublePoints")
            {
                scoreManager.setMultiplierTo(scoreManager.multiplierValue * 2);
                powerupAvailable = false;
            }
            else if (dropEffect == "DoubleBalls")
            { 
                shootController.firingCooldown = shootController.firingCooldown / 2;
                powerupAvailable = false;
            }
            else
            {
                Debug.LogError("You broke the power ups activating.");
            }
        }

        private void deactivateDropEffect()
        {
            Debug.Log("Deactivating...");

            if (dropEffect == "DoublePoints")
            {
                if(scoreManager.multiplierValue != 1) scoreManager.setMultiplierTo(Mathf.FloorToInt(scoreManager.multiplierValue / 2));
            }
            else if (dropEffect == "DoubleBalls")
            {
                shootController.firingCooldown = shootController.firingCooldown * 2;
            }
            else
            {
                Debug.LogError("You broke the power ups deactivating.");
            }
        }

        private IEnumerator powerupCooldown(int seconds)
        {
            powerupAvailable = false;
            Debug.Log("Powerups disabled while one is active.");

            yield return new WaitForSeconds(seconds);

            deactivateDropEffect();
            Debug.Log("Powerups available.");
            powerupAvailable = true;

            Destroy(gameObject);
        }
    }
}
