using UnityEngine;
using System.Collections;

public class EnemyMovementController : MonoBehaviour, ICollisionDetector {
    new Rigidbody2D rigidbody2D;
    public int direction = 1;
    public float decentDelay = 5;
    float decentTime;
    public float decentInterval = .01f;
    public float speed = 3;

    // Use this for initialization
    void Awake() {
        rigidbody2D = GetComponent<Rigidbody2D>();
        decentTime = Time.time;
    }

    // Update is called once per frame
    void FixedUpdate() {
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
        offset *= speed * Time.deltaTime;
        Vector2 position = rigidbody2D.position;
        position.x += offset.x;
        position.y += offset.y;
        rigidbody2D.MovePosition(position);
    }

    public void OnCollision(Collision2D collision) {
        direction *= -1;
    }
}
