using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;

public class PlayerMover : PlayerCore
{
    [SerializeField] private float runThreshold;
    [SerializeField] private float runForce;
    [SerializeField] private float runSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpMoveForce;
    [SerializeField] private ContactFilter2D filter2d;
    
    private Vector2 inputDirection;
    private PlayerInputProvider inputProvider;
    private Rigidbody2D rb;

    protected override void OnInitialize()
    {
        inputProvider = GetComponent<PlayerInputProvider>();
        rb = GetComponent<Rigidbody2D>();

        inputProvider.MoveDirection
            .TakeUntilDestroy(this)
            //.Where(_ => GameManager.Instance.CurrentSceneState.Value == SceneState.Game)
            .Subscribe(x =>
            {
                var value = Vector3.zero;
                if (!IsAlive.Value) return;
                if (IsGround.Value)
                {
                    value = x.normalized * runForce;
                }
                else // 飛んでる時
                {
                    value = x.normalized * runForce * jumpMoveForce;
                }

                inputDirection = value;
            });

        inputProvider.JumpButton
            .TakeUntilDestroy(this)
            //.Where(_ => GameManager.Instance.CurrentSceneState.Value == SceneState.Game)
            .Where(x => x && IsAlive.Value && IsGround.Value)
            //.ThrottleFirst(TimeSpan.FromSeconds(1))
            .Subscribe(_ =>
            {
                Jump();
            });

        this.FixedUpdateAsObservable()
            .TakeUntilDestroy(this)
            .Subscribe(_ =>
            {
                Move();
                isGround.Value = CheckGrounded();
            });
    }

    private void Move()
    {
        float speedX = Mathf.Abs(this.rb.velocity.x);
        if (speedX < runThreshold)
        {
            rb.AddForce(inputDirection);
        }
        else
        {
            this.transform.position += new Vector3(runSpeed * Time.deltaTime * inputDirection.normalized.x, 0, 0);
        }
    }

    private void Jump()
    {
        rb.AddForce(transform.up * jumpForce);

        isGround.Value = false;
        isJumped.Value = true;
        Observable.Timer(TimeSpan.FromSeconds(0.5f))
            .TakeUntilDestroy(this)
            .Subscribe(_ =>
            {
                isJumped.Value = false;
            });
    }

    private bool CheckGrounded()
    {
        return rb.IsTouching(filter2d);
    }
}
