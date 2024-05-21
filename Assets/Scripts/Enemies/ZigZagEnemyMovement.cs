using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZigZagEnemyMovement : MonoBehaviour {
	int bounce;
	[SerializeField] AudioClip steps;
	[SerializeField] AudioClip dead;
    AudioSource audioSource;


    void Start () {
        audioSource = GetComponent<AudioSource>();

        bounce = 1;
	}

	void Update () {
		transform.Translate(new Vector3(-1, bounce, 0) * EnemyManager.instance.speed * Time.deltaTime);
		if (transform.position.x <= -9) {
			EnemyManager.instance.destroyObject(gameObject);

		}
		if (GameManager.instance.isDead) {
            SoundManager.instance.setSound(dead);
			GameManager.instance.isDead = false;


        }


    }
	private void OnTriggerEnter2D (Collider2D collision) {
		if (collision.CompareTag("Bounds")) {
			audioSource.clip = steps;
			audioSource.Play();
			bounce *= -1;
		}
		

	}
}
