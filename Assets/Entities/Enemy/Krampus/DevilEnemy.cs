using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class DevilEnemy : MonoBehaviour {
    public HappyStateData happyState;
    public TransformVar player;
    public float movementSpeed;


    public float focusRange; // Maximum distance to the player, until the spider wants to attack him.
    public float attackDistance;   // the distance the fly wants to be in, to start shooting at the player
    private bool inAttackRange; // true if distance to Player < attackRange
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

    public bool isPaused = true;

    private void Start() {
        enemy = GetComponent<Enemy>();
        inAttackRange = false;
        state = states.Move;
        facing = new Vector3(0, 1, 0);
    }

    void Update() {
        isPaused = (
            (player.Value == null || !player.Value.GetComponent<PlayerCombatBehaviour>().alive) ||
            (happyState.isHappy || DistanceToPlayer() > focusRange)
        );
        if (enemy.health <= 0) // Need to keep method for death events (spawn more enemies, or smth)
        {
            Destroy(gameObject);
        }
    }

    private float DistanceToPlayer() {
        Vector3 delta = new Vector3(player.Value.position.x - transform.position.x, player.Value.position.y - transform.position.y, 0);
        return Mathf.Sqrt(delta.sqrMagnitude);
    }
}
