using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlayerSound : PlayerCore
{
    protected override void OnInitialize()
    {
        IsJumped.TakeUntilDestroy(this)
            .Where(x => x && IsAlive.Value)
            .Subscribe(_ => OnJumpedSound());
    }

    private void OnJumpedSound()
    {
        Debug.Log("JumpSE");
        AudioManager.PlaySoundEffect(AudioType.JumpSE);
    }
}
