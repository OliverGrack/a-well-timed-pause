using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatBehaviour : MonoBehaviour
{
    public string attackButton = "Fire1";
    public string changeWeaponKey = "e";

    private enum states { Attack, Idle}
    private states state;
    private enum Weapons { Knife, Gun };
    private Weapons equippedWeapon;

    public float playerSize;
    public float attackTime = 0.5f;
    private float attackCounter = 0.0f;

    public GameObject bullet;
    public GameObject meleeAttack;

    private SpriteRenderer sprite;

    public IntVar health;
    public bool alive;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        alive = true;
        state = states.Idle;
        equippedWeapon = Weapons.Gun;

        sprite = GetComponent<SpriteRenderer>();
        health.Reset();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(alive)
        {
            checkInput();
            UpdateAttack();
        }
    }

    void checkInput()
    {
        if(Input.GetButtonDown(attackButton) && state == states.Idle)
        {
            state = states.Attack;
            this.attackCounter = attackTime;
            sprite.color = Color.blue;
            PerformAttack();
        }
        if (Input.GetKeyDown(changeWeaponKey))
        {
            equippedWeapon = equippedWeapon == Weapons.Knife ? Weapons.Gun : Weapons.Knife;
        }
    }
    
    void PerformAttack()
    {
        if (equippedWeapon == Weapons.Gun)
        {
            Bullet b = Instantiate(bullet).GetComponent<Bullet>();
            b.transform.position = transform.position + transform.up * playerSize;
            b.transform.rotation = transform.rotation;
            b.gameObject.GetComponent<DamageSource>().type = DamageSource.damageTypes.toEnvironment;
        }
        else if (equippedWeapon == Weapons.Knife)
        {
            MeleeAttack m = Instantiate(meleeAttack).GetComponent<MeleeAttack>();
            anim.SetTrigger("Knife");
            m.transform.position = transform.position + transform.up * playerSize;
            m.transform.rotation = transform.rotation;
            m.gameObject.GetComponent<DamageSource>().type = DamageSource.damageTypes.toEnvironment;
        }

    }

    void UpdateAttack()
    {
        if(attackCounter > 0f)
        {
            attackCounter -= Time.deltaTime;
        }
        if(attackCounter < 0f)
        {
            attackCounter = 0;
            state = states.Idle;
            sprite.color = Color.white;
        }
    }

    void ApplyDamage(int amount)
    {
        health.Value -= amount;
        if (health.Value <= 0)
        {
            health.Value = 0;
            alive = false;
            sprite.color = Color.black;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        checkDamageTrigger(col);
    }
    void checkDamageTrigger(Collider2D col)
    {
        DamageSource dmg = col.gameObject.GetComponent<DamageSource>();
        if (alive && dmg != null && dmg.type != DamageSource.damageTypes.toEnvironment)
        {
            ApplyDamage(dmg.damageAmount);
            Debug.Log("Health: " + health.Value);
        }
    }

}
