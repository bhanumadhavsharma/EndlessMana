using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : MonoBehaviour
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

    public GameObject
        frozenImage;
    private Rigidbody2D
        rb;
    public int
        health = 7;
    bool
        knowsAboutPlayer = false,
        isFrozen = false;
    private Collider2D
        target;

    private void Start()
    {
        InvokeRepeating("Fire", 0f, fireDelay);
        knowsAboutPlayer = false;
        isFrozen = false;
        frozenImage.SetActive(false);
        rb = this.GetComponent<Rigidbody2D>();
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    private void Update()
    {
        if (!isFrozen)
        {
            CheckEnvironment();
            LookAtTarget();
        }
    }

    private void CheckEnvironment()
    {
        target = Physics2D.OverlapCircle(transform.position, scanRadius, whatIsPlayer);
    }

    private void LookAtTarget()
    {
        if(target != null)
        {
            if (knowsAboutPlayer == false)
            {
                knowsAboutPlayer = true;
                this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            }
            Vector2 direction = target.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward); // -90f; //used for when the sprite is rotated the wrong way
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
            bulletRB.AddForce(firePoint.right * force, ForceMode2D.Impulse);
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
                isFrozen = true;
                Freeze();
                //freeze
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
        rb.constraints = RigidbodyConstraints2D.FreezePosition;
    }

    public void IsFrozenIsFalse()
    {
        isFrozen = false;
        frozenImage.SetActive(false);
        rb.constraints = RigidbodyConstraints2D.None;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, scanRadius); 
    }
}
