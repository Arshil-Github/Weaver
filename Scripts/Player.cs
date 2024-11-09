using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour, IHealth
{
    public static Player Instance;
    
    //Player Movement
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float checkRadius;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private List<Move> moves;
    [SerializeField] private CharacterStats playerStats;

    [SerializeField] private PlayerWorldUI playerWorldUI;
    [SerializeField] private Animator playerAnimator;
    
    private float moveInput;
    private Rigidbody2D rb;
    private bool facingRight = true;
    private bool isGrounded;
    private float currentHealth;

    public EventHandler OnDeath { get; set; }

    public EventHandler OnActionButtonPressed;
    
    private bool canMove = true;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        currentHealth = playerStats.baseHealth;
        
    }

    private Vector3 originalScale;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalScale = playerAnimator.gameObject.transform.localScale;
    }

    private void Update()
    {
        GetMovementInput();
        playerAnimator.SetBool("inAir", !isGrounded);
        playerAnimator.SetFloat("xVelocity", Mathf.Abs(rb.linearVelocity.x));

        if (rb.linearVelocity.x < 0)
        {
            //Flip animator gameobject
            playerAnimator.gameObject.transform.localScale =  new Vector3(-originalScale.x, originalScale.y, 1);
        }
        else if (rb.linearVelocity.x > 0)
        {
            playerAnimator.gameObject.transform.localScale =  new Vector3(originalScale.x, originalScale.y, 1);
        }
        
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            OnActionButtonPressed?.Invoke(this, EventArgs.Empty);
        }

        CheckForGround();
    }

    public void ShowTalkPrompt()
    {
        playerWorldUI.ShowPrompt();
    }
    public void ShowFlyingText(string text)
    {
        playerWorldUI.TriggerFlyingText(text);
    }

    private void FixedUpdate()
    {
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        if(!canMove) return;
        //To be called in update to move the player
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);
    }
    
    private void Jump()
    {
        if(!canMove) return;
        rb.AddForce(jumpForce * Vector2.up, ForceMode2D.Impulse);
    }
    private void CheckForGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
    }
    private void GetMovementInput()
    {
        //To be called in update to get movementq input
        moveInput = Input.GetAxis("Horizontal");
    }
    public void SetMovability(bool allowMovement)
    {
        this.canMove = allowMovement;
    }
    
    public List<Move> GetMoves()
    {
        return moves;
    }
    public CharacterStats GetCharacterStats()
    {
        return playerStats;
    }
    
    #region Health
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(float healAmount)
    {
        currentHealth += healAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, playerStats.baseHealth);
    }

    public void SetMaxHealth(float maxHealth)
    {
        playerStats.baseHealth = maxHealth;
    }

    public float GetMaxHealth()
    {
        return playerStats.baseHealth;
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public void Die()
    {
        Debug.Log("Player has died");
    }
    #endregion
}
