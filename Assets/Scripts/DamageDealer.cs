using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] int damage = 100;
    // Start is called before the first frame update
   public int GetDamage()
    {
        return this.damage;
    }
    public void Hit()
    {
        if(tag=="Enemy")
        {
            GetComponent<Enemy>().destroyEnemy();
            
        }
        else if (tag != "Player")
        {
          
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
      //  collision.gameObject.GetComponent<Enemy>().
    }
}
