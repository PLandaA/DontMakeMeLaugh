using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseEnemyMovement : MonoBehaviour {


	[SerializeField] Vector3 waypoint1, wayPoint2;
	[SerializeField] Vector3 waypoint3, wayPoint4;

	bool hasArrived = false;
	bool hasArrivedToNext = false;

    [SerializeField] AudioClip steps;
    [SerializeField] AudioClip dead;


    void Start () {
        SoundManager.instance.setSound(steps);

        checkForParent();
	}

	void Update () {
		horseMovement();
		if (transform.position.x <= -9) {
			EnemyManager.instance.destroyObject(gameObject);
		}
        if (GameManager.instance.isDead) {
            SoundManager.instance.setSound(dead);
            GameManager.instance.isDead = false;



        }

    }

	private void horseMovement () {

		if (transform.position.x <= 4 && !hasArrived) {
			transform.position = Vector3.MoveTowards(transform.position, waypoint1, .1f);
			StartCoroutine(waitToArrival());
		}

		if (transform.position.x <= -1 && !hasArrivedToNext) {
			transform.position = Vector3.MoveTowards(transform.position, wayPoint2, .1f);
			StartCoroutine(waitToNextArrival());
		}


		transform.Translate(Vector3.left * EnemyManager.instance.speed * Time.deltaTime);
	}

	void checkForParent () {
		string parentName = transform.parent.name;
		if (parentName == "ValidPoint3") {
			waypoint1 = waypoint3;
			wayPoint2 = wayPoint4;
		} 
		if (parentName == "ValidPoint2") {
			hasArrived = true;
			hasArrivedToNext = true;

		}

	}

	IEnumerator waitToArrival () {
		yield return new WaitForSeconds(.5f);
		hasArrived = true;
	}

	IEnumerator waitToNextArrival () {
		yield return new WaitForSeconds(.5f);
		hasArrivedToNext = true;
	}

}
