using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Test : MonoBehaviour
{
    public NodeManager nm;

    Pathfinder pf;
    List<Vector3> path;

    int a = 0;
    bool dir = true;

    void Start()
    {
        pf = new Pathfinder(nm, false);
        
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        yield return new WaitForSeconds(1f);
        path = dir ? pf.GetPath(transform.position, new Vector3(13, 14, -1)) : pf.GetPath(transform.position, new Vector3(-13, -14, -1));
        for (;;)
        {
            if (Vector3.Distance(transform.position, path[a]) <= 0.02f)
            {
                a++;
            }
            else {
                transform.position =
                    Vector3.MoveTowards(transform.position, path[a], 1 * Time.deltaTime);
            }

            if (a > path.Count - 1)
            {
                dir = !dir;
                a = 0;
                StartCoroutine(Move());
                yield break;
            }
            yield return null;
        }
    }
}