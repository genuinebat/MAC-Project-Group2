using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WhackaAd
{
    public class WhackaAdSpawner : MonoBehaviour
    {
        [Header("Adware Prefabs")]
        public List<GameObject> Adwares = new List<GameObject>();

        [Header("Reference Variables")]
        public Transform ImageTarget;
        public GameObject SpawnParticles;

        [Header("Function Variables")]
        public float SpawnTime;
        public float SpawnRadius;

        [HideInInspector]
        public List<GameObject> AdwareTemp = new List<GameObject>();

        public bool GameStarted { get; set; }

        float elap, minX, maxX, minY, maxY;

        void Start()
        {
            AdwareTemp.AddRange(Adwares);

            SetBoundaries();
            elap = Mathf.Infinity;

            GameStarted = false;
        }

        void Update()
        {
            if (!GameStarted)
                return;

            if (elap >= SpawnTime)
            {
                SpawnAdware();
                elap = 0f;
            }
            else
                elap += Time.deltaTime;
        }

        void SetBoundaries()
        {
            // set x and y boundaries based on image target
            minX = ImageTarget.position.x - SpawnRadius;
            maxX = ImageTarget.position.x + SpawnRadius;
            minY = ImageTarget.position.y - SpawnRadius;
            maxY = ImageTarget.position.y + SpawnRadius;
        }

        void SpawnAdware()
        {
            GameObject adware = Instantiate(
                AdwareTemp[Random.Range(0, AdwareTemp.Count)],
                new Vector3(
                    Random.Range(minX, maxX),
                    Random.Range(minY, maxY),
                    ImageTarget.position.z - 1
                ),
                Quaternion.Euler(0f, 0f, 0f)
            );
            // Instantiate(SpawnParticles, new Vector3(adware.transform.position.x, adware.transform.position.y, (adware.transform.position.z - 1)), Quaternion.identity);

            adware.transform.parent = transform;

            AdwareEffects effectScript = adware.GetComponent<AdwareEffects>();

            effectScript.Speed = Random.Range(0.5f, 1f);
            effectScript.MoveRange = Random.Range(1f, 3f);
            effectScript.TeleportCooldown = Random.Range(2, 5);
            effectScript.TeleportRange = Random.Range(2f, 4f);
        }
    }
}
