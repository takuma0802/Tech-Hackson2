using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

public enum SceneState
{
    Title,
    Life,
    Game,
    GameOver,
}

[Serializable]
public class SceneStateReactiveProperty : ReactiveProperty<SceneState>
{

    public SceneStateReactiveProperty()
    {
    }

    public SceneStateReactiveProperty(SceneState initialValue) : base(initialValue)
    {
    }
}
