using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class BasicEnemyMovement : MonoBehaviour {
    [SerializeField] AudioClip fanAudio;
    [SerializeField] AudioClip dead;



    void Start () {
        SoundManager.instance.setSound(fanAudio);


    }

    void Update () {
	    transform.Translate(Vector3.left * EnemyManager.instance.speed * Time.deltaTime);
		if (transform.position.x <= -9) {
			EnemyManager.instance.destroyObject(gameObject);
		}

        if (GameManager.instance.isDead) {
            SoundManager.instance.setSound(dead);
            GameManager.instance.isDead = false;

        }



    }


	
}

