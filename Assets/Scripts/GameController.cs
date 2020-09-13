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
	int numShips;
	int currentLevel;
	bool gameOver;

	public GameController() {
        instance = this;
		currentMax = 1;
		
	}

	void Start() {
		lastIncremented = Time.time;
		Level1();

	}


	void Level1() {
		EnemyRowController[] enemyRowControllers = FindObjectsOfType<EnemyRowController>();
		enemyRowControllers[2].Activate();
		enemyRowControllers[5].Activate();
		maxBulletCount = 5;
		currentMax = 1;
	}
	void Level2() {
		EnemyRowController[] enemyRowControllers = FindObjectsOfType<EnemyRowController>();
		enemyRowControllers[1].Activate();
		enemyRowControllers[2].Activate();
		enemyRowControllers[3].Activate();
		maxBulletCount = 7;
		currentMax = 1;
	}
	void Level3() {
		EnemyRowController[] enemyRowControllers = FindObjectsOfType<EnemyRowController>();
		enemyRowControllers[1].Activate();
		enemyRowControllers[2].Activate();
		enemyRowControllers[3].Activate();
		enemyRowControllers[4].Activate();
		enemyRowControllers[5].Activate();
		maxBulletCount = 10;
		currentMax = 1;
	}
	void level4() {
		EnemyRowController[] enemyRowControllers = FindObjectsOfType<EnemyRowController>();
		enemyRowControllers[1].Activate();
		enemyRowControllers[2].Activate();
		enemyRowControllers[3].Activate();
		enemyRowControllers[4].Activate();
		enemyRowControllers[5].Activate();
		enemyRowControllers[6].Activate();
		maxBulletCount = 13;
		currentMax = 1;
	}

	public void GameOver() {
		gameOver = true;
	}

	// Update is called once per frame
	void Update() {
		Debug.Log("Current max = " + currentMax);
		if (currentMax < maxBulletCount) {
			if (Time.time > lastIncremented + 2) {
				currentMax++;
				lastIncremented = Time.time;
			}
		}
    }

	internal void ShipCreated() {
		numShips++;
	}

	internal void BulletDestroyed() {
		numBullets = Mathf.Clamp(numBullets - 1, 0, currentMax);
		
	}

	internal bool CanHaveMoreBullets() {
		return numBullets < currentMax && !gameOver;
	}

	internal void BulletFired() {
		numBullets = Mathf.Clamp(numBullets + 1, 0, currentMax);
	}

	internal float FiringDelay() {
		return (float)numBullets / (float)currentMax;
	}

	internal void ShipDestroyed() {
		numShips--;
		if (numShips == 0) {
			Debug.Log("Finished level " + currentLevel);
			Invoke("Level" + ++currentLevel, 3);
		}
	}
}
