using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{
  public int damagePoint = 1;
  public float pushForce = 2.0f;
  
  public int weaponLevel = 0;
  private SpriteRenderer spriteRenderer;
  
  private float cooldown = 0.3f;
  private float lastSwing;
  private bool isThrown = false;
  private float spinSpeed = 2.0f;
  private float spinSpeedMultiplier = 2.0f;

  
  
  protected override void Start()
  {
    base.Start();
    spriteRenderer = GetComponent<SpriteRenderer>();
  }

  protected override void Update()
  {
    base.Update();
    if (Input.GetKeyDown(KeyCode.Space))
    {
      if (Time.time - lastSwing > cooldown)
      {
        lastSwing = Time.time;
        Swing();
      }
    }
    
    if (Input.GetMouseButtonDown(0))
    {
      isThrown = true;
      spinSpeed *= spinSpeedMultiplier;
    }
    
    if (Input.GetMouseButtonUp(0))
    {
      spinSpeed /= spinSpeedMultiplier;
    }
    
    if (Input.GetKeyDown(KeyCode.T))
    {
      if (isThrown)
      {
        isThrown = false;
        ReturnToPlayer();
      }
      else
      {
        Throw();
      }
    }
    
    if (isThrown)
    {
      transform.Rotate(0, 0, spinSpeed);
    }
  }
  
  protected override void OnCollide(Collider2D coll)
  {
    if (coll.tag == "Fighter" && coll.name != "Player")
    {
      Damage dmg = new Damage()
      {
        damageAmount = damagePoint * (int)(isThrown ? spinSpeedMultiplier : 1),
        origin = transform.position,
        pushForce = pushForce
      };
      coll.SendMessage("ReceiveDamage", dmg);
      isThrown = false;
      ReturnToPlayer();
    }
  }
  
  private void Swing()
  {
    // Trigger animation for swinging the sword
    Debug.Log("Swing");
  }

  private void Throw()
  {
    // Set the position of the weapon to the mouse pointer
    Vector3 throwDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
    transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) - throwDirection;
  }
  
  private void ReturnToPlayer()
  {
    // Return the weapon back to the player
    transform.position = Player.instance.transform.position;
  }
}
