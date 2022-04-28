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

        // Start is called before the first frame update
        void Start()
        {
            scoreManager = FindObjectOfType<ScoreManager>();

            //Get the ScriptableObj data.
            shipName = ship.name;
            shipType = ship.type;
            shipHealth = ship.health;
            shipSpeed = ship.speed;
            shipLocation = ship.location;
            damageOnHit = ship.damageOnHit;

            //Get the model and material data.
            if (GetComponent<MeshFilter>() != null) meshFilter = GetComponent<MeshFilter>();
            if (GetComponent<MeshRenderer>() != null) meshRenderer = GetComponent<MeshRenderer>();
            if (GetComponent<SphereCollider>() != null) sphereCollider = GetComponent<SphereCollider>();

            if (meshFilter == null) meshFilter = gameObject.AddComponent(typeof(MeshFilter)) as MeshFilter;
            meshFilter.mesh = ship.model.gameObject.GetComponent<MeshFilter>().sharedMesh;

            if (meshRenderer == null) meshRenderer = gameObject.AddComponent(typeof(MeshRenderer)) as MeshRenderer;
            meshRenderer.material = ship.model.gameObject.GetComponent<MeshRenderer>().sharedMaterial;
        }

        // Update is called once per frame
        void Update()
        {
            transform.Translate(0, 0, 3f * Time.deltaTime * shipSpeed);
        }

        public void shipIsHit()
        {
            Debug.Log("Hit!" + gameObject.name);

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
                StartCoroutine(destroyAfterSeconds(2));
            }
        }

        private void spawnCrate()
        {
            Instantiate(lootDrop[Random.Range(0, lootDrop.Length)], this.transform.position, this.transform.rotation);
        }

        private IEnumerator destroyAfterSeconds(int seconds)
        {
            yield return new WaitForSeconds(seconds);
            Destroy(gameObject);
        }
    }
}
