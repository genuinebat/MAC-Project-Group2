using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

namespace Malorant
{
    public class FPSEnemy : MonoBehaviour
    {
        public Malware Type;
        public float MaxHealth;

        public float Health { get; private set; }

        // Start is called before the first frame update
        void Start()
        {
            Health = MaxHealth;
        }

        // Update is called once per frame
        void Update()
        {
            if (Health <= 0)
            {
                Destroy(gameObject);
            }

            transform.LookAt(Camera.main.transform.position);
            //transform.position(Camera.main.transform.position);
        }

        public void TakeDamage(bool correctWeapon)
        {
            Health -= correctWeapon ? 10 : 5;
        }
    }
}
