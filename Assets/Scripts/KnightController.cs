using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDiretions), typeof(Damageable))]
public class KnightController : MonoBehaviour
{
    public float walkAccelaration = 3f;
    public float maxSpeed = 3f;
    public float walkStopRate = 0.06f;
    public DetectionZone attackZone;
    public DetectionZone cliffDetectionZone;

    Rigidbody2D rb;
    TouchingDiretions touchingDiretions;
    Animator animator;
    Damageable damageable;

    public enum WalkableDirection { Right, Left }

    private WalkableDirection _walkdirection;
    private Vector2 walkDirectionVector = Vector2.right;

    public WalkableDirection WalkDirection
    { 
        get 
        { 
            return _walkdirection; 
        }
        set 
        { 
            if (_walkdirection != value)
            {
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);
                if(value == WalkableDirection.Right)
                {
                    walkDirectionVector = Vector2.right;
                }else if(value == WalkableDirection.Left)
                {
                    walkDirectionVector = Vector2.left;
                }
            }
            _walkdirection = value; 
        } 
    }

    public bool _hasTarget = false;
    public bool HasTarget
    {
        get { return _hasTarget; }
        private set 
        { 
            _hasTarget = value; 
            animator.SetBool(AnimationStrings.hasTarget, value);
        }
    }

    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove); 
        }
        
    }

    public float AttackCoolDown { 
        get 
        {
            return animator.GetFloat(AnimationStrings.attackCooldown); 
        }
        private set 
        { 
            animator.SetFloat(AnimationStrings.attackCooldown, Mathf.Max(value, 0));
        } 
    }
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingDiretions= GetComponent<TouchingDiretions>();
        animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
    }

    void Update()
    {
        HasTarget = attackZone.detectedCollider.Count > 0;
        if(AttackCoolDown > 0)
        {
            AttackCoolDown -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        
        if (touchingDiretions.IsGrounded && touchingDiretions.IsOnWall)
        {
            FlipDirection();
        }
        if (!damageable.LockVelocity) 
        {
            if (CanMove && touchingDiretions.IsGrounded) 
            {
                
                rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x + (walkAccelaration * walkDirectionVector.x * Time.fixedDeltaTime), -maxSpeed, maxSpeed), rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }
        
            
        
    }

    private void FlipDirection()
    {
        if(WalkDirection == WalkableDirection.Right)
        {
            WalkDirection = WalkableDirection.Left;
        }else if(WalkDirection == WalkableDirection.Left)
        {
            WalkDirection = WalkableDirection.Right;
        }else
        {
            Debug.LogError("Current walkable is not set to legal value of right or left");
        }
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }

    public void OnCliffDectected()
    {
        if(touchingDiretions.IsGrounded)
        {
            FlipDirection();
        }
    }
}
