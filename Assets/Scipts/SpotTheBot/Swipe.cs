using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpotTheBot
{
    public class Swipe : MonoBehaviour
    {
        public Transform ImageTarget;
        public Transform SwipeCanvas;

        Vector2 firstPressPos, secondPressPos, currentSwipe;

        Vector3 ogPos;

        void Start()
        {
            ogPos = new Vector3(ImageTarget.position.x, ImageTarget.position.y, ImageTarget.position.z - 1);
        }

        void Update()
        {
            if (Input.touches.Length > 0)
            {
                Touch t = Input.GetTouch(0);
                if (t.phase == TouchPhase.Began)
                {
                    firstPressPos = new Vector2(t.position.x, t.position.y);
                }

                if (t.phase == TouchPhase.Ended)
                {
                    secondPressPos = new Vector2(t.position.x, t.position.y);

                    currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

                    currentSwipe.Normalize();

                    if (currentSwipe.y > - 0.5f && currentSwipe.y < 0.5f)
                    {
                        // swipe left
                        if (currentSwipe.x < 0)
                        {
                            Debug.Log("SwipedLeft");
                            Vector3 left = new Vector3(SwipeCanvas.position.x - 2, SwipeCanvas.position.y, SwipeCanvas.position.z );

                            SwipeCanvas.position =  Vector3.Lerp(SwipeCanvas.position, left, Mathf.Abs(SwipeCanvas.position.x) / 300);
                        }

                        // swipe right
                        if (currentSwipe.x > 0)
                        {

                        }
                    }
                }
            }
            else
            {
                SwipeCanvas.position = Vector3.MoveTowards(SwipeCanvas.position, ogPos, 0.1f  * Time.deltaTime);
            }
        }
    }
}