using UnityEngine;
using System.Collections;

public class BonusEnemyFireController : EnemyFireController {


    private void Awake() {
        firingDelay = 1f;
        fireTime = Time.time + Random.value * firingDelay;
        
    }

    void Update() {
        if (Time.time > fireTime) {
            Fire();
            fireTime = Time.time + Random.value * firingDelay;
        }

    }


}
