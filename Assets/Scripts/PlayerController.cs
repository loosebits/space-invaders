using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float thrust = 10;
    public GameObject bulletPrefab;

    float horizontal;
    float vertical;
    new Rigidbody2D rigidbody2D;
    // Start is called before the first frame update
    void Start() {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        if (Input.GetKeyDown(KeyCode.Space)) {
            Fire();
		}
    }

	private void FixedUpdate() {
        if (horizontal != 0) {
            Vector2 force = new Vector2(horizontal, 0) * thrust;
            Debug.Log(horizontal);
            rigidbody2D.AddForce(force);
        }

	}

    void Fire() {
        GameObject bullet = Instantiate(bulletPrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.velocity = new Vector2(rigidbody2D.velocity.x, 0);
	}
}
