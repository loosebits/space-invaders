using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
    Animator animator;
    new Rigidbody2D rigidbody2D;
    int direction = 1;
    public GameObject bulletPrefab;
    public AudioClip boom;
    float firingDelay;
    float fireTime;
    AudioSource audioSource;

    // Use this for initialization
    void Awake() {

        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        GameObject go = Instantiate(bulletPrefab);
        firingDelay = Random.value * GameController.instance.FiringDelay();
        fireTime = Time.time;
        GameController.instance.ShipCreated();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        if (Time.time > fireTime + firingDelay) {
            Fire();
            firingDelay = Random.value * GameController.instance.FiringDelay();
            fireTime = Time.time;
        }
    }

	private void FixedUpdate() {
        Vector2 position = rigidbody2D.position;
        position.x += 3.0f * direction * Time.deltaTime;
        rigidbody2D.MovePosition(position);
	}

	private void OnCollisionEnter2D(Collision2D collision) {
        BulletController bulletController = collision.collider.GetComponent<BulletController>();
        if (bulletController != null) {
            rigidbody2D.simulated = false;
            animator.SetTrigger("Hit");
            audioSource.PlayOneShot(boom);
            GameController.instance.ShipDestroyed();
        } else {
			direction *= -1;
        }
    }

    void Fire() {
        Debug.Log("Fire");
        if (GameController.instance.CanHaveMoreBullets() && rigidbody2D.simulated) {
            GameObject bullet = Instantiate(bulletPrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            bulletRb.velocity = new Vector2(rigidbody2D.velocity.x, 0);
            GameController.instance.BulletFired();
        }
    }


}
