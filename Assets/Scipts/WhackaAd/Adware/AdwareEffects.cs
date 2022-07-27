using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WhackaAd
{
    public class AdwareEffects : MonoBehaviour
    {
        [Header("Effects the adware will spawn with")]
        public bool Movement;
        public bool TeleportEffect;

        Effect movementEffect, teleportEffect;

        void Start()
        {
            movementEffect = new MovementEffect(Movement);
        }

        void Update()
        {
            movementEffect.Update();
        }

        void FixedUpdate()
        {
            movementEffect.FixedUpdate();
        }
    }
}