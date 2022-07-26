using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Malorant
{
    public class Adware : MonoBehaviour, IScannable, IDamageable
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

        public GameObject AdwarePrefab;
        
        public GameObject X;
        public float Speed;

        public bool Scanned { get; private set; }

        public float MoveRange;

        List<Quad> quads = new List<Quad>();

        Vector3 targetLocation;
        int quad;

        void Start()
        {
            X.SetActive(false);

            CreateQuads();
            quad = 0;

            targetLocation = transform.position;
        }

        void Update()
        {
            if (Vector3.Distance(transform.position, targetLocation) < 0.01f)
            {
                SetNewRandomTargetLocation();
            }
            else
            {
                MoveToTargetLocation();
            }
        }

        public void GetHit()
        {
            float minX = transform.position.x - 3;
            float maxX = transform.position.x + 3;
            float minY = transform.position.y - 3;
            float maxY = transform.position.y + 3;

            GameObject adware = Instantiate(
                AdwarePrefab,
                new Vector3(
                    Random.Range(minX, maxX),
                    Random.Range(minY, maxY),
                    transform.position.z
                ),
                Quaternion.Euler(0f, 180f, 0f)
            );

            adware.transform.parent = transform.parent;
            adware.tag = "ScannableEnemy";
        }

        public void Scan()
        {
            Scanned = true;

            gameObject.tag = "Enemy";
            X.SetActive(true);
        }

        void MoveToTargetLocation()
        {
            transform.position = 
                Vector3.MoveTowards(
                    transform.position,
                    targetLocation, 
                    Speed * Time.deltaTime
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

        void CreateQuads()
        {
            quads.Clear();

            Vector3 pos = transform.position;

            quads.Add(new Quad(
                pos.x,
                pos.x + MoveRange,
                pos.y,
                pos.y + MoveRange,
                pos.z
            ));
            quads.Add(new Quad(
                pos.x,
                pos.x + MoveRange,
                pos.y - MoveRange,
                pos.y,
                pos.z
            ));
            quads.Add(new Quad(
                pos.x - MoveRange,
                pos.x,
                pos.y - MoveRange,
                pos.y,
                pos.z
            ));
            quads.Add(new Quad(
                pos.x - MoveRange,
                pos.x,
                pos.y,
                pos.y + MoveRange,
                pos.z
            ));
        }
    }
}