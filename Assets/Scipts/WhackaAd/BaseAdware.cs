using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WhackaAd
{
    public class BaseAdware : MonoBehaviour
    {
        public GameObject AdwarePrefab;

        [HideInInspector]
        public bool IsDestroyable { get; set; }

        void Start()
        {
            IsDestroyable = true;
        }

        public void CloseAd()
        {
            if (IsDestroyable == true)
            {
                Debug.Log("des");
                Destroy(gameObject);
            }
        }

        public void DuplicateEnemy()
        {
            Instantiate(
                AdwarePrefab,
                new Vector3(
                    gameObject.transform.position.x,
                    gameObject.transform.position.y,
                    gameObject.transform.position.z
                ),
                Quaternion.identity
            );
            transform.position = new Vector3(
                gameObject.transform.position.x + .01f,
                gameObject.transform.position.y - .01f,
                gameObject.transform.position.z - .01f
            );
        }
    }
}
