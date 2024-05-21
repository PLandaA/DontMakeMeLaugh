using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour {

	public float time = .2f;

	private void Awake () {
		Destroy(gameObject, time);
	}

	private void OnTriggerEnter2D (Collider2D collision) {
		if (collision.CompareTag("Enemy")) {
			GameManager.instance.increaseScore();
            Destroy(collision.gameObject);
		}
	}

	IEnumerator waitToDestroy (Collider2D collision) {
		yield return new WaitForSeconds(.15f);

	}


}
