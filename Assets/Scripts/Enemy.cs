using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float timeBtwAttack;
    public float startTimeBtwAttack;

    public int health;
    public float speed;
    public GameObject deathEffect;
    public int damage;
    private float stopTime;
    public float startStopTime;
    public float normalSpeed;
    private Player player;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        player = FindObjectOfType<Player>();
        normalSpeed = speed;
    }

    
    

    // Update is called once per frame
    void Update()
    {
        if(stopTime <= 0)
        {
            speed = normalSpeed;
        }
        else
        {
            speed=0;
            stopTime -= Time.deltaTime;
        }
        if(health<=0)
        {
            Destroy(gameObject);
        }
       transform.position = Vector2.MoveTowards(transform.position, player.transform.position,  speed * Time.deltaTime); 
    }

    public void TakeDamage(int damage)
    {
        stopTime = startStopTime;
        health -= damage;
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            if(timeBtwAttack <= 0)
            {
                anim.SetTrigger("enemyAtack");
            }
            else
            {
                timeBtwAttack -= Time.deltaTime;
            }
        }
    }

    public void OnEnemyAttack()
    {
       player.health -= damage;
       timeBtwAttack = startTimeBtwAttack; 
    }
}
