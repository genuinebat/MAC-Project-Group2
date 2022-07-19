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
            GameObject trojan = Instantiate(
                Malwares[0],
                ImageTarget.position +
                new Vector3(-3, 0, -1),
                Quaternion.Euler(0f, 180f, 0f)      
            );
            GameObject ransomware = Instantiate(
                Malwares[1],
                ImageTarget.position +
                new Vector3(0, 0, -1),
                Quaternion.identity     
            );
            //GameObject botware = Instantiate(
            //    Malwares[2],
            //    ImageTarget.position +
            //    new Vector3(3, 0, -1),
            //    Quaternion.Euler(0f, 90f, 0f)      
            //);
            //GameObject trojan = Instantiate(
            //    Malwares[3],
            //    ImageTarget.position +
            //    new Vector3(3, 3, -1),
            //    Quaternion.Euler(0f, 180f, 0f)          
            //);

            trojan.transform.parent = transform;
            ransomware.transform.parent = transform;
            //botware.transform.parent = transform;
            //adware.transform.parent = transform;
        }

        public void SpawnMalwares2()
        {
            GameObject botware = Instantiate(
                Malwares[2],
                ImageTarget.position +
                new Vector3(3, 0, -1),
                Quaternion.Euler(0f, 90f, 0f)
            );
            GameObject adware = Instantiate(
                Malwares[3],
                ImageTarget.position +
                new Vector3(3, 3, -1),
                Quaternion.Euler(0f, 180f, 0f)
            );

            botware.transform.parent = transform;
            adware.transform.parent = transform;
        }
    }
}