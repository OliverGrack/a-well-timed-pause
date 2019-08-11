using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private const int bulletSpeed = 10;
    private Camera mainCamera;
    private DamageSource dmg;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        dmg = gameObject.GetComponent<DamageSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + transform.up * bulletSpeed * Time.deltaTime;

        // check if it should be destroyed
        Vector2 screenPos = mainCamera.WorldToScreenPoint(transform.position);
        if(screenPos.x > Screen.width*1.1 || screenPos.x < Screen.width*-0.1 || screenPos.y > Screen.height * 1.1 || screenPos.y < Screen.height * -0.1)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if(dmg.type == DamageSource.damageTypes.toPlayer)
        {
            PlayerCombatBehaviour player = col.gameObject.GetComponent<PlayerCombatBehaviour>();
            if (player != null)
            {
                Destroy(gameObject);
            }

        }
        if (dmg.type == DamageSource.damageTypes.toEnvironment)
        {
            Enemy enemy = col.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                Destroy(gameObject);
            }
        }
    }
}
