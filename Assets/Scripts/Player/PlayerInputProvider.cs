using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class PlayerInputProvider : PlayerCore
{

    private ReactiveProperty<bool> _jump = new BoolReactiveProperty();
    private ReactiveProperty<Vector3> _moveDirection = new ReactiveProperty<Vector3>();
    public IReadOnlyReactiveProperty<Vector3> MoveDirection { get { return _moveDirection; } }
    public IReadOnlyReactiveProperty<bool> JumpButton { get { return _jump; } }

    protected override void OnInitialize()
    {
        this.UpdateAsObservable()
                .Select(_ => Input.GetKey(KeyCode.Space))
                .DistinctUntilChanged()
                .Subscribe(x => _jump.Value = x);

        this.UpdateAsObservable()
            .Select(_ => new Vector3(Input.GetAxis("Horizontal"), 0, 0))
            .Subscribe(x => _moveDirection.SetValueAndForceNotify(x));
    }
}
