using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cannes_Shooter
{
    public class ShipController : MonoBehaviour
    {
        private ScoreManager scoreManager;
        private LootboxController lootboxController;
        public Ship ship;
        public GameObject[] lootDrop;

        private string shipName;
        private Ship.shipType shipType;
        private Ship.shipSpawnLocation shipLocation;
        private float damageOnHit;

        [SerializeField] private float shipHealth;
        private float shipSpeed;

        private MeshFilter meshFilter;
        private MeshRenderer meshRenderer;
        private SphereCollider sphereCollider;

        [Header("Ship Visuals")]
        public ParticleSystem shipExplosion;

        // Start is called before the first frame update
        void Start()
        {
            scoreManager = FindObjectOfType<ScoreManager>();

            //Get the ScriptableObj data.
            shipName = ship.name;
            shipType = ship.type;
            shipHealth = ship.health;
            shipSpeed = Random.Range(ship.minSpeed, ship.maxSpeed);
            shipLocation = ship.location;
            damageOnHit = ship.damageOnHit;

            //Get the model and material data.
            if (GetComponent<MeshFilter>() != null) meshFilter = GetComponent<MeshFilter>();
            if (GetComponent<MeshRenderer>() != null) meshRenderer = GetComponent<MeshRenderer>();

            //Incase the sphereCollider isn't active.
            if (GetComponent<SphereCollider>() != null) sphereCollider = GetComponent<SphereCollider>();
            else
            {
                sphereCollider = this.gameObject.AddComponent<SphereCollider>();
                sphereCollider = GetComponent<SphereCollider>();
                sphereCollider.radius = 3f;
                sphereCollider.isTrigger = true;
            }

        }

        // Update is called once per frame
        void Update()
        {
            transform.Translate(0, 0, 3f * Time.deltaTime * shipSpeed);
        }

        public void shipIsHit()
        {
            StartCoroutine(delayBeforePoints(1));
        }

        private IEnumerator delayBeforePoints(int seconds)
        {
            yield return new WaitForSeconds(seconds);

            Debug.Log("Hit!" + gameObject.name);

            shipExplosion.Play();

            if (shipHealth > damageOnHit)
            {
                shipHealth = shipHealth - damageOnHit;
                scoreManager.addPoints(10);
            }
            else
            {
                scoreManager.addPoints(100);
                scoreManager.addOntoMultiplier(1);
                spawnCrate();
                LeanTween.scale(gameObject, Vector3.zero, 2f);
                StartCoroutine(destroyAfterSeconds(2));
            }
        }

        private void spawnCrate()
        {
            Instantiate(lootDrop[Random.Range(0, lootDrop.Length)], this.transform.position, this.transform.rotation, null);
        }

        private IEnumerator destroyAfterSeconds(int seconds)
        {
            yield return new WaitForSeconds(seconds);
            Destroy(gameObject);
        }

        //Destroy the cannonball if it does hit the ship.
        private void OnTriggerEnter(Collider col)
        {
            if (col.CompareTag("Coconut"))
            {
                Destroy(col);
            }
        }
    }
}
