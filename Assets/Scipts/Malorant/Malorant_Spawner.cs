using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Malorant
{
    public class Malorant_Spawner : MonoBehaviour
    {
        public Transform ImageTarget;

        //list to store enemy GamObjects
        public List<GameObject> Malwares = new List<GameObject>();

        public void SpawnMalwares()
        {
            GameObject adware = Instantiate(
                Malwares[0],
                ImageTarget.position +
                new Vector3(-4, 0, -1),
                Quaternion.identity  
            );
            GameObject ransomware = Instantiate(
                Malwares[1],
                ImageTarget.position +
                new Vector3(0, 0, -1),
                Quaternion.identity     
            );
            GameObject botware = Instantiate(
                Malwares[2],
                ImageTarget.position +
                new Vector3(4, 0, -1),
                Quaternion.identity      
            );
            GameObject trojan = Instantiate(
                Malwares[3],
                ImageTarget.position +
                new Vector3(4, 4, -1),
                Quaternion.identity         
            );

            adware.transform.parent = transform;
            ransomware.transform.parent = transform;
            botware.transform.parent = transform;
            trojan.transform.parent = transform;
        }
    }
}