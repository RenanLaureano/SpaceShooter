using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int damage;
    private Rigidbody2D rigidbody2D;

    public void Init(int damage, int force)
    {
        this.damage = damage;

        rigidbody2D = GetComponent<Rigidbody2D>();
        Vector2 _force = new Vector2 (0,(transform.forward * force).z);
        rigidbody2D.AddForce(_force);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(gameObject.tag == collision.gameObject.tag)
        {
            return;
        }

        collision.gameObject.SendMessage("HitDamage", damage, SendMessageOptions.DontRequireReceiver);
        Destroy(gameObject);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
