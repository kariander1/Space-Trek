using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] float Velocity = 10f;
    [SerializeField] float AngularVelocity = 0f;
    [SerializeField] public static float WeaponSpacing = 0.5f;
    [SerializeField] public bool enemyWeapon = false;
    [SerializeField] AudioClip FireSFX;
    [SerializeField] float FireVolume = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        if(FireSFX!=null)
            AudioSource.PlayClipAtPoint(FireSFX, Camera.main.transform.position,this.FireVolume);
        Rigidbody2D body = GetComponent<Rigidbody2D>();
        float deltaVX = Mathf.Sin(Mathf.Deg2Rad * (-transform.rotation.z*100)) * this.Velocity;
        float deltaVY = Mathf.Cos(Mathf.Deg2Rad * transform.rotation.z * 100) * this.Velocity;
        if (enemyWeapon)
            deltaVY *= -1;
        body.velocity = new Vector2(deltaVX, deltaVY);
        body.angularVelocity = this.AngularVelocity;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
