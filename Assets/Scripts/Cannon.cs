using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Cannon : MonoBehaviour
{
    //Bullet Pool
    [SerializeField] private List<GameObject> bullets;
    [SerializeField] private GameObject bullet;
    public Transform bulletSpawnPoint;
    public float minBulletForce = 10f; 
    public float maxBulletForce = 20f;
    public float forceMultiplier = 10f;
    public float chargeTime = 5f;
    private float currentChargeTime = 0f;
    private bool isCharging = false;

    private void Awake()
    {
        for(int i = 0; i < 5; i++)
        {
            GameObject blt = Instantiate(bullet);
            blt.transform.position = this.bulletSpawnPoint.position;
            this.bullets.Add(blt);
            blt.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isCharging = true;
            currentChargeTime = 0f;
        }

        if (Input.GetMouseButton(0) && isCharging)
        {
            currentChargeTime += Time.deltaTime;
        }

        if (Input.GetMouseButtonUp(0) && isCharging)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            ShootBullet(currentChargeTime, mousePosition);
            isCharging = false;
        }
    }

    void ShootBullet(float chargeTime,Vector3 mousePosition)
    {
        float bulletForce = minBulletForce + chargeTime * forceMultiplier;

        GameObject bullet = GetBulletFromPool();
        if (bullet != null)
        {
            bullet.transform.position = bulletSpawnPoint.position;

            Vector2 direction = mousePosition - bulletSpawnPoint.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            bullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            bullet.SetActive(true);
            Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
            bulletRigidbody.gravityScale = 0f;
            bulletRigidbody.AddForce(bullet.transform.right * bulletForce, ForceMode2D.Impulse);
        }
    }


    public GameObject GetBulletFromPool()
    {
        for (int i = 0; i < bullets.Count; i++)
        {
            if (!bullets[i].activeInHierarchy)
            {
                bullets[i].SetActive(true);
                return bullets[i];
            }
        }
        return null;
    }

    public void ReturnBulletToPool(GameObject bullet)
    {
        bullet.SetActive(false);

        StartCoroutine(DestroyBulletAfterDelay(bullet, 2f));
    }

    IEnumerator DestroyBulletAfterDelay(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        bullets.Remove(bullet);
        Destroy(bullet);
    }
}
