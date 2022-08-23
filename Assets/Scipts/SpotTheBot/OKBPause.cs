using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace OKB
{
    public class OKBPause : MonoBehaviour
    {
        public Pause P;
        public Swipe s;

        public void PauseOKB()
        {
            s.Active = false;
            P.PauseGame();
        }

        public void ResumeOKB()
        {
            s.Active = true;
            P.ResumeGame();
        }
    }
}