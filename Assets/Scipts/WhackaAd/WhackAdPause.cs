using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace WhackaAd
{
    public class WhackAdPause : MonoBehaviour
    {
        public Pause P;
        public TappedOnAdware T;

        public void PauseWA()
        {
            T.Paused = true;
            P.PauseGame();
        }

        public void ResumeWA()
        {   
            P.ResumeGame();
            T.Paused = false;
        }
    }
}