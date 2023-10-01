using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public ControlType controlType;
    public Joystick joystick;
    public float speed;

public enum ControlType{PC, Android}

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 moveVelocity;
    private Animator anim;

    private bool facingRight = true;

    public float health;
    public int numOfHearts;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        if(controlType== ControlType.PC)
        {
            joystick.gameObject.SetActive(false);
        }
    }


    // Update is called once per frame
    void Update()
    {
        
        if(controlType== ControlType.PC)
        {
            moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }
         if(controlType== ControlType.Android)
        {
            moveInput = new Vector2(joystick.Horizontal, joystick.Vertical);
        }
        
        moveVelocity = moveInput.normalized * speed;

        if(moveInput.x == 0)
        {
            anim.SetBool("isRunning", false);
        }
        else
        {
           anim.SetBool("isRunning", true); 
        }

        if(!facingRight && moveInput.x > 0)
        {
            Flip();
        }
        if(facingRight && moveInput.x < 0)
        {
            Flip();
        }
    }

    void FixedUpdate()
    {
        if(health > numOfHearts)
        {
            health = numOfHearts;
        }

        for(int i=0; i < hearts.Length; i++)
        {
            if(i < Mathf.RoundToInt(health))
            {
                hearts[i].sprite = fullHeart;
            }
            else{

                hearts[i].sprite = emptyHeart;
            }

           if(i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
        
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

}
