using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyFireball : MonoBehaviour
{
    [SerializeField]
    private float
        speed = 8f;
    [SerializeField]
    GameObject
        hitExplosion;
    public
        int effect;

    private Rigidbody2D rb;
    bool readyToRemove = false;
    [SerializeField]
    private LayerMask
        whatIsPlayer,
        whatIsWall;
    Collider2D
        hitPlayer,
        hitWall;
    float hitRadius = .2f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //        rb.MovePosition(transform.TransformPoint(Vector3.right * speed * Time.deltaTime));
        //        bulletRB.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);

        CheckEnvironment();
        EffectOnTarget();
        if (readyToRemove)
        {
            Destroy(gameObject, 2f);
        }
    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.GetComponent<Bullet>())
        {
            //GameObject explosionSprite = Instantiate(hitExplosion, transform.position, Quaternion.identity);
            if (collision.gameObject.GetComponent<PlayerStats>() != null)
            {
                Debug.Log("hit player");
                collision.gameObject.GetComponent<PlayerStats>().OnHit(effect);
            }
            
            if (collision.gameObject.CompareTag("Wall"))
            {
                Destroy(gameObject);
            }
            
            //Destroy(explosionSprite, 2f);
            Destroy(gameObject);
        }
    } */

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

    void CheckEnvironment()
    {
        hitPlayer = Physics2D.OverlapCircle(transform.position, hitRadius, whatIsPlayer);
        hitWall = Physics2D.OverlapCircle(transform.position, hitRadius, whatIsWall);
    }

    void EffectOnTarget()
    {
        if(hitWall != null)
        {
            Destroy(gameObject);
        }
        if (hitPlayer != null)
        {
            PlayerStats.instance.OnHit(effect);
            Destroy(gameObject);
        }
    }

    public void SetBulletSpeed(float value)
    {
        speed = value;
    }
}
