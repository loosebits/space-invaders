using UnityEngine;
using System.Collections.Generic;

public class EnemyRowController : MonoBehaviour {
    // Use this for initialization
    public int numberOfShips;
    public GameObject shipPrefab;
    public int direction = 1;
    public float speed = 3;

    List<GameObject> ships = new List<GameObject>();

    public void Activate() {
        RectTransform t = (RectTransform) transform;
        Rect rowBounds = t.rect;
        float y = t.position.y;
        float distance = rowBounds.width / numberOfShips;
        for (float x = distance / 2; x <= rowBounds.width - (distance / 2); x += distance) {
            GameObject ship = Instantiate(shipPrefab, new Vector3(t.position.x + t.rect.xMin + x, y, 0), Quaternion.identity);
            EnemyMovementController controller = ship.GetComponent<EnemyMovementController>();
            controller.direction = direction;
            controller.speed = speed;
            EnemyController ctl = ship.GetComponent<EnemyController>();
            ships.Add(ship);
		}
        
    }

    // Update is called once per frame
    void Update() {

    }
}
