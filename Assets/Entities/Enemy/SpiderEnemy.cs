using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Enemy))]
public class SpiderEnemy : MonoBehaviour {


    public HappyStateData happyState;
    public TransformVar player;
    public float movementSpeed;

    public float focusRange; // Maximum distance to the player, until the spider wants to attack him.
    public float attackRange;   // range of the attacks. 
                                // The spider considers itself in range, when the 
                                // distance to the player is smaller than half of that range
    private bool inAttackRange; // true if distance to Player < attackRange
    public float inRangeCounter = 0.0f;     // time since the enemy is in range
    public float inRangeTime;   // time the enemy must be in range to start attacking

    // private SpriteRenderer sprite;
    // private Rigidbody2D rigid;
    private Enemy enemy;

    private enum states { Attack, Move }
    private states state;
    public float attackTime = 0.3f;
    private float attackCounter = 0.0f;
    public GameObject meleeAttack;
    private float playerSize;
    public int damage;

    private Animator animator;
    private void Start()
    {
        // sprite = GetComponent<SpriteRenderer>();
        // rigid = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();

        enemy = GetComponent<Enemy>();
        inAttackRange = false;
        state = states.Move;
        playerSize = player.Value.gameObject.GetComponent<PlayerCombatBehaviour>().playerSize;
        meleeAttack.GetComponent<DamageSource>().damageAmount = damage;
    }

    void Update() {
        if (player.Value == null || !player.Value.GetComponent<PlayerCombatBehaviour>().alive)
        {
            animator.SetBool("isMoving", false);
            return;
        }

        if (happyState.isSad && DistanceToPlayer() < focusRange ) {
            UpdateLookDirection();
            UpdateMovement();
            UpdateAttack();
        }

        if (enemy.health <= 0) // Need to keep method for death events (spawn more enemies, or smth)
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
            if (DistanceToPlayer() - playerSize - enemy.size > attackRange / 2) // if out of range, move towards player
            {
                inAttackRange = false;
                inRangeCounter = 0f;
                transform.position = transform.position + transform.up * movementSpeed * Time.deltaTime;
                animator.SetBool("isMoving", true);
            } else // attack Player
            {
                animator.SetBool("isMoving", false);
                inAttackRange = true;
            }
        }
    }

    void PerformAttack()
    {
        // sprite.color = Color.magenta;
        MeleeAttack m = Instantiate(meleeAttack).GetComponent<MeleeAttack>();
        animator.SetTrigger("Attack");
        m.transform.position = transform.position + transform.up * enemy.size;
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
            // sprite.color = Color.red;
            attackCounter = 0;
            state = states.Move;
        }
    }

    private float DistanceToPlayer()
    {
        Vector3 delta = new Vector3(player.Value.position.x - transform.position.x, player.Value.position.y - transform.position.y, 0);
        return Mathf.Sqrt(delta.sqrMagnitude);
    }
}
