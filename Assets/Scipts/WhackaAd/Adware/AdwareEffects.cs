using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WhackaAd
{
    public class AdwareEffects : MonoBehaviour
    {
        [Header("Active effects the adware will spawn with")]
        public bool Movement;

        public bool Teleport;

        public float Speed;

        public float MoveRange;

        public float TeleportCooldown;

        public float TeleportRange;

        MovementEffect movementEffect;

        TeleportEffect teleportEffect;

        void Start()
        {
            movementEffect = new MovementEffect(gameObject, Speed, MoveRange);
            teleportEffect =
                new TeleportEffect(gameObject, TeleportCooldown, TeleportRange);

            movementEffect.Init();
            teleportEffect.Init();
        }

        void Update()
        {
            if (Movement) movementEffect.Update();
            if (Teleport) teleportEffect.Update();
        }

        void FixedUpdate()
        {
            if (Movement) movementEffect.FixedUpdate();
            if (Teleport) teleportEffect.FixedUpdate();
        }

        public void CheckTeleport(Transform CurrentLocation)
        {
            if (teleportEffect.CanTele)
            {
                teleportEffect.Teleport (CurrentLocation);
            }
            else
            {
                GetComponent<BaseAdware>().CloseAd();
            }
        }
    }
}
