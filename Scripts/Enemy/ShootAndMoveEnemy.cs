using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAndMoveEnemy : MonoBehaviour
{
    public int
        playerDamage,
        damageDoneToPlayer,
        xpAdded;

    [SerializeField]
    private float
        scanRadius = 10f;
    [SerializeField]
    private LayerMask
        whatIsPlayer;
    [SerializeField]
    private GameObject
        fireball;
    [SerializeField]
    Transform firePoint;
    [SerializeField]
    float fireDelay = .75f;
    [SerializeField]
    float bulletSpeed;
    [SerializeField]
    float
        force = 10f;
    [SerializeField]
    ParticleSystem
        shootingParticle;

    public float
        moveSpeed = 2f;
    Vector2 movementDirection;
    Rigidbody2D rb;

    public GameObject
        frozenImage;
    public int
        health = 5;
    bool
        knowsAboutPlayer = false,
        isFrozen = false;
    private Collider2D
        target;

    /// <summary>
    /// MAIN FUNCTIONS
    /// </summary>

    private void Start()
    {
        InvokeRepeating("Fire", 0f, fireDelay);
        knowsAboutPlayer = false;
        isFrozen = false;
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!isFrozen)
        {
            CheckEnvironment();
            LookAtTarget();
        }
    }

    private void FixedUpdate()
    {
        if (!isFrozen)
        {
            Move(movementDirection);
        }
    }
    
    /// <summary>
    /// 
    /// </summary>

    private void CheckEnvironment()
    {
        target = Physics2D.OverlapCircle(transform.position, scanRadius, whatIsPlayer);
    }

    private void Move(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.fixedDeltaTime));
        //Debug.Log(rb.position);
    }

    private void LookAtTarget()
    {
        if (target != null)
        {
            if (knowsAboutPlayer == false)
            {
                knowsAboutPlayer = true;
                this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            }
            Vector2 direction = target.transform.position - transform.position;
            direction.Normalize();
            movementDirection = direction;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg -90f; // -90f is used for when the sprite is rotated the wrong way
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward); 
            //vector3.forward is z axis, so this will rotate angle amount around z-axis
        }
    }

    void Fire()
    {
        if (target != null && !isFrozen)
        {
            Instantiate(shootingParticle, firePoint.position, firePoint.rotation);
            GameObject bullet = Instantiate(fireball, firePoint.position, firePoint.rotation);
            bullet.gameObject.GetComponent<enemyFireball>().SetBulletSpeed(bulletSpeed);
            bullet.gameObject.GetComponent<enemyFireball>().effect = damageDoneToPlayer;
            Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
            bulletRB.AddForce(firePoint.up * force, ForceMode2D.Impulse);
        }
    }

    public void OnHit(int efffect)
    {
        playerDamage = PlayerStats.instance.damage;
        switch (efffect)
        {
            case 1:
                Damage(playerDamage + 1);
                break;
            case 2:
                Damage(playerDamage);
                Freeze();
                break;
            default:
                break;
                //nothing
        }
    }

    public void Damage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        PlayerStats.instance.AddXP(xpAdded);
        Destroy(gameObject);
    }


    public void Freeze()
    {
        isFrozen = true;
        //Vector2 tempPosition = transform.position;
        //rb.MovePosition(tempPosition);
        //rb.velocity = new Vector2(0f, 0f);
        Quaternion tempRotation = transform.rotation;
        transform.rotation = tempRotation;
        frozenImage.SetActive(true);
        rb.constraints = RigidbodyConstraints2D.FreezePosition;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        StartCoroutine(UnFreeze());
    }

    IEnumerator UnFreeze()
    {
        yield return new WaitForSecondsRealtime(2);
        isFrozen = false;
        frozenImage.SetActive(false);
        rb.constraints = RigidbodyConstraints2D.None;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, scanRadius);
    }
}
