using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OKBSM
{
    public class Rotate : MonoBehaviour
    {
        public float RotateSpeed;

        void Update()
        {
            transform.Rotate(0, 180f * RotateSpeed * Time.deltaTime, 0);
        }
    }
}