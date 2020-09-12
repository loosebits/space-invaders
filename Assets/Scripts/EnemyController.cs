using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
    Animator animator;

    // Use this for initialization
    void Awake() {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {

    }

    private void OnCollisionEnter2D(Collision2D collision) {
        BulletController bulletController = collision.collider.GetComponent<BulletController>();
        if (bulletController != null) {
            animator.SetTrigger("Hit");
            Destroy(bulletController.gameObject);
        }
    }
}
