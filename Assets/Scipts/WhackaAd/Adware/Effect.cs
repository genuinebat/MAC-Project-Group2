using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WhackaAd
{
    public class Effect
    {
        public bool Active;

        public Effect(bool _active)
        {
            Active = _active;
        }

        public virtual void Update()
        {
            if (!Active) return;
        }

        public virtual void FixedUpdate()
        {
            if (!Active) return;
        }
    }
}