using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

namespace RansomMan
{
    public class RansomManCollector : MonoBehaviour
    {
        public ByteTracker BTScript;
        public NodeManager nm;
        public BackupSpawner bs;

        [HideInInspector]
        public bool BackedUp;
        [HideInInspector]
        public bool Reverting;
        [HideInInspector]
        public List<Vector3> BackupPath = new List<Vector3>();

        Pathfinder pf;
        Vector3 backupLocation;

        void Start()
        {
            pf = new Pathfinder(nm, false);

            BackedUp = false;
            Reverting = false;
        }

        void Update()
        {
            if (BackedUp)
            {
                Node n = nm.GetNearestNodeToPosition(transform.position, 0.2f);

                if (n != null)
                {
                    if (BackupPath.Count > 0)
                    {
                        if (nm.GetNodeWorldPosition(n) != BackupPath[BackupPath.Count - 1])
                        {
                            BackupPath.Add(nm.GetNodeWorldPosition(n));
                        }
                    }
                    else
                    {
                        BackupPath.Add(nm.GetNodeWorldPosition(n));
                    }
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (Reverting) return;

            if (other.gameObject.tag == "Byte")
            {
                Node node = nm.GetNearestNodeToPosition(other.gameObject.transform.position);

                node.Object.SetActive(false);

                if (BackedUp) BTScript.Temp.Add(node);
                else BTScript.Collected++;
            }
            else if (other.gameObject.tag == "Backup")
            {
                if (BackedUp)
                {
                    Destroy(other.gameObject);

                    BTScript.Collected += BTScript.Temp.Count;

                    BTScript.Temp.Clear();
                    BackupPath.Clear();

                    nm.GetNearestNodeToPosition(backupLocation).Particle.SetActive(false);

                    backupLocation = other.gameObject.transform.position;

                    nm.GetNearestNodeToPosition(backupLocation).Particle.SetActive(true);
                }
                else
                {
                    Destroy(other.gameObject);

                    BackedUp = true;

                    backupLocation = other.gameObject.transform.position;

                    nm.GetNearestNodeToPosition(backupLocation).Particle.SetActive(true);
                }

                StartCoroutine(bs.SpawnBackup(other.gameObject));
            }
        }

        public IEnumerator RevertToBackup()
        {
            BackedUp = false;
            Reverting = true;

            PlayerMovement movementScript = GetComponent<PlayerMovement>();

            movementScript.enabled = false;

            BackupPath.RemoveAt(0);

            BackupPath.Reverse();

            int n = 0;
            while (n < BackupPath.Count)
            {
                if (Vector3.Distance(transform.position, BackupPath[n]) < 0.02f)
                {
                    Node node = nm.GetNearestNodeToPosition(BackupPath[n]);

                    if (BTScript.Temp.Contains(node))
                    {
                        node.Object.SetActive(true);
                        BTScript.Temp.Remove(node);
                    }
                    n++;
                }
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, BackupPath[n], 6 * Time.deltaTime);

                    if (n > 0)
                    {
                        transform.rotation = Quaternion.LookRotation((BackupPath[n - 1] - transform.position), -Vector3.forward);
                    }
                }
                yield return null;
            }

            transform.position = backupLocation;

            if (BackupPath.Count > 0) transform.rotation = Quaternion.LookRotation((BackupPath[n - 1] - backupLocation), -Vector3.forward);

            Node backupNode = nm.GetNearestNodeToPosition(backupLocation);

            if (backupNode.Object.activeSelf)
            {
                BTScript.Collected++;
                backupNode.Object.SetActive(false);
            }

            backupNode.Particle.SetActive(false);

            BTScript.Collected += BTScript.Temp.Count;

            BTScript.Temp.Clear();
            BackupPath.Clear();

            Reverting = false;

            movementScript.enabled = true;
        }
    }
}
