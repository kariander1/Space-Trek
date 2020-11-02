using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject weapon;
    [SerializeField] GameObject explosionVFX;
    [SerializeField] float health = 100;
    [SerializeField] float shotCounter = 0;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] int score=100;
    [SerializeField] int enemyCountForPowerUp = 1;

    [Header("Enemy Audio")]
    [SerializeField] AudioClip EnemyDiesSFX;
    [SerializeField] float EnemyDiesSFXVolume = 0.5f;
    [SerializeField] AudioClip EnemyHitSFX=null;
    [SerializeField] float EnemyHitSFXVolume = 0.5f;

    private GameSession gameSession;
    // Start is called before the first frame update
    void Start()
    {
         gameSession = FindObjectOfType<GameSession>();
        shotCounter = Random.Range(0, maxTimeBetweenShots);
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        if (weapon != null)
        {
            shotCounter -= Time.deltaTime;
            if (shotCounter <= 0f)
            {
                Fire();
                shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
            }
        }
    }

    private void Fire()
    {
        Instantiate(weapon, transform.position, Quaternion.identity);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Weapon weapon = collision.gameObject.GetComponent<Weapon>();
     

            DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
            damageDealer.Hit();
            ProcessHit(damageDealer);

    }
    public void destroyEnemy()
    {
        PowerUp power = this.gameSession.EnemyDestroyed(this.enemyCountForPowerUp);
        if(power != null)
        {
            Instantiate(power, transform.position, Quaternion.identity);
        }
        FindObjectOfType<GameSession>().AddToScore(this.score);
        GameObject explosion = Instantiate(explosionVFX, transform.position, transform.rotation);
        Destroy(explosion, 1f);
        AudioSource.PlayClipAtPoint(this.EnemyDiesSFX, Camera.main.transform.position, this.EnemyDiesSFXVolume);
        Destroy(gameObject);
    }
    private void ProcessHit(DamageDealer damageDealer)
    {
        this.health -= damageDealer.GetDamage();
        if (this.health <= 0)
        {
            destroyEnemy();
        }
        else
        {
            AudioSource.PlayClipAtPoint(this.EnemyHitSFX, Camera.main.transform.position, this.EnemyHitSFXVolume);
        }
    }

    
}
