using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WhackaAd
{
    public class MovementEffect : Effect
    {
        struct Quad
        {
            public float MinX, MaxX, MinY, MaxY, Z;
            public Quad(float _minX, float _maxX, float _minY, float _maxY, float _z)
            {
                MinX = _minX;
                MaxX = _maxX;
                MinY = _minY;
                MaxY = _maxY;
                Z = _z;
            }
        }

        GameObject adware;
        List<Quad> quads = new List<Quad>();
        Vector3 targetLocation;
        float speed, moveRange;
        int quad;

        public MovementEffect(bool active, GameObject _adware, float _speed, float _moveRange) : base (active)
        {
            adware = _adware;
            speed = _speed;
            moveRange = _moveRange;
        }

        public override void Init()
        {
            CreateQuads();
            quad = 0;

            targetLocation = adware.transform.position;
        }

        public override void Update()
        {
            base.Update();

            if (Vector3.Distance(adware.transform.position, targetLocation) < 0.01f)
            {
                SetNewRandomTargetLocation();
            }
            else
            {
                MoveToTargetLocation();
            }
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
        }

        void CreateQuads()
        {
            quads.Clear();

            Vector3 pos = adware.transform.position;

            quads.Add(new Quad(
                pos.x,
                pos.x + moveRange,
                pos.y,
                pos.y + moveRange,
                pos.z
            ));
            quads.Add(new Quad(
                pos.x,
                pos.x + moveRange,
                pos.y - moveRange,
                pos.y,
                pos.z
            ));
            quads.Add(new Quad(
                pos.x - moveRange,
                pos.x,
                pos.y - moveRange,
                pos.y,
                pos.z
            ));
            quads.Add(new Quad(
                pos.x - moveRange,
                pos.x,
                pos.y,
                pos.y + moveRange,
                pos.z
            ));
        }

        void MoveToTargetLocation()
        {
            adware.transform.position = 
                Vector3.MoveTowards(
                    adware.transform.position,
                    targetLocation, 
                    speed * Time.deltaTime
                );
        }

        void SetNewRandomTargetLocation()
        {
            targetLocation =
                new Vector3(
                    Random.Range(quads[quad].MinX, quads[quad].MaxX),
                    Random.Range(quads[quad].MinY, quads[quad].MaxY),
                    quads[quad].Z
                );
            
            if (quad < 3) quad++;
            else quad = 0;
        }
    }
}