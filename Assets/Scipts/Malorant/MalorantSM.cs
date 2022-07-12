using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Malorant
{
    public class MalorantSM : PuzzleSM
    {
        public GameObject Spawner, UI;

        FPS_Spawner spawnerScript;

        void Start()
        {
            spawnerScript = Spawner.GetComponent<FPS_Spawner>();

            Cancel();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space)) Initialize();
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

            UI.SetActive(false);
            Spawner.SetActive(false);
        }
    }
}