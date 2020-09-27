using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
    Animator animator;
    new Rigidbody2D rigidbody2D;
    public AudioClip boom;
    float firingDelay;
    float fireTime;

    AudioSource audioSource;

    // Use this for initialization
    void Awake() {
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        GameController.instance.ShipCreated(gameObject);
        audioSource = GetComponent<AudioSource>();
    }


	protected void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log("Collided with " + collision.collider.gameObject +  " that has a " + collision.collider.GetComponent<BulletController>());
        if (collision.collider.GetComponent<BulletController>() != null || collision.collider.GetComponent<PlayerController>() != null) {
            GameController.instance.ShipDestroyed(gameObject, collision.collider.GetComponent<BulletController>() != null);
            rigidbody2D.simulated = false;
            animator.SetTrigger("Hit");
            audioSource.PlayOneShot(boom);
            Debug.Log("Ship destroyed");
            Destroy(gameObject);
        } else {
			foreach (ICollisionDetector det in gameObject.GetComponents<ICollisionDetector>()) {
                det.OnCollision(collision);
            }
        }
    }

    


}
