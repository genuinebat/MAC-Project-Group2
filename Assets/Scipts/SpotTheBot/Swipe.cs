using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OKB
{
    public class Swipe : MonoBehaviour
    {
        [Header("Reference Variables")]
        public Transform AnchorPoint;
        public Transform SwipeObj;
        public GameObject GoodBotPanel;
        public GameObject BadBotPanel;

        [HideInInspector]
        public bool Active;
        [HideInInspector]
        public Vector3 ogPos;
        [HideInInspector]
        public bool StopFly;
        [HideInInspector]
        public bool Trojan;

        TemplateManager tm;
    
        Vector2 firstPressPos, secondPressPos, currentSwipe;

        Vector3 left, right;
        Quaternion ogRot, targetRotLeft, targetRotRight;

        Vector3 targetPosition;
        Quaternion targetRot;

        void Start()
        {
            tm = GetComponent<TemplateManager>();

            ogPos = new Vector3(AnchorPoint.position.x, AnchorPoint.position.y, AnchorPoint.position.z - 1f);
            
            ogRot = Quaternion.Euler(0f, 0f, 0f);

            targetRotLeft = Quaternion.Euler(0f, 0f, 15f);
            
            targetRotRight = Quaternion.Euler(0f, 0f, -15f);

            left = new Vector3(SwipeObj.position.x - 0.8f, SwipeObj.position.y, SwipeObj.position.z);

            right = new Vector3(SwipeObj.position.x + 0.8f, SwipeObj.position.y, SwipeObj.position.z);

            Active = false;
            Trojan = false;
        }

        void Update()
        {
            if (!Active) return;

            if (Input.touches.Length > 0)
            {
                Touch t = Input.GetTouch(0);
                if (t.phase == TouchPhase.Began)
                {
                    firstPressPos = new Vector2(t.position.x, t.position.y);
                }

                if (t.phase == TouchPhase.Moved)
                {
                    secondPressPos = new Vector2(t.position.x, t.position.y);

                    currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

                    Vector3 currentSwipeNorm = currentSwipe.normalized;

                    if (currentSwipe.x < -250)
                    {
                        GoodBotPanel.SetActive(false);
                        BadBotPanel.SetActive(true);
                    }
                    else if (currentSwipe.x > 250)
                    {
                        GoodBotPanel.SetActive(true);
                        BadBotPanel.SetActive(false);
                    }
                    else
                    {
                        GoodBotPanel.SetActive(false);
                        BadBotPanel.SetActive(false);
                    }

                    // swipe left
                    if (currentSwipe.x < 0)
                    {
                        targetPosition =  Vector3.Lerp(ogPos, left, Mathf.Abs(currentSwipe.x) / 300);
                    
                        targetRot = Quaternion.Lerp(ogRot, targetRotLeft, Mathf.Abs(currentSwipe.x) / 300);
                    }

                    // swipe right
                    if (currentSwipe.x > 0)
                    {
                        targetPosition =  Vector3.Lerp(ogPos, right, Mathf.Abs(currentSwipe.x) / 300);

                        targetRot = Quaternion.Lerp(ogRot, targetRotRight, Mathf.Abs(currentSwipe.x) / 300);
                    }

                    SwipeObj.position = Vector3.MoveTowards(SwipeObj.position, targetPosition, 10 * Time.deltaTime);

                    SwipeObj.rotation = Quaternion.RotateTowards(SwipeObj.rotation, targetRot, 80 * Time.deltaTime);
                }

                if (t.phase == TouchPhase.Ended)
                {
                    secondPressPos = new Vector2(t.position.x, t.position.y);

                    currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

                    // bad bot (left)
                    if (currentSwipe.x < -250)
                    {
                        Active = false;
                        StopFly = false;

                        if (Trojan)
                        {
                            StartCoroutine(FlyLeft());
                            tm.Win();
                        }
                        else
                        {
                            StartCoroutine(FlyLeft());
                            tm.SetStatementSelect();
                        }
                    }
                    // good bot (right)
                    else if (currentSwipe.x > 250)
                    {
                        Active = false;
                        StopFly = false;

                        if (Trojan)
                        {
                            StartCoroutine(FlyRight());
                            tm.PlayerHealth = 0;
                            tm.LoseTrojan();
                        }
                        else
                        {
                            StartCoroutine(FlyRight());
                            tm.GoodBot();
                        }
                    }
                }
            }
            else
            {
                GoodBotPanel.SetActive(false);
                BadBotPanel.SetActive(false);

                SwipeObj.position = Vector3.MoveTowards(SwipeObj.position, ogPos, 8  * Time.deltaTime);

                SwipeObj.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0f, 0f, 0f), 5 * Time.deltaTime);
            }
        }

        IEnumerator FlyRight()
        {
            Vector3 target = SwipeObj.position + (5 * SwipeObj.right);

            while (Vector3.Distance(SwipeObj.position, target) > 0.02f)
            {
                if (StopFly) yield break;

                SwipeObj.position = Vector3.MoveTowards(SwipeObj.position, target, 10 * Time.deltaTime);

                yield return null;
            }
        }

        IEnumerator FlyLeft()
        {
            Vector3 target = SwipeObj.position - (5 * SwipeObj.right);

            while (Vector3.Distance(SwipeObj.position, target) > 0.02f)
            {
                if (StopFly) yield break;

                SwipeObj.position = Vector3.MoveTowards(SwipeObj.position, target, 10 * Time.deltaTime);

                yield return null;
            }
        }
    }
}