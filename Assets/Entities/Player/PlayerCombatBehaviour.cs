using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatBehaviour : MonoBehaviour
{
    public string attackButton = "Fire1";
    public string changeWeaponKey = "e";
    public AudioSource meleeSound;
    public AudioSource gunshotSound;
    public AudioSource hitSound;
    public TransformVar sceneTransition;

    private enum states { Attack, Idle}
    private states state;
    private enum Weapons { Knife, Gun };
    private Weapons equippedWeapon;

    public float playerSize;
    public float attackTime = 0.5f;
    private float attackCounter = 0.0f;

    [Header("Health")]
    public HappyStateData happyState;
    public IntVar health;
    public bool alive;
    public Color hitColor;
    public float hitTime;
    private SpriteRenderer sprite;

    [Header("Attack Spawning")]
    public GameObject bullet;
    public Transform bulletSpawnPoint;
    public GameObject meleeAttack;


    [Header("Weapon display")]
    public GameObject knife;
    public GameObject gun;


    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        alive = true;
        state = states.Idle;
        changeWeapon(Weapons.Gun);

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
            PerformAttack();
        }
        if (Input.GetKeyDown(changeWeaponKey)) {
            changeWeapon(equippedWeapon == Weapons.Knife ? Weapons.Gun : Weapons.Knife);
        }
    }

    private void changeWeapon(Weapons w) {
        equippedWeapon = w;
        knife.SetActive(equippedWeapon == Weapons.Knife);
        gun.SetActive(equippedWeapon == Weapons.Gun);
    }
    
    void PerformAttack()
    {
        if (happyState.isHappy) {
            return;
        }

        if (equippedWeapon == Weapons.Gun)
        {
            Bullet b = Instantiate(bullet, bulletSpawnPoint.position, transform.rotation).GetComponent<Bullet>();
            b.gameObject.GetComponent<DamageSource>().type = DamageSource.damageTypes.toEnvironment;
            anim.SetTrigger("Gun");
            gunshotSound.Play();
        }
        else if (equippedWeapon == Weapons.Knife)
        {
            MeleeAttack m = Instantiate(meleeAttack).GetComponent<MeleeAttack>();
            anim.SetTrigger("Knife");
            m.transform.position = transform.position + transform.up * playerSize;
            m.transform.rotation = transform.rotation;
            m.gameObject.GetComponent<DamageSource>().type = DamageSource.damageTypes.toEnvironment;
            meleeSound.Play();
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
        }
    }

    void ApplyDamage(int amount)
    {
        health.Value -= amount;
        if (health.Value <= 0)
        {
            health.Value = 0;
            if (alive) {
                sceneTransition.Value.GetComponent<SceneTransitioner>().StartTransitionTo("GameOverScene");
            }

            alive = false;
            sprite.color = Color.black;


        } else {
            StopAllCoroutines();
            StartCoroutine(recolorDamageCoro());
        }
    }

    IEnumerator recolorDamageCoro() {
        Color prevColor = sprite.color;
        sprite.color = hitColor;
        yield return new WaitForSeconds(hitTime);
        sprite.color = Color.white;
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
            hitSound.Play();
        }
    }
}
