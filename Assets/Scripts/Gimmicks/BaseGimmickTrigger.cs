using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class BaseGimmickTrigger : MonoBehaviour
{
    private ReactiveProperty<bool> onTrigger = new BoolReactiveProperty(false);
    public IReadOnlyReactiveProperty<bool> OnTrigger { get { return onTrigger; } }

    void OnTriggerEnter2D(Collider2D col)
    {
        onTrigger.Value = true;
        GetComponent<BoxCollider2D>().enabled = false;
    }
}
