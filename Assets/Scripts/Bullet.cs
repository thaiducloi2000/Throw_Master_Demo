using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float gravity = 9.8f;
    public float bounceForce = 5f;
    [SerializeField] private Rigidbody2D rigidbody;

    private void Start()
    {
        this.rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            BounceBullet(collision.contacts[0].normal);
        }
        if (collision.gameObject.CompareTag("Target"))
        {
            StartCoroutine(DestroyBulletAfterDelay(1f, collision.gameObject));
            BounceBullet(collision.contacts[0].normal);
        }
    }

    void BounceBullet(Vector2 collisionNormal)
    {
        Vector2 newDirection = Vector2.Reflect(rigidbody.velocity.normalized, collisionNormal);
        rigidbody.velocity = newDirection * bounceForce;
        StartCoroutine(DestroyBulletAfterDelay(2f,this.gameObject));
    }

    IEnumerator DestroyBulletAfterDelay(float delay,GameObject obj)
    {
        yield return new WaitForSeconds(delay);
        obj.SetActive(false);
    }
}
