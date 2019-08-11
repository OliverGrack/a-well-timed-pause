using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class DevilEnemy : MonoBehaviour {
    public HappyStateData happyState;
    public TransformVar player;
    public float focusRange;

    public List<Transform> deleportPoints;

    private Enemy enemy;
    
    public GameObject bullet;
    public List<GameObject> eyes;

    public bool isPaused = true;

    public float shootEyeInbetween = 0.3f;
    public float afterShootingTimeout = 0.6f;
    public float afterDeleportTimeout = 0.2f;

    private IEnumerator Start() {
        enemy = GetComponent<Enemy>();

        yield return null;
        yield return waitIfPaused();


        while(true) {
            foreach(var e in eyes)
            {
                Instantiate(bullet, e.transform.position, e.transform.rotation);
                e.SetActive(false);
                yield return new WaitForSeconds(shootEyeInbetween);
            }
            yield return new WaitForSeconds(afterShootingTimeout);
            
            transform.position = deleportPoints[Random.Range(0, deleportPoints.Count)].position;

            yield return new WaitForSeconds(afterDeleportTimeout);
            foreach (var e in eyes)
            {
                e.SetActive(true);
            }
        }
    }

    IEnumerator waitIfPaused()
    {
        while(isPaused)
        {
            yield return null;
        }
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
