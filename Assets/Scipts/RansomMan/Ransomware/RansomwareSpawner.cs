using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

namespace RansomMan
{
    public class RansomwareSpawner : MonoBehaviour
    {
        [Header("Prefab")]
        public GameObject Ransomware;

        [Header("Reference Variables")]
        public NodeManager nm;

        public void SpawnRansomwares()
        {
            GameObject r1 = Instantiate(Ransomware, Vector3.zero, Quaternion.identity);
            GameObject r2 = Instantiate(Ransomware, Vector3.zero, Quaternion.identity);
            GameObject r3 = Instantiate(Ransomware, Vector3.zero, Quaternion.identity);
            GameObject r4 = Instantiate(Ransomware, Vector3.zero, Quaternion.identity);

            r1.transform.parent = transform;
            r2.transform.parent = transform;
            r3.transform.parent = transform;
            r4.transform.parent = transform;

            Ransomware r1Script = r1.GetComponent<Ransomware>();
            r1Script.nm = nm;
            r1Script.SetSpawnPosition(5, 4);

            Ransomware r2Script = r2.GetComponent<Ransomware>();
            r2Script.nm = nm;
            r2Script.SetSpawnPosition(17, 4);

            Ransomware r3Script = r3.GetComponent<Ransomware>();
            r3Script.nm = nm;
            r3Script.SetSpawnPosition(4, 20);

            Ransomware r4Script = r4.GetComponent<Ransomware>();
            r4Script.nm = nm;
            r4Script.SetSpawnPosition(18, 20);

            r1Script.GameStarted = true;
            r2Script.GameStarted = true;
            r3Script.GameStarted = true;
            r4Script.GameStarted = true;
            Debug.Log(r1Script.GameStarted);
            Debug.Log(r2Script.GameStarted);
            Debug.Log(r3Script.GameStarted);
            Debug.Log(r4Script.GameStarted);
        }
    }
}