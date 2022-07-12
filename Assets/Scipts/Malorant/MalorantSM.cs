using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Malorant
{
    public class MalorantSM : PuzzleSM
    {
        public GameObject Spawner;
        public GameObject UI;

        FPS_Spawner spawnerScript;

        void Start()
        {
            spawnerScript = Spawner.GetComponent<FPS_Spawner>();
        }

        public override void Initialize()
        {
            if (IsRunning) return;

            base.Initialize();

            UI.SetActive(true);
            Spawner.SetActive(true);
            spawnerScript.EnterView();
        }

        public override void Cancel()
        {
            base.Cancel();

            Spawner.SetActive(false);
            UI.SetActive(false);
        }
    }
}