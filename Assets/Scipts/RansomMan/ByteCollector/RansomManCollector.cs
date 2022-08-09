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

        [HideInInspector]
        public bool BackedUp;
        [HideInInspector]
        public bool Reverting;

        List<Vector3> backupPath = new List<Vector3>();
        Vector3 backupLocation;

        void Start()
        {
            BackedUp = false;
            Reverting = false;
        }
        
        void Update()
        {
            if (BackedUp)
            {
                Node n = nm.GetNearestNodeToPosition(transform.position, 0.1f);

                if (n != null)
                {
                    if (backupPath.Count > 0)
                    {
                        if (nm.GetNodeWorldPosition(n) != backupPath[backupPath.Count - 1])
                        {
                            backupPath.Add(nm.GetNodeWorldPosition(n));
                        }
                    }
                    else
                    {
                        backupPath.Add(nm.GetNodeWorldPosition(n));
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

                if (BackedUp) BTScript.Temp.Add(node);
                else BTScript.Collected++;

                node.Object.SetActive(false);
            }
            else if (other.gameObject.tag == "Backup")
            {
                if (BackedUp)
                {
                    BTScript.Collected += BTScript.Temp.Count;

                    BTScript.Temp.Clear();
                    backupPath.Clear();
                }
                else
                {
                    Destroy(other.gameObject);

                    BackedUp = true;

                    backupLocation = gameObject.transform.position;
                }
            }
        }

        public IEnumerator RevertToBackup()
        {
            BackedUp = false;
            Reverting = true;

            PlayerMovement movementScript = GetComponent<PlayerMovement>();

            movementScript.enabled = false;

            backupPath.Reverse();

            int n = 0;
            while (n < backupPath.Count)
            {
                if (Vector3.Distance(transform.position, backupPath[n]) < 0.02f)
                {
                    Node node = nm.GetNearestNodeToPosition(backupPath[n]);

                    if (BTScript.Temp.Contains(node))
                    {
                        node.Object.SetActive(true);
                        BTScript.Temp.Remove(node);
                    }
                    n++;
                }
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, backupPath[n], 8 * Time.deltaTime);

                    if (n > 0)
                    {
                        Quaternion lookRot = Quaternion.LookRotation((backupPath[n - 1] - transform.position), -Vector3.forward);

                        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRot, 720 * Time.deltaTime);
                    }
                }
                yield return null;
            }

            BTScript.Collected += BTScript.Temp.Count;

            BTScript.Temp.Clear();
            backupPath.Clear();

            transform.position = backupLocation;

            Reverting = false;
            
            movementScript.enabled = true;
        }
    }
}