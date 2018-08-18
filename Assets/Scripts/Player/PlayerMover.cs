using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;

public class PlayerMover : PlayerCore
{
    [SerializeField] private float runThreshold = 1f;
    [SerializeField] private float runSpeed = 0.1f;
    [SerializeField] private float runForce = 50f;
    [SerializeField] private float jumpForce = 300f;

    [SerializeField] private ContactFilter2D filter2d;
    private Vector2 inputDirection;
    private PlayerInputProvider inputProvider;
    private Rigidbody2D rb;

    protected override void OnInitialize()
    {
        inputProvider = GetComponent<PlayerInputProvider>();
        rb = GetComponent<Rigidbody2D>();

        inputProvider.MoveDirection
            //.Where(_ => CurrentGameState.Value == GameState.Battle)
            .Subscribe(x =>
            {
                var value = Vector3.zero;
                if (!IsAlive.Value) return;
                if (IsGround.Value)
                {
                    value = x.normalized * runForce;
                }
                else
                {
					value = x.normalized * runForce * 0.5f;
                }

                inputDirection = value;
            });

        inputProvider.JumpButton
            //.Where(_ => CurrentGameState.Value == GameState.Battle)
            .Where(x => x && IsAlive.Value && IsGround.Value)
            .ThrottleFirst(TimeSpan.FromSeconds(1))
            .Subscribe(_ =>
            {
                Jump();
            });

        this.FixedUpdateAsObservable()
                .Subscribe(_ =>
                {
                    Move();
                    isGround.Value = CheckGrounded();
                });


        // inputProvider.MoveDirection
        //         .Where(x => IsAlive.Value)
        //         .Subscribe(x => _isRunning.Value = !IsJumping.Value && x.magnitude >= 0.1f);


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
    }

    private bool CheckGrounded()
    {
        return rb.IsTouching(filter2d);
    }
}
