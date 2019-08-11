using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Enemy))]
public class FlyEnemy : MonoBehaviour
{
    public HappyStateData happyState;
    public TransformVar player;
    public float movementSpeed;


    public float attackDistance;   // the distance the fly wants to be in, to start shooting at the player
    private bool inAttackRange; // true if distance to Player < attackRange
    public float inRangeCounter = 0.0f;     // time since the enemy is in range
    public float inRangeTime;   // time the enemy must be in range to start attacking
    private Vector3 facing;

    // private SpriteRenderer sprite;
    // private Rigidbody2D rigid;
    private Enemy enemy;

    private enum states { Attack, Move }
    private states state;
    public float attackTime = 0.3f;
    private float attackCounter = 0.0f;
    public GameObject bullet;
    public int damage;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
        inAttackRange = false;
        state = states.Move;
        bullet.GetComponent<DamageSource>().damageAmount = damage;
        facing = new Vector3(0, 1, 0);
    }

    void Update()
    {
        if (player.Value == null || !player.Value.GetComponent<PlayerCombatBehaviour>().alive)
        {
            return;
        }

        if (happyState.isSad)
        {
            UpdateLookDirection();
            UpdateMovement();
            UpdateAttack();
        }

        if(enemy.health <= 0) // Need to keep method for death events (spawn more enemies, or smth)
        {
            Destroy(gameObject);
        }
    }

    void UpdateLookDirection()
    {
        facing = (player.Value.position - transform.position);
        facing.Normalize();
    }

    void UpdateMovement()
    {
        if (state == states.Move)
        {
            Vector3 delta = player.Value.position - transform.position;
            float deltaM = Mathf.Sqrt(delta.sqrMagnitude);
            if (deltaM > attackDistance * 0.7) // if out of range, move towards player
            {
                inAttackRange = false;
                inRangeCounter = 0f;
                transform.position = transform.position + facing * Time.deltaTime;
            }
            else // attack Player
            {
                inAttackRange = true;
            }
        }
    }

    void PerformAttack()
    {
        Bullet b = Instantiate(bullet).GetComponent<Bullet>();
        b.transform.position = transform.position + facing * enemy.size;

        float rotZ = Mathf.Atan2(facing.y, facing.x) * Mathf.Rad2Deg;
        b.transform.rotation = Quaternion.Euler(0f, 0f, rotZ - 90);

        b.gameObject.GetComponent<DamageSource>().type = DamageSource.damageTypes.toPlayer;
    }

    void UpdateAttack()
    {
        if (inAttackRange && attackCounter <= 0)
        {
            inRangeCounter += Time.deltaTime;
        }
        if (attackCounter <= 0 && inRangeCounter > inRangeTime)
        {
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
            // sprite.color = Color.red;
            attackCounter = 0;
            state = states.Move;
        }
    }
}
