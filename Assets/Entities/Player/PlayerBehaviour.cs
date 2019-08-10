using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
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

    public int health;
    public bool alive;

    // Start is called before the first frame update
    void Start()
    {
        alive = true;
        state = states.Idle;
        equippedWeapon = Weapons.Gun;

        sprite = GetComponent<SpriteRenderer>();
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
        if(Input.GetButtonDown("Fire1") && state == states.Idle)
        {
            state = states.Attack;
            this.attackCounter = attackTime;
            sprite.color = Color.blue;
            PerformAttack();
        }
        if (Input.GetKeyDown("1"))
        {
            equippedWeapon = Weapons.Knife;
            Debug.Log("Knife");
        } else if (Input.GetKeyDown("2"))
        {
            equippedWeapon = Weapons.Gun;
            Debug.Log("Gun");
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
        health -= amount;
        if (health <= 0)
        {
            health = 0;
            alive = false;
            sprite.color = Color.black;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        DamageSource dmg = col.gameObject.GetComponent<DamageSource>();
        if (alive && dmg != null && dmg.type != DamageSource.damageTypes.toEnvironment)
        {
            ApplyDamage(dmg.damageAmount);
            Debug.Log("Health: "+health);
        }
    }
}
