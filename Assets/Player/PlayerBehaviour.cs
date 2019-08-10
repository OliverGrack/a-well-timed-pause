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

    // Start is called before the first frame update
    void Start()
    {
        state = states.Idle;
        equippedWeapon = Weapons.Gun;

        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        checkInput();
        updateAttack();
    }

    void checkInput()
    {
        if(Input.GetButtonDown("Fire1") && state == states.Idle)
        {
            state = states.Attack;
            this.attackCounter = attackTime;
            sprite.color = Color.blue;
            performAttack();
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
    
    void performAttack()
    {
        if (equippedWeapon == Weapons.Gun)
        {
            Bullet b = Instantiate(bullet).GetComponent<Bullet>();
            b.transform.position = transform.position + transform.up * playerSize;
            b.transform.rotation = transform.rotation;
        }
        else if (equippedWeapon == Weapons.Knife)
        {
            MeleeAttack m = Instantiate(meleeAttack).GetComponent<MeleeAttack>();
            m.transform.position = transform.position + transform.up * playerSize;
            m.transform.rotation = transform.rotation;
        }

    }

    void updateAttack()
    {
        if(attackCounter > 0f)
        {
            attackCounter -= Time.deltaTime;
        }
        if(attackCounter < 0f)
        {
            this.attackCounter = 0;
            this.state = states.Idle;
            sprite.color = Color.white;
        }
    }
}
