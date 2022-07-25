using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhackaAdSM : PuzzleSM
{
    public override void EnablePopup()
    {
        if (IsRunning || IsCompleted) return;

        base.EnablePopup();
    }

    public override void DisablePopup()
    {
        base.DisablePopup();
    }

    public override void Initialize()
    {
        if (IsRunning) return;

        base.Initialize();
    }

    public override void Cancel()
    {
        base.Cancel();
    }
}
