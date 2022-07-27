using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WhackaAd
{
    public class WhackaAdSpawner : MonoBehaviour
    {
        [Header("Adware Prefabs")]
        public GameObject Adware;

        [Header("Reference Variables")]
        public Transform ImageTarget;

        [Header("Function Variables")]
        public float SpawnTime;
        public float SpawnRadius;

        float elap, minX, maxX, minY, maxY;

        void Start()
        {
            SetBoundaries();
            elap = 0f;
        }

        void Update()
        {
            if (elap >= SpawnTime)
            {
                SpawnAdware();
                elap = 0f;
            }
            else elap += Time.deltaTime;
        }

        void SetBoundaries()
        {
            // set x and y boundaries based on image target
        }

        void SpawnAdware()
        {
            adware = Instantiate(
                Adware,
                new Vector3(
                    Random.Range(minX, maxX),
                    Random.Range(minY, maxY),
                    ImageTarget.position.z - ImageTarget.forward
                ),
                Quaternion.Euler(0f, 0f, 0f)
            );

            // randomize the effects using random.range(0, 1)
        }
    }
}