using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableObject : MonoBehaviour {

	[SerializeField] public Sprite [] throwableSprites;
	[SerializeField] public Sprite powerUpSprite;
	SpriteRenderer spriteRenderer;


	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer>();
		if (tag == "PowerUp") {
			Debug.Log("Name");
			spriteRenderer.sprite = powerUpSprite;

		} else {
			spriteRenderer.sprite = throwableSprites [Random.Range(0, throwableSprites.Length)];
		}
	}

	void Update () {
		//transform.Translate(Vector3.left * 6 * Time.deltaTime);
		Vector2 tempPos = transform.position;
		tempPos.x -= 6 * Time.deltaTime;

		transform.position = tempPos;
		if (transform.position.x <= -9) {
			Destroy(gameObject);
		}
	}


}
