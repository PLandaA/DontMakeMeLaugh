using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

	public static EnemyManager instance;
	public float speed;




	private void Awake () {
		instance = this;
	}

	void Start () {

	}

	// Update is called once per frame
	void Update () {
		increaseSpeed();

	}

	private void increaseSpeed () {
		if (GameManager.instance.score == 20) {
			speed = 3;
		}
		if (GameManager.instance.score == 35) {
			speed = 4;
		}
		if (GameManager.instance.score == 65) {
			speed = 5;
		}
		if (GameManager.instance.score > 100) {
			speed = 7;
		}
	}

	public void destroyObject (GameObject go) {
		Destroy(go);
		GameManager.instance.takeLives();
		

	}

	
}
