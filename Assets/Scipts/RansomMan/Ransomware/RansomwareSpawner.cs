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

            r1Script.TimeOutLocation = nm.GetNodeWorldPosition(nm.grid.Get(9, 14));
            r2Script.TimeOutLocation = nm.GetNodeWorldPosition(nm.grid.Get(10, 14));
            r3Script.TimeOutLocation = nm.GetNodeWorldPosition(nm.grid.Get(12, 14));
            r4Script.TimeOutLocation = nm.GetNodeWorldPosition(nm.grid.Get(13, 14));

            r1Script.Active = true;
            r2Script.Active = true;
            r3Script.Active = true;
            r4Script.Active = true;
        }
    }
}