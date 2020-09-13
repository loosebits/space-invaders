using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    // Start is called before the first frame update
    public static GameController instance;
    public int maxBulletCount;
	int currentMax;
	int numBullets;
	float lastIncremented;

	public GameController() {
        instance = this;
		currentMax = 1;
		
	}

	void Start() {
		lastIncremented = Time.time;
	}

    // Update is called once per frame
    void Update() {
		Debug.Log("Current max = " + currentMax);
		if (currentMax < maxBulletCount) {
			if (Time.time > lastIncremented + 2) {
				currentMax++;
				lastIncremented = Time.time;
				Debug.Log("Current max = " + currentMax);
			}
		}
    }

	internal void BulletDestroyed() {
		numBullets = Mathf.Clamp(numBullets - 1, 0, currentMax);
		
	}

	internal bool CanHaveMoreBullets() {
		return numBullets < currentMax;
	}

	internal void BulletFired() {
		numBullets = Mathf.Clamp(numBullets + 1, 0, currentMax);
	}

	internal float FiringDelay() {
		return (float)numBullets / (float)currentMax;
	}


}
