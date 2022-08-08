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

        Vector3 backupLocation;

        void Start()
        {
            BackedUp = false;
        }

        private void OnTriggerEnter(Collider other)
        {
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

                }
                else
                {
                    BackedUp = true;
                    backupLocation = gameObject.transform.position;
                    Destroy(other.gameObject);
                }
            }
        }

        public IEnumerator RevertToBackup()
        {
            PlayerMovement movementScript = GetComponent<PlayerMovement>();

            movementScript.enabled = false;

            List<Vector3> path = new List<Vector3>();

            foreach (Node node in BTScript.Temp)
            {
                path.Add(nm.GetNodeWorldPosition(node));
            }

            path.Reverse();
            
            int n = 0;
            while (n < path.Count)
            {
                if (Vector3.Distance(transform.position, path[n]) < 0.02f)
                {
                    n++;
                }
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, path[n], 10 * Time.deltaTime);
                }
                yield return null;
            }

            transform.position = backupLocation;

            transform.rotation = Quaternion.LookRotation((path[n - 2] - transform.position), -Vector3.forward);

            movementScript.enabled = true;
        }
    }
}
