using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
    Animator animator;
    new Rigidbody2D rigidbody2D;
    public int direction = 1;
    public GameObject bulletPrefab;
    public AudioClip boom;
    public float decentDelay = 5;
    public float decentTime;
    public float decentInterval = .01f;
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
        decentTime = Time.time;
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
        //Vector2 position = rigidbody2D.position;
        //position.x += 3.0f * direction * Time.deltaTime;
        //if (Time.time > decentTime + decentDelay) {
        //    position.y += -1.0f * Time.deltaTime;
        //}
        //if (Time.time > decentTime + decentDelay + decentInterval) {
        //    decentTime = Time.time;
        //}
        float x = direction;
        float y = 0;
        if (Time.time > decentTime + decentDelay) {
            y = -.3f;
        }
        if (Time.time > decentTime + decentDelay + decentInterval) {
            decentTime = Time.time;
        }
        Vector2 offset = new Vector2(x, y);
        offset.Normalize();
        offset *= 3.0f * Time.deltaTime;
        Vector2 position = rigidbody2D.position;
        position.x += offset.x;
        position.y += offset.y;
        rigidbody2D.MovePosition(position);
	}

	private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.GetComponent<BulletController>() != null || collision.collider.GetComponent<PlayerController>() != null) {
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
