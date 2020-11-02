using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{

    //[SerializeField] private float screenWidthInUnits = 16f;
     float minXPos ;
    float maxXPos;
    float minYPos;
    float maxYPos;

    [Header("Player")]
    [SerializeField] int health = 200;
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float topMovementCap = 0.3f;
    [SerializeField] float padding = 0.5f;
    [SerializeField] bool CLAMP = true;
    [SerializeField] GameObject explosionVFX;

    [Header("Player Projectile")]
    [SerializeField] GameObject weapon;
    [SerializeField] int powerLevel = 1;
    [SerializeField] float projectileFiringPeriod = 0.5f;

    [SerializeField] bool DEBUG = true;


    Coroutine firingCoroutine = null;

    [Header("Player Audio")]
    [SerializeField] AudioClip PlayerHitSFX;
    [SerializeField] float PlayerHitSFXVolume = 0.5f;
    [SerializeField] AudioClip PlayerDiesSFX;
    [SerializeField] float PlayerDiesSFXVolume=0.5f;

    // Start is called before the first frame update
    void Start()
    {
      //  laser = FindObjectOfType<Laser>();

        SetUpMoveBoundaries();
    }
    // Update is called once per frame

    void Update()
    {
        Move();
        Fire();
    }
    public void PowerUp()
    {
        this.powerLevel++;
    }
    public int GetHealth()
    {
        return this.health;
    }
    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        this.minXPos = gameCamera.ViewportToWorldPoint(new Vector3(0,0)).x+padding;
        this.maxXPos = gameCamera.ViewportToWorldPoint(new Vector3(1, 0)).x-padding;

        this.minYPos = gameCamera.ViewportToWorldPoint(new Vector3(0, 0)).y+padding;
        this.maxYPos = gameCamera.ViewportToWorldPoint(new Vector3(0f, topMovementCap)).y;
    }
    IEnumerator FireContinously()
    {
        while (true)
        {
            switch ((weapon.tag))
            {
                case "Laser":
                    handleLaserFire();
                    break;
            }

            yield return new WaitForSeconds(this.projectileFiringPeriod);
        }
     
    }
    private void handleLaserFire()
    {
      
        switch(this.powerLevel)
        {
            
            case 1:
                Instantiate(weapon, transform.position, Quaternion.identity);
                break;
            case 2:
                Instantiate(weapon, new Vector2(transform.position.x +Weapon.WeaponSpacing/2, transform.position.y), Quaternion.identity);
                Instantiate(weapon, new Vector2(transform.position.x - Weapon.WeaponSpacing / 2, transform.position.y), Quaternion.identity);
                break;
            case 3:
                Instantiate(weapon, transform.position, Quaternion.identity);
                Instantiate(weapon, new Vector2(transform.position.x + Weapon.WeaponSpacing, transform.position.y), Quaternion.identity);
                Instantiate(weapon, new Vector2(transform.position.x - Weapon.WeaponSpacing, transform.position.y), Quaternion.identity);
                break;
            case 4:
                Instantiate(weapon, new Vector2(transform.position.x + Weapon.WeaponSpacing / 2, transform.position.y), Quaternion.identity);
                Instantiate(weapon, new Vector2(transform.position.x - Weapon.WeaponSpacing / 2, transform.position.y), Quaternion.identity);
                Instantiate(weapon, new Vector2(transform.position.x + Weapon.WeaponSpacing / 2, transform.position.y), Quaternion.Euler(0,0,-5));
                Instantiate(weapon, new Vector2(transform.position.x - Weapon.WeaponSpacing / 2, transform.position.y), Quaternion.Euler(0, 0, 5));
                break;
        }
    }
    private void Fire()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            if(firingCoroutine==null)
                firingCoroutine =  StartCoroutine(FireContinously());
          
            
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            if(firingCoroutine!=null)
            {
                StopCoroutine(firingCoroutine);

                firingCoroutine = null;
            }

        }
    }
    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime* moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        var newXPos = transform.position.x + deltaX;
        var newYPos = transform.position.y + deltaY;

        if(DEBUG)
        {
            Debug.Log("X : " + transform.position.x + " New X : " + newXPos);
            Debug.Log("Y : " + transform.position.y + " New Y : " + newYPos);
        }
        if(CLAMP)
        {
            newXPos = Mathf.Clamp(newXPos,this.minXPos,this.maxXPos);
            newYPos = Mathf.Clamp(newYPos, this.minYPos, this.maxYPos);
        }
      

        transform.position = new Vector2(newXPos, newYPos);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "PowerUp")
        {
            Weapon weapon = collision.gameObject.GetComponent<Weapon>();
            DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
            damageDealer.Hit();
            ProcessHit(damageDealer);
        }

    }
    private void ProcessHit(DamageDealer damageDealer)
    {
        this.health -= damageDealer.GetDamage();
        if (this.health <= 0)
        {
            DestroyPlayer();
        }
        else
        {
            AudioSource.PlayClipAtPoint(this.PlayerHitSFX, Camera.main.transform.position, this.PlayerHitSFXVolume);
        }
    }
    private void DestroyPlayer()
    {
        GameObject explosion = Instantiate(explosionVFX, transform.position, transform.rotation);
        Destroy(explosion, 3f);
        AudioSource.PlayClipAtPoint(this.PlayerDiesSFX, Camera.main.transform.position, this.PlayerDiesSFXVolume);
        Destroy(gameObject);
        FindObjectOfType<Level>().LoadGameOver();
        
    }
}
