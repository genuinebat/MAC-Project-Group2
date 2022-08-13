using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpotTheBot
{
    public class Swipe : MonoBehaviour
    {
        public Transform SwipePanel;

        Vector2 firstPressPos, secondPressPos, currentSwipe;

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

                        }

                        // swipe right
                        if (currentSwipe.x > 0)
                        {

                        }
                    }
                }
            }
        }
    }
}