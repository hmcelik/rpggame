using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Mover
{
    public int xpRate = 1;
    public float triggerLenght = 0.3f;
    public float chaseLenght = 1f;
    private bool chasing;
    private bool collidingWithPLayer;
    private Transform playerTransform;
    private Vector3 startingPosition;
    
    private BoxCollider2D hitbox;
    private Collider2D[] hits = new Collider2D[10];
    public ContactFilter2D filter;
    
    protected override void Start()
    {
        base.Start();
        playerTransform = GameManager.instance.player.transform;
        startingPosition = transform.position;
        hitbox = transform.GetChild(0).GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(playerTransform.position, startingPosition) < chaseLenght)
        {
            if (Vector3.Distance(playerTransform.position, startingPosition) < triggerLenght)
            {
                chasing=true;
            }
            if(chasing){
                if (!collidingWithPLayer)
                {
                    UpdateMotor((playerTransform.position - transform.position).normalized);
                }
            }
            else
            {
                UpdateMotor(startingPosition - transform.position);
            }
        }
        else
        {
            UpdateMotor(startingPosition - transform.position);
            chasing = false;
        }
        collidingWithPLayer = false;
        
        boxCollider.OverlapCollider(filter, hits);
        
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i]==null)
            {
                continue;
            }

            if (hits[i].CompareTag("Fighter")&&hits[i].name=="Player")
            {
                collidingWithPLayer = true;
            }
            hits[i] = null;
        }
    }
    protected override void Death()
    {
        Destroy(gameObject);
        GameManager.instance.experience += xpRate;
        GameManager.instance.ShowText("+" + xpRate + " xp", 25, Color.magenta, transform.position, Vector3.up * 25, 1.0f);
    }
}
