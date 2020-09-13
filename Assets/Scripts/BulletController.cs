using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    public float force = 100;
    public float maxDistance = 5;
    float startPosition;
    new Rigidbody2D rigidbody2D;
    
    

    void Awake() {
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.AddForce(new Vector2(0, 1) * force);
        startPosition = transform.position.y;
    }

    // Update is called once per frame
    void Update() {
        if (Mathf.Abs(transform.position.y - startPosition) > maxDistance) {
            Destroy(gameObject);
		}
    }

	void OnCollisionEnter2D(Collision2D collision) {
        
        if (gameObject.layer == LayerMask.NameToLayer("EnemyProjectile")) {
            GameController.instance.BulletDestroyed();
		}
        Destroy(gameObject);
	}

}
