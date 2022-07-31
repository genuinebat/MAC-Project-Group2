using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WhackaAd
{
    public class BaseAdware : MonoBehaviour
    {
        public GameObject AdwarePrefab;

        public bool IsDestroyable { get; set; }
        public bool IsTeleport { get; set; }

        void Start()
        {
            IsDestroyable = true;
        }

        public void CloseAd()
        {
            if (IsDestroyable == true)
            {
                Destroy(gameObject);
            }
        }

        public void DuplicateEnemy()
        {
            GameObject dup = Instantiate(
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

            dup.transform.parent = transform.parent;
        }
    }
}
