using UnityEngine;
using System.Collections;

public class EnemyFireController : MonoBehaviour {

    protected float firingDelay;
    protected float fireTime;
    public GameObject bulletPrefab;
    new Rigidbody2D rigidbody2D;

    // Use this for initialization
    void Awake() {
        firingDelay = Random.value * GameController.instance.FiringDelay();
        fireTime = Time.time;
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update() {
        if (Time.time > fireTime + firingDelay) {
            if (GameController.instance.CanHaveMoreBullets()) {
                Fire();
            }
            firingDelay = Random.value * GameController.instance.FiringDelay();
            fireTime = Time.time;
        }
    }

    protected void Fire() {
        Debug.Log("Fire");
        if (GetComponent<Rigidbody2D>().simulated) {
            GameObject bullet = Instantiate(bulletPrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            bulletRb.velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0);
            GameController.instance.BulletFired();
        }
    }
}
