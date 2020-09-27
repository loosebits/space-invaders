using System;
using UnityEngine;

public interface ICollisionDetector {
    void OnCollision(Collision2D collision);
}
