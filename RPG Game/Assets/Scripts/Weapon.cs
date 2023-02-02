using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{
  public int damagePoint = 1;
  public float pushForce = 10.0f;
  
  public int weaponLevel = 0;
  private SpriteRenderer spriteRenderer;
  private Animator anim;
  private float cooldown = 0.3f;
  private float lastSwing;
private float lastThrow;
private float throwCooldown = 2.5f;


  private Camera _cameraMain;
  
  
  
  protected override void Start()
  {
    _cameraMain = Camera.main;
    base.Start();
    spriteRenderer = GetComponent<SpriteRenderer>();
    anim = GetComponent<Animator>();
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
    
    if (Input.GetKeyDown(KeyCode.T))
    {
      if (Time.time - lastThrow > throwCooldown)
      {
        lastThrow = Time.time;
        Throw(); 
      }
  
    }
  }
  
  protected override void OnCollide(Collider2D coll)
  {
    if (coll.tag == "Fighter" && coll.name != "Player")
    {
      Damage dmg = new Damage()
      {
        origin = transform.position,
        pushForce = pushForce
      };
      coll.SendMessage("ReceiveDamage", dmg);
    }
  }
  
  private void Swing()
  {
    anim.SetTrigger("Swing");
  }

  private void Throw()
  {
anim.SetTrigger("Throw");
  }
  
  
}
