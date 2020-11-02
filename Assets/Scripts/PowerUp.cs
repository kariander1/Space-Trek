using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] float VelocityY = -1f;
    [SerializeField] float AngularVelocity = 180f;
    [Header("SFX")]
    [SerializeField] AudioClip GenerateSFX;
    [SerializeField] AudioClip GainSFX;
    // Start is called before the first frame update
    void Start()
    {
        AudioSource.PlayClipAtPoint(this.GenerateSFX, Camera.main.transform.position);
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, VelocityY);
        GetComponent<Rigidbody2D>().angularVelocity = this.AngularVelocity;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

      
        Player p = collision.gameObject.GetComponent<Player>();
        if (p != null)
        {
            AudioSource.PlayClipAtPoint(this.GainSFX, Camera.main.transform.position);
            p.PowerUp();
            Destroy(gameObject);
        }
    }

}
