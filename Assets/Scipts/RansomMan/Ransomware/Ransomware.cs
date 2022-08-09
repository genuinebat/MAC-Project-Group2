using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

namespace RansomMan
{
    public class Ransomware : MonoBehaviour
    {
        [Header("Gameplay Values")]
        public float WanderSpeed;
        public float ChaseSpeed;
        public float TimeOutDuration;

        [HideInInspector]
        public bool Active = false;
        [HideInInspector]
        public NodeManager nm;
        [HideInInspector]
        public Vector3 TimeOutLocation;
        [HideInInspector]
        public int quad;

        Pathfinder pf;
        BackupSpawner bs;

        GameObject player;
        List<Vector3> chasePlayerPath, wanderPath;
        Vector3 spawnLocation;

        bool chase;
        int wanderNode;
        float minX, maxX, minY, maxY, z;

        void Start()
        {
            nm = GameObject.Find("NodeManager").GetComponent<NodeManager>();

            pf = new Pathfinder(nm, false);
            bs = GameObject.Find("Spawner").GetComponent<BackupSpawner>();

            player = GameObject.Find("Player");

            chase = false;
            wanderPath = new List<Vector3>();
            wanderNode = int.MaxValue;
        }
        
        void Update()
        {
            if (!Active) return;

            CheckChasePlayer();

            if (chase) ChasePlayer();
            else Wander();
        }

        public void SetSpawnPosition(int x, int y)
        {
            transform.position = nm.GetNodeWorldPosition(nm.grid.Get(x, y));
            transform.rotation = Quaternion.Euler(-90f, 90f, -90f);
            
            spawnLocation = transform.position;
            
            SetWanderBoundaries();
        }

        void SetWanderBoundaries()
        {
            minX = transform.position.x - 1.2f;
            maxX = transform.position.x + 1.2f;
            minY = transform.position.y - 1.2f;
            maxY = transform.position.y + 1.2f;
            z = transform.position.y;
        }

        void Wander()
        {
            if (wanderNode >= wanderPath.Count)
            {
                Vector3 pos = new Vector3(
                    Random.Range(minX, maxX),
                    Random.Range(minY, maxY),
                    z
                );

                wanderPath = pf.GetPath(transform.position, pos);

                wanderNode = 0;
            }
            else
            {
                if (Vector3.Distance(transform.position, wanderPath[wanderNode]) < 0.02f)
                {
                    wanderNode++;
                }
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, wanderPath[wanderNode], WanderSpeed * Time.deltaTime);
                    
                    Quaternion lookRotation = Quaternion.LookRotation((wanderPath[wanderNode] - transform.position), -Vector3.forward);

                    transform.rotation =
                        Quaternion.RotateTowards(transform.rotation, lookRotation, 360 * Time.deltaTime);
                }
            }
        }

        void CheckChasePlayer()
        {
            if (player.GetComponent<RansomManCollector>().Reverting)
            {
                chase = false;
                return;
            }

            chasePlayerPath = pf.GetPath(transform.position, player.transform.position);

            if (chase)
            {
                if (chasePlayerPath.Count <= 0) CatchPlayer();
                if (chasePlayerPath.Count > 10)
                {
                    chase = false;
                    wanderNode = int.MaxValue;
                }
            }
            else
            {
                if (chasePlayerPath.Count <= 6) chase = true;
            }
        }

        void ChasePlayer()
        {   
            if (chasePlayerPath.Count <= 0) return;

            transform.position = Vector3.MoveTowards(transform.position, chasePlayerPath[0], ChaseSpeed * Time.deltaTime);

            Quaternion lookRotation = Quaternion.LookRotation((chasePlayerPath[0] - transform.position), -Vector3.forward);

            transform.rotation =
                Quaternion.RotateTowards(transform.rotation, lookRotation, 360 * Time.deltaTime);
        }

        void CatchPlayer()
        {
            RansomManCollector rmCollector = player.GetComponent<RansomManCollector>();

            if (rmCollector.BackedUp)
            {
                StartCoroutine(rmCollector.RevertToBackup());
                StartCoroutine(TimeOut());
            }
            else
            {
                // end game
                Debug.Log("YOU LOSE!");
            }
        }

        IEnumerator TimeOut()
        {
            Active = false;
            transform.position = TimeOutLocation;

            yield return new WaitForSeconds(TimeOutDuration);

            transform.position = spawnLocation;
            Active = true;

            // spawn backup
            bs.SpawnBackup(quad);
        }
    }
}