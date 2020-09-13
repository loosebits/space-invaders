using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    // Start is called before the first frame update
    public static GameController instance;
    int maxBulletCount;
	public int startingLevel;
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
		currentLevel = startingLevel;
		StartLevel(currentLevel);

	}

	void StartLevel(int level) {
		EnemyRowController[] enemyRowControllers = FindObjectsOfType<EnemyRowController>();
		currentMax = 1;
		switch (level) {
			case 1:
				enemyRowControllers[2].Activate();
				enemyRowControllers[5].Activate();
				maxBulletCount = 5;
				break;
			case 2:
				enemyRowControllers[1].Activate();
				enemyRowControllers[2].Activate();
				enemyRowControllers[3].Activate();
				maxBulletCount = 7;
				break;
			case 3:
				for (int i = 0; i < 5; i++) {
					enemyRowControllers[i].Activate();
				}
				maxBulletCount = 10;
				break;
			case 4:
				foreach (EnemyRowController ctl in enemyRowControllers) {
					ctl.Activate();
				}

				maxBulletCount = 13;
				break;
			case 5:
				foreach (EnemyRowController ctl in enemyRowControllers) {
					ctl.numberOfShips = 8;
					ctl.Activate();
				}
				maxBulletCount = 13;
				currentMax = 1;
				break;

		}
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
			Invoke("NextLevel", 3);
		}
	}

	internal void NextLevel() {
		StartLevel(++currentLevel);
	}
}
