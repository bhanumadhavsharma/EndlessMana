using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject
        hitExplosion;

    public int effect;

    bool readyToRemove;

    private void Awake()
    {
        
    }

    private void Update()
    {
        if (readyToRemove)
        {
            Destroy(gameObject, 2f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.GetComponent<Bullet>())
        {
            GameObject explosionSprite = Instantiate(hitExplosion, transform.position, Quaternion.identity);
            if (collision.gameObject.GetComponent<Enemy>() != null)
            {
                collision.gameObject.GetComponent<Enemy>().OnHit(effect);
            }
            else if (collision.gameObject.GetComponent<ShootingEnemy>() != null)
            {
                collision.gameObject.GetComponent<ShootingEnemy>().OnHit(effect);
            }
            else if (collision.gameObject.GetComponent<ShootAndMoveEnemy>() != null)
            {
                collision.gameObject.GetComponent<ShootAndMoveEnemy>().OnHit(effect);
            }
            Destroy(explosionSprite, 2f);
            Destroy(gameObject);
        }

        if (collision.gameObject.GetComponent<Bullet>())
        {
            Destroy(gameObject);
        }
    }

    private void OnBecameVisible()
    {
        readyToRemove = true;
    }

    private void OnBecameInvisible()
    {
        if (readyToRemove)
        {
            Destroy(gameObject);
        }
    }
}
