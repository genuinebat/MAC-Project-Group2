using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Malorant
{
    public class Malorant_Spawner : MonoBehaviour
    {
        [Header("Malware Prefabs")]
        //list to store enemy GamObjects
        public List<GameObject> Malwares = new List<GameObject>();

        [Header("Reference Variables")]
        public Transform ImageTarget;

        // function that spawns the first set of malwares
        public void SpawnMalwares()
        {
            GameObject trojan = Instantiate(
                Malwares[0],
                ImageTarget.position + new Vector3(3, 2, -1),
                Quaternion.Euler(0f, 180f, 0f)
            );
            GameObject ransomware = Instantiate(
                Malwares[1],
                ImageTarget.position + new Vector3(0, 0, -1),
                Quaternion.Euler(0f, 180f, 0f)
            );

            trojan.transform.parent = transform;
            ransomware.transform.parent = transform;
        }

        // function that spawns the second set of malwares
        public void SpawnMalwares2()
        {
            GameObject botware = Instantiate(
                Malwares[2],
                ImageTarget.position + new Vector3(3, 0, -1),
                Quaternion.Euler(0f, 90f, 0f)
            );
            GameObject adware = Instantiate(
                Malwares[3],
                ImageTarget.position + new Vector3(-3, 0, -1),
                Quaternion.Euler(0f, 180f, 0f)
            );

            botware.transform.parent = transform;
            adware.transform.parent = transform;
        }

        // function to clear all of the malwares that are currently in the scene
        public void ResetMalorant()
        {
            foreach (Transform child in gameObject.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
    }
}
