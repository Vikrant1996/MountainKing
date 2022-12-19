using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float RunSpeed = 10f;

    public float JumpSpeed = 23f;

    public float ClimbSpeed = 5f;

    public Vector2 DeathKick = new Vector2 (8f, 8f);

    public GameObject Bullet;

    public Transform Gun;

    Vector2 moveInput;

    Rigidbody2D MyRigidbody;

    Animator MyAnimator;

    CapsuleCollider2D MyBodyCollider;

    BoxCollider2D MyFeetCollider;

    float GravityScaleAtStart;

    bool IsAlive = true;

    void Start()
    {
        MyRigidbody = GetComponent<Rigidbody2D>();
        MyAnimator = GetComponent<Animator>();
        MyBodyCollider = GetComponent<CapsuleCollider2D>();
        MyFeetCollider =GetComponent<BoxCollider2D>();
        GravityScaleAtStart = MyRigidbody.gravityScale;
    }

    void Update()
    {
        if(!IsAlive){
            return;
        }
        Run();
        FlipSprite();
        ClimbLadder();
        Die();
    }
      
      void OnFire(InputValue value){

        if(!IsAlive){

            return;
        }
            Instantiate(Bullet, Gun.position, transform.rotation);
      }
    void OnMove(InputValue value)
    {
        if(!IsAlive){
            return;
        }
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if(!IsAlive){
            return;
        }

        if (!MyFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }
        if (value.isPressed)
        {
            MyRigidbody.velocity += new Vector2(0f, JumpSpeed);
        }
    }

    void Run()
    {
        Vector2 playerVelocity =
            new Vector2(moveInput.x * RunSpeed, MyRigidbody.velocity.y);
        MyRigidbody.velocity = playerVelocity;

        bool PlayerHasHorizontalSpeed =
            Mathf.Abs(MyRigidbody.velocity.x) > Mathf.Epsilon;
        MyAnimator.SetBool("IsRun", PlayerHasHorizontalSpeed);
    }

    void FlipSprite()
    {
        bool PlayerHasHorizontalSpeed =
            Mathf.Abs(MyRigidbody.velocity.x) > Mathf.Epsilon;
        if (PlayerHasHorizontalSpeed)
        {
            transform.localScale =
                new Vector2(Mathf.Sign(MyRigidbody.velocity.x), 1f);
        }
    }

    void ClimbLadder()
    {
        if (!MyFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            MyRigidbody.gravityScale = GravityScaleAtStart;

            MyAnimator.SetBool("IsClimbing", false);

            return;
        }

        Vector2 climbVelocity =
            new Vector2(MyRigidbody.velocity.x, moveInput.y * ClimbSpeed);
        MyRigidbody.velocity = climbVelocity;
        MyRigidbody.gravityScale = 0f;

        bool PlayerHasVerticalSpeed =
            Mathf.Abs(MyRigidbody.velocity.y) > Mathf.Epsilon;
        MyAnimator.SetBool("IsClimbing", PlayerHasVerticalSpeed);
    }

    void Die(){

        if(MyBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies", "Hazards"))){
            IsAlive = false;
            MyAnimator.SetTrigger("Dying");
            MyRigidbody.velocity = DeathKick;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }
}
