using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace WhackaAd
{
    public class AdwareEffects : MonoBehaviour
    {
        [Header("Active effects the adware will spawn with")]
        public bool Movement;
        public bool Teleport;

        [HideInInspector]
        public float Speed;
        [HideInInspector]
        public float MoveRange;
        [HideInInspector]
        public float TeleportCooldown;
        [HideInInspector]
        public float TeleportRange;

        MovementEffect movementEffect;
        TeleportEffect teleportEffect;

        void Start()
        {
            movementEffect = new MovementEffect(Movement ,gameObject, Speed, MoveRange);
            teleportEffect = new TeleportEffect(Teleport, gameObject, TeleportCooldown, TeleportRange);

            movementEffect.Init();
            teleportEffect.Init();
        }

        void Update()
        {
            movementEffect.Update();
            teleportEffect.Update();
        }

        void FixedUpdate()
        {
            movementEffect.FixedUpdate();
            teleportEffect.FixedUpdate();
        }

        public void CheckTeleport()
        {
            if (teleportEffect.CanTele)
            {
                teleportEffect.Teleport();
            }
            else
            {
                GetComponent<BaseAdware>().CloseAd();
            }
        }
    }

    [CustomEditor(typeof(AdwareEffects))]
    public class AdwareEffectsEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            AdwareEffects script = (AdwareEffects) target;

            DrawDefaultInspector();

            if (script.Movement)
            {
                EditorGUILayout.Space();

                EditorGUILayout.LabelField("Movement Values", EditorStyles.boldLabel);

                script.Speed = EditorGUILayout.FloatField("Speed", script.Speed);

                script.MoveRange = EditorGUILayout.FloatField("Move Range", script.MoveRange);
            }

            if (script.Teleport)
            {
                EditorGUILayout.Space();

                EditorGUILayout.LabelField("Teleport Values", EditorStyles.boldLabel);

                script.TeleportCooldown = EditorGUILayout.FloatField("Teleport Cooldown", script.TeleportCooldown);

                script.TeleportRange = EditorGUILayout.FloatField("Teleport Range", script.TeleportRange);
            }
        }
    }
}