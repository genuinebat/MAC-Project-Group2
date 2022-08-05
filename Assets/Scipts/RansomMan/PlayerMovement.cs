using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

namespace RansomMan
{
    public class PlayerMovement : MonoBehaviour
    {
        public float Speed;

        [HideInInspector]
        public bool GameStarted;

        NodeManager nm;
        Pathfinder pf;
        Coroutine turningCor;

        List<Vector3> path;
        Vector3 targetLocation;
        Vector2 firstPressPos, secondPressPos, currentSwipe;

        void Start()
        {
            nm = GameObject.Find("NodeManager").GetComponent<NodeManager>();
            pf = new Pathfinder(nm, false);

            GameStarted = false;
        }

        void Update()
        {
            if (!GameStarted) return;

            SwipeInput();
            
            // FOR DEVELOPMENT ONLY
            ArrowInput();

            path = pf.GetPath(transform.position, transform.position + (transform.forward * nm.CellSize));

            if (path.Count > 0) targetLocation = path[0];

            transform.position = Vector3.MoveTowards(transform.position, targetLocation, Speed * Time.deltaTime);
        }

        public void PlayerStartPosition()
        {
            transform.position = nm.GetNodeWorldPosition(nm.grid.Get(13, 7));
            transform.rotation = Quaternion.Euler(0f, 90f, -90f);
        }

        void SwipeInput()
        {
            if(Input.touches.Length > 0)
            {
                Touch t = Input.GetTouch(0);
                if(t.phase == TouchPhase.Began)
                {
                    firstPressPos = new Vector2(t.position.x,t.position.y);
                }
                if(t.phase == TouchPhase.Ended)
                {
                    secondPressPos = new Vector2(t.position.x,t.position.y);
                                
                    currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
                    
                    currentSwipe.Normalize();
        
                    //swipe upwards
                    if(currentSwipe.y > 0 && currentSwipe.x > -0.5f  &&currentSwipe.x < 0.5f)
                    {
                        if (turningCor != null) StopCoroutine(turningCor);

                        turningCor = StartCoroutine(Turn(Quaternion.Euler(-90f, 0f, 0f)));
                    }
                    
                    //swipe down
                    if(currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                    {
                        if (turningCor != null) StopCoroutine(turningCor);

                        turningCor = StartCoroutine(Turn(Quaternion.Euler(90f, 180f, 0f)));
                    }

                    //swipe left
                    if(currentSwipe.x < 0 && currentSwipe.y > -0.5f &&currentSwipe.y < 0.5f)
                    {
                        if (turningCor != null) StopCoroutine(turningCor);

                        turningCor = StartCoroutine(Turn(Quaternion.Euler(0f, -90f, 90f)));
                    }

                    //swipe right
                    if(currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                    {
                        if (turningCor != null) StopCoroutine(turningCor);

                        turningCor = StartCoroutine(Turn(Quaternion.Euler(0f, 90f, -90f)));
                    }
                }
            }
        }

        IEnumerator Turn(Quaternion lookRot)
        {
            for (;;)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRot, Time.deltaTime * 720);

                yield return null;
            }
        }

        // FOR DEVELOPMENT ONLY
        void ArrowInput()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (turningCor != null) StopCoroutine(turningCor);

                turningCor = StartCoroutine(Turn(Quaternion.Euler(-90f, 0f, 0f)));
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (turningCor != null) StopCoroutine(turningCor);

                turningCor = StartCoroutine(Turn(Quaternion.Euler(90f, 180f, 0f)));
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (turningCor != null) StopCoroutine(turningCor);

                turningCor = StartCoroutine(Turn(Quaternion.Euler(0f, -90f, 90f)));
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (turningCor != null) StopCoroutine(turningCor);

                turningCor = StartCoroutine(Turn(Quaternion.Euler(0f, 90f, -90f)));
            }
        }
    }
}