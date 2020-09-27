using UnityEngine;
using System.Collections;

public class BonusEnemyMovementController : MonoBehaviour {
    new Rigidbody2D rigidbody2D;
    public int direction = 1;
    public float speed = 5;

    // Use this for initialization
    void Awake() {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate() {
        float x = direction;
        float y = 0;
        Vector2 offset = new Vector2(x, y);
        offset.Normalize();
        offset *= speed * Time.deltaTime;
        Vector2 position = rigidbody2D.position;
        position.x += offset.x;
        position.y += offset.y;
        rigidbody2D.MovePosition(position);
    }

}
