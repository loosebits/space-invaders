using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
	EnemyRowController[] enemyRowControllers;

	public GameController() {
        instance = this;
		currentMax = 1;
		
	}

	void Start() {
		enemyRowControllers = FindObjectsOfType<EnemyRowController>();
		Array.Sort(enemyRowControllers, new EnemyRowComparator());
		lastIncremented = Time.time;
		currentLevel = startingLevel;
		StartLevel(currentLevel);

	}

	class EnemyRowComparator : IComparer<EnemyRowController> {
		public int Compare(EnemyRowController x, EnemyRowController y) {
			if (x.transform.position.y > y.transform.position.y) {
				return 1;
            }
			return -1;
		}
	};

	void Activate(params int[] rows) {
		foreach (int row in rows) {
			enemyRowControllers[row].Activate();
        }
    }


	void StartLevel(int level) {
		
		
		currentMax = 1;
		for (int i = 0; i < enemyRowControllers.Length; i++) {
			if (i % 2 == 1) {
				enemyRowControllers[i].direction = -1;
			}
        }
		switch (level) {
			case 1:
				Activate(3, 5);
				maxBulletCount = 5;
				break;
			case 2:
				Activate(1, 3, 5);
				maxBulletCount = 7;
				break;
			case 3:
				Activate(0, 2, 5);
				maxBulletCount = 8;
				break;
			case 4:
				Activate(1, 2 , 3, 4);
				maxBulletCount = 8;
				break;
			case 5:
				Activate(0, 1, 2, 3, 4, 5);
				maxBulletCount = 8;
				break;
			case 6:
				foreach (EnemyRowController ctl in enemyRowControllers) {
					ctl.numberOfShips = 8;
					ctl.Activate();
				}
				maxBulletCount = 10;
				break;
			default:
				foreach (EnemyRowController ctl in enemyRowControllers) {
					ctl.numberOfShips = 8;
					ctl.speed += 0.5f * (level - 5);
					ctl.Activate();
					
				}
				maxBulletCount = 10 + 2 * (level - 5);
				break;

		}
	}

	public void GameOver() {
		gameOver = true;
		Invoke("MainMenu", 3f);
	}

	public void MainMenu() {
		SceneManager.LoadScene("MainMenu");
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
		ScoreController.instance.AddScore(100);
	}

	internal void NextLevel() {
		StartLevel(++currentLevel);
	}
}
