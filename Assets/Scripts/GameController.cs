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
	public float bonusShipDelay = 5;
	public Vector2 bonusShipSpawnPosition = new Vector2(-10, 4.5f);

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
		bonusShipSpawnTime = Time.time + bonusShipDelay * UnityEngine.Random.value + bonusShipDelay;
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
		if (level < 0) {
			return;
        }
		
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

	float bonusShipSpawnTime;
    bool bonusShip = false;
	public GameObject bonusShipPrefab;
	// Update is called once per frame
	void Update() {
		Debug.Log("Current max = " + currentMax);
		if (currentMax < maxBulletCount) {
			if (Time.time > lastIncremented + 2) {
				currentMax++;
				lastIncremented = Time.time;
			}
		}
		if (!bonusShip  && Time.time > bonusShipSpawnTime + bonusShipDelay * UnityEngine.Random.value) {
			if (numShips >= minimumNumberOfShipsForBonusShipSpawn) {
				Instantiate(bonusShipPrefab, new Vector3(bonusShipSpawnPosition.x, bonusShipSpawnPosition.y, 1), Quaternion.identity);
				bonusShip = true;
				Debug.Log("Creating bonus ship");
			}
			bonusShipSpawnTime = Time.time;
		}
    }

	public int minimumNumberOfShipsForBonusShipSpawn = 5;

	internal void ShipCreated(GameObject gameObject) {
		if (gameObject.GetComponent<BonusEnemyMovementController>() == null) {
			numShips++;
		} 
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

	internal void ShipDestroyed(GameObject gameObject, bool scores = false) {

		if (gameObject.GetComponent<BonusEnemyMovementController>() != null) {
			Debug.Log("Bonus ship destroyed");
			bonusShipSpawnTime = Time.time + bonusShipDelay * UnityEngine.Random.value + bonusShipDelay;
			bonusShip = false;
		} else { 
			numShips--;
			if (numShips == 0) {
				Debug.Log("Finished level " + currentLevel);
				Invoke("NextLevel", 3);
			}
		}
		if (scores) {
			ScoreController.instance.AddScore(gameObject.GetComponent<ScoreComponent>().GetScore());
		}
	}

	internal void NextLevel() {
		StartLevel(++currentLevel);

	}
}
