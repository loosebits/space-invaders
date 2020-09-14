using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float thrust = 10;
    public GameObject bulletPrefab;
    public AudioClip pewPew;
    public AudioClip boom;

    float horizontal;
    float vertical;
    new Rigidbody2D rigidbody2D;
    Animator animator;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start() {
        rigidbody2D = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        if (Input.GetKeyDown(KeyCode.Space) && rigidbody2D.simulated) {
            Fire();
		}
    }

	private void FixedUpdate() {
        Vector2 position = rigidbody2D.position;
        position.x += 10.0f * horizontal * Time.deltaTime;
        rigidbody2D.MovePosition(position);

    }

    void Fire() {
        GameObject bullet = Instantiate(bulletPrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        audioSource.PlayOneShot(pewPew);
	}

	private void OnCollisionEnter2D(Collision2D collision) {
        BulletController bullet = collision.collider.GetComponent<BulletController>();
        if (bullet != null || collision.collider.GetComponent<EnemyController>() != null) {
            rigidbody2D.simulated = false;
            animator.SetTrigger("Hit");
            audioSource.PlayOneShot(boom);
            GameController.instance.GameOver();
        }

	}
}
