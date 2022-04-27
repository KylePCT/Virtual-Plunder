using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cannes_Shooter
{
    public class ShipController : MonoBehaviour
    {
        public ScoreManager scoreManager;
        public Ship ship;

        private string shipName;
        private Ship.shipType shipType;
        private Ship.shipSpawnLocation shipLocation;
        private float damageOnHit;

        private float shipHealth;
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

            if (sphereCollider == null) sphereCollider = gameObject.AddComponent(typeof(SphereCollider)) as SphereCollider;
            sphereCollider.radius = 10f;
            sphereCollider.isTrigger = true;

            //StartCoroutine(destroyMe());
        }

        // Update is called once per frame
        void Update()
        {
            transform.Translate(0, 0, 3f * Time.deltaTime * shipSpeed);
        }

        ////Destroy object after set time.
        //public IEnumerator destroyMe()
        //{
        //    yield return new WaitForSeconds(21f);
        //    Destroy(gameObject);
        //}

        private void OnTriggerEnter(Collider col)
        {
            Debug.Log("Hit!" + gameObject.name);

            if (col.CompareTag("Coconut"))
            {
                if (shipHealth >= 20f)
                {
                    shipHealth = shipHealth - damageOnHit;
                    scoreManager.addPoints(10);
                }
                else
                {
                    scoreManager.addPoints(100);
                    scoreManager.addOntoMultiplier(1);
                    Destroy(gameObject);
                }
                Destroy(col);
            }
        }
    }
}
