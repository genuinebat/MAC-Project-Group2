using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WhackaAd
{
    public class TeleportEffect : Effect
    {
        public bool CanTele { get; private set; }

        GameObject adware;
        float cooldown, range, elap;

        public TeleportEffect(GameObject _adware, float _cooldown, float _range)
        {
            adware = _adware;
            cooldown = _cooldown;
            range = _range;
        }

        public override void Init()
        {
            elap = cooldown;
        }

        public override void Update()
        {
            base.Update();

            CanTele = elap < cooldown ? false : true;
            elap += Time.deltaTime;
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
        }

        public void Teleport()
        {
            if (!CanTele) return;

            float minX = adware.transform.position.x - 1;
            float maxX = adware.transform.position.x + 1;
            float minY = adware.transform.position.y - 1;
            float maxY = adware.transform.position.y + 1;

            Vector3 dir = new Vector3(
                Random.Range(minX, maxX),
                Random.Range(minY, maxY),
                adware.transform.position.z
            );

            dir.Normalize();

            Vector3 pos = adware.transform.position + (dir * range);

            adware.transform.position = pos;

            elap = 0f;
        }
    }
}