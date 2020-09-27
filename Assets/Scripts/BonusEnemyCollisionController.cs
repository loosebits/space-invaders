using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusEnemyCollisionController : MonoBehaviour, ICollisionDetector {
    public float enableCollisionDetectionX;
    BoxCollider2D boxCollider2D;

    public void OnCollision(Collision2D collision) {
        GameController.instance.ShipDestroyed(gameObject);
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Awake() {
        boxCollider2D = GetComponent<BoxCollider2D>();
        boxCollider2D.enabled = false;
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (transform.position.x > enableCollisionDetectionX) {
            boxCollider2D.enabled = true;
        }
    }
}
