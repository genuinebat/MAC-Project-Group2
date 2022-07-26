using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WhackaAd
{
    public class WhackaAdSpawner : MonoBehaviour
    {
        [Header("Adware Prefabs")]
        public List<GameObject> Adwares = new List<GameObject>();

        [Header("Reference Variables")]
        public Transform ImageTarget;
    }
}