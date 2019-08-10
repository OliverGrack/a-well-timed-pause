using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Enemy))]
public class SpiderEnemy : MonoBehaviour {


    public HappyStateData happyState;
    public TransformVar player;
    public float movementSpeed;
    public float enemySize;


    public float attackRange;   // range of the attacks. 
                                // The spider considers itself in range, when the 
                                // distance to the player is smaller than half of that range
    private bool inAttackRange; // true if distance to Player < attackRange
    public float inRangeCounter = 0.0f;     // time since the enemy is in range
    public float inRangeTime;   // time the enemy must be in range to start attacking

    private SpriteRenderer sprite;
    // private Rigidbody2D rigid;
    private Enemy enemy;

    private enum states { Attack, Move }
    private states state;
    public float attackTime = 0.3f;
    private float attackCounter = 0.0f;
    public GameObject meleeAttack;
    private float playerSize;
    public int damage;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        // rigid = GetComponent<Rigidbody2D>();

        enemy = GetComponent<Enemy>();
        inAttackRange = false;
        state = states.Move;
        playerSize = player.Value.gameObject.GetComponent<PlayerBehaviour>().playerSize;
        meleeAttack.GetComponent<DamageSource>().damageAmount = damage;
    }

    void Update() {
        if (player.Value == null || !player.Value.GetComponent<PlayerBehaviour>().alive) return;

        if (happyState.isSad) {
            UpdateLookDirection();
            UpdateMovement();
            UpdateAttack();
        }

        if(enemy.health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void UpdateLookDirection()
    {
        Vector3 diff = (player.Value.position) - transform.position;
        diff.Normalize();

        float rotZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ - 90);
    }

    void UpdateMovement()
    {
        if(state == states.Move)
        {
            float deltaX2 = Mathf.Pow(player.Value.position.x - transform.position.x, 2);
            float deltaY2 = Mathf.Pow(player.Value.position.y - transform.position.y, 2);
            float delta = Mathf.Sqrt(deltaX2 + deltaY2);
            if (delta - playerSize - enemySize > attackRange / 2) // if out of range, move towards player
            {
                inAttackRange = false;
                inRangeCounter = 0f;
                transform.position = transform.position + transform.up * movementSpeed * Time.deltaTime;
            } else // attack Player
            {
                inAttackRange = true;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        DamageSource dmg = col.gameObject.GetComponent<DamageSource>();
        if (dmg != null && dmg.type != DamageSource.damageTypes.toPlayer)
        {
            enemy.ApplyDamage(dmg.damageAmount);
            Debug.Log("Enemy Health: " + enemy.health);
        }
    }

    void PerformAttack()
    {
        Debug.Log("perform Attack");
        sprite.color = Color.magenta;
        MeleeAttack m = Instantiate(meleeAttack).GetComponent<MeleeAttack>();
        m.transform.position = transform.position + transform.up * enemySize;
        m.transform.rotation = transform.rotation;
        m.range = attackRange;
        m.gameObject.GetComponent<DamageSource>().type = DamageSource.damageTypes.toPlayer;
    }

    void UpdateAttack()
    {
        if(inAttackRange && attackCounter <= 0)
        {
            inRangeCounter += Time.deltaTime;
        }
        if(attackCounter <= 0 && inRangeCounter > inRangeTime)
        {
            Debug.Log("in range");
            state = states.Attack;
            attackCounter = attackTime;
            PerformAttack();
        }

        if (attackCounter > 0f)
        {
            attackCounter -= Time.deltaTime;
        }
        if (attackCounter < 0f)
        {
            sprite.color = Color.red;
            attackCounter = 0;
            state = states.Move;
        }
    }
}
