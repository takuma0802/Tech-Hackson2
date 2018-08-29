using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public abstract class PlayerCore : MonoBehaviour, IDamagable
{

    protected ReactiveProperty<bool> isAlive = new BoolReactiveProperty(true);
    protected ReactiveProperty<bool> isGround = new BoolReactiveProperty(true);
    protected ReactiveProperty<bool> isJumped = new BoolReactiveProperty(false);
    public IReadOnlyReactiveProperty<bool> IsAlive { get { return isAlive; } }
    public IReadOnlyReactiveProperty<bool> IsGround { get { return isGround; } }
    public IReadOnlyReactiveProperty<bool> IsJumped { get { return isJumped; } }

    void Start()
    {
        OnInitialize();
    }

    protected abstract void OnInitialize();

    public void Damage()
    {
        Destroy(this.gameObject);
        isAlive.Value = false;
    }


}
