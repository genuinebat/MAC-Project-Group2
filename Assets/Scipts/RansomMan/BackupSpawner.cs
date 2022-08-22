using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

namespace RansomMan
{
    public class BackupSpawner : MonoBehaviour
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

        struct BackupObject
        {
            public int Q;
            public GameObject BackupObj;
            public BackupObject(int _q, GameObject _obj)
            {
                Q = _q;
                BackupObj = _obj;
            }
        }

        [Header("Prefab")]
        public GameObject Backup;

        [HideInInspector]
        public NodeManager nm;
        [HideInInspector]
        BackupObject[] backupObjects = new BackupObject[4];

        List<Quad> quads = new List<Quad>();

        public void CreateQuads()
        {
            Vector3 q1min = nm.GetNodeWorldPosition(nm.grid.Get(1, 1));
            Vector3 q1max = nm.GetNodeWorldPosition(nm.grid.Get(9, 8));
            Quad q1 = new Quad(
                q1min.x, q1max.x, 
                q1min.y, q1max.y,
                q1min.z
            );

            Vector3 q2min = nm.GetNodeWorldPosition(nm.grid.Get(13, 1));
            Vector3 q2max = nm.GetNodeWorldPosition(nm.grid.Get(21, 8));
            Quad q2 = new Quad(
                q2min.x, q2max.x, 
                q2min.y, q2max.y,
                q2min.z
            );

            Vector3 q3min = nm.GetNodeWorldPosition(nm.grid.Get(1, 17));
            Vector3 q3max = nm.GetNodeWorldPosition(nm.grid.Get(9, 23));
            Quad q3 = new Quad(
                q3min.x, q3max.x, 
                q3min.y, q3max.y,
                q3min.z
            );

            Vector3 q4min = nm.GetNodeWorldPosition(nm.grid.Get(13, 17));
            Vector3 q4max = nm.GetNodeWorldPosition(nm.grid.Get(21, 23));
            Quad q4 = new Quad(
                q4min.x, q4max.x, 
                q4min.y, q4max.y,
                q4min.z
            );

            quads.Add(q1);
            quads.Add(q2);
            quads.Add(q3);
            quads.Add(q4);
        }

        public void CreateBackup(int q)
        {            
            Quad quad = quads[q];

            Node node = null;

            while (node == null || node.Obstacle)
            {
                node = nm.GetNearestNodeToPosition(
                    new Vector3(
                        Random.Range(quad.MinX, quad.MaxX),
                        Random.Range(quad.MinY, quad.MaxY),
                        quad.Z
                    )
                );
            }

            GameObject backup = Instantiate(Backup, nm.GetNodeWorldPosition(node), Quaternion.identity);

            backupObjects[q] = new BackupObject(q, backup);

            backup.transform.parent = transform;
        }
        
        public IEnumerator SpawnBackup(GameObject obj)
        {
            yield return new WaitForSeconds(20f);

            int bo = 0;

            for (int i = 0; i < backupObjects.Length; i++)
            {
                if (backupObjects[i].BackupObj == obj) bo = i;
            }
            
            Quad quad = quads[backupObjects[bo].Q];

            Node node = null;

            while (node == null || node.Obstacle)
            {
                node = nm.GetNearestNodeToPosition(
                    new Vector3(
                        Random.Range(quad.MinX, quad.MaxX),
                        Random.Range(quad.MinY, quad.MaxY),
                        quad.Z
                    )
                );
            }

            GameObject backup = Instantiate(Backup, nm.GetNodeWorldPosition(node), Quaternion.identity);

            backupObjects[bo] = new BackupObject(backupObjects[bo].Q, backup);

            backup.transform.parent = transform;
        }
    }
}