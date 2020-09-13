using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
    Animator animator;
    new Rigidbody2D rigidbody2D;
    int direction = 1;

    // Use this for initialization
    void Awake() {
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {

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
            Destroy(bulletController.gameObject);
        } else {
            Debug.Log("Collided");

            direction *= -1;
        }
    }


}
