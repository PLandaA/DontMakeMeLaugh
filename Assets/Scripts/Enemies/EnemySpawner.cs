using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
	[SerializeField] GameObject [] enemyPrefab;
	[SerializeField] GameObject [] spawnPoints;
	[SerializeField] GameObject throwableObjects;
	[SerializeField] GameObject powerUpObject;

	int repeatRate = 8;



	void Start () {
		StartCoroutine(spawnEnemies(5));
		InvokeRepeating(nameof(spawnThrowableObjects), 10, 20);
		InvokeRepeating(nameof(spawnPowerUpObject), 23, 34);
	}


	void Update () {



	}


	void spawnThrowableObjects () {
		Instantiate(throwableObjects, spawnPoints [Random.Range(0, spawnPoints.Length)].transform);

	}
	void spawnPowerUpObject () {
		Instantiate(powerUpObject, spawnPoints [Random.Range(0, spawnPoints.Length)].transform);

	}

	IEnumerator spawnEnemies (float time) {
		yield return new WaitForSeconds(time);
		while (true) {
			Instantiate(enemyPrefab [Random.Range(0, enemyPrefab.Length)], spawnPoints [Random.Range(0, spawnPoints.Length)].transform);
			switch (GameManager.instance.score) {
				case 5:
					repeatRate = 5;
					break;
				case 10:
					repeatRate = 4;
					break;
				case 15:
					repeatRate = 3;
					break;
				case 30:
					repeatRate = 2;
					break;
				case 60:
					repeatRate = 1;
					break;
			}
			yield return new WaitForSeconds(repeatRate);

		}

	}


}
