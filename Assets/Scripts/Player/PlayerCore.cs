using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public abstract class PlayerCore : MonoBehaviour, IDamagable
{

    protected ReactiveProperty<bool> isAlive = new BoolReactiveProperty(true);
    protected ReactiveProperty<bool> isGround = new BoolReactiveProperty(true);
    public IReadOnlyReactiveProperty<bool> IsAlive { get { return isAlive; } }
    public IReadOnlyReactiveProperty<bool> IsGround { get { return isGround; } }

    void Awake()
    {

    }

    void Start()
    {
        OnInitialize();
    }

    protected abstract void OnInitialize();

    public void Damage()
    {
        isAlive.Value = false;
    }


}
