using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float thrust = 10;
    public GameObject bulletPrefab;

    float horizontal;
    float vertical;
    new Rigidbody2D rigidbody2D;
    Animator animator;
    // Start is called before the first frame update
    void Start() {
        rigidbody2D = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
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
        bulletRb.velocity = new Vector2(rigidbody2D.velocity.x, 0);
	}

	private void OnCollisionEnter2D(Collision2D collision) {
        BulletController bullet = collision.collider.GetComponent<BulletController>();
        if (bullet != null) {
            rigidbody2D.simulated = false;
            animator.SetTrigger("Hit");
            GameController.instance.GameOver();
        }

	}
}
