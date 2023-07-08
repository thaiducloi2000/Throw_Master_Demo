using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    public float bounceForce = 1f;
    [SerializeField] private Rigidbody2D rd;

    private void Start()
    {
        this.rd = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            BounceBullet(collision.contacts[0].normal,this.gameObject);
        }
        if (collision.gameObject.CompareTag("Target"))
        {
            BounceBullet(collision.contacts[0].normal, collision.gameObject);
        }
    }

    void BounceBullet(Vector2 collisionNormal,GameObject obj)
    {
        Vector2 newDirection = Vector2.Reflect(this.rd.velocity.normalized, collisionNormal);
        this.rd.velocity = newDirection * bounceForce;
        StartCoroutine(DestroyBulletAfterDelay(2f, obj));
    }

    IEnumerator DestroyBulletAfterDelay(float delay,GameObject obj)
    {
        yield return new WaitForSeconds(delay);
        obj.SetActive(false);
    }
}
