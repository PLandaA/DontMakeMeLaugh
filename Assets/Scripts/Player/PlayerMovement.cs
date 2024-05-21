using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    BoxCollider2D boxCol2D;
    bool canMove = true;


    [SerializeField] Transform MeleeAttackSpawnPoint;
    [SerializeField] GameObject MeleeAttackPrefab;
    [SerializeField] float MeleeAttackCD = 0.4f;
    [SerializeField] float MeleeAttackNextFire = 0;
    string [] attackSprites = { "Attacking", "AttackingFeet", "AttackingKame" };
    [SerializeField] AudioClip showTime;
    [SerializeField] AudioClip [] attacksSound;

    [SerializeField] AudioClip Stun;
    Animator anim;

    SpriteRenderer spriteRenderer;
    AudioSource audioSource;




    void Start () {
        boxCol2D = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();




    }

    void Update () {
        if (canMove) {
            upAndDownMovement();
            attack();
            dodge();
            ultAttack();
        }
      
    }

    private void upAndDownMovement () {
        Vector2 tempPos = transform.position;
        if (Input.GetKeyDown(KeyCode.W)) {
            if (tempPos.y == 0) {
                tempPos.y = -2;
            }
            tempPos.y += 2.0f;
        }
        if (Input.GetKeyDown(KeyCode.S)) {
            if (tempPos.y == -4.0f) {
                tempPos.y = -2;
            }
            tempPos.y -= 2.0f;

        }
        transform.position = tempPos;

    }

    private void ultAttack () {
        if (Input.GetKeyDown(KeyCode.Q)) {
            if (GameManager.instance.powerUpPercentage >= 90) {
                SoundManager.instance.setSound(showTime);
                GameManager.instance.destroyAllEnemies();
                GameManager.instance.powerUpPercentage = 0;
            }
        }
    }

    private void attack () {
        if (Input.GetKeyDown(KeyCode.D)   && Time.time > MeleeAttackNextFire) {
            MeleeAttackNextFire = Time.time + MeleeAttackCD;
            GameObject MeleeAttack = Instantiate(MeleeAttackPrefab, MeleeAttackSpawnPoint.position, transform.rotation);
            anim.SetBool(attackSprites[Random.Range(0, attackSprites.Length)], true);
            audioSource.clip = attacksSound [Random.Range(0, attacksSound.Length)];
            audioSource.Play();
            StartCoroutine(attackingCooldown());


        }

    }

    private void dodge () {
        if (boxCol2D.enabled) {
            if (Input.GetKeyDown(KeyCode.A)) {
                boxCol2D.enabled = false;
                StartCoroutine(restartCollider());
                anim.SetBool("Dodge", true);
                StartCoroutine(dodgeAnim());

            }

        }

    }

   



    IEnumerator restartCollider () {
        yield return new WaitForSeconds(2);
        boxCol2D.enabled = true;

    }


    private void OnTriggerEnter2D (Collider2D collision) {
        if (collision.CompareTag("Object")) {
            Destroy(collision.gameObject);
            canMove = false;
            StartCoroutine(restartMovement());
            audioSource.clip = Stun;
            audioSource.Play();
            anim.SetBool("Stunned", true);
        }
        if (collision.CompareTag("PowerUp")) {
            Destroy(collision.gameObject);
            if (GameManager.instance.powerUpPercentage <= 90) {
                GameManager.instance.increasePowerUpPercentage();
            }
        }
    }

    IEnumerator restartMovement () {
        yield return new WaitForSeconds(3);
        anim.SetBool("Stunned", false);
        canMove = true;
    }

    IEnumerator attackingCooldown () {
        yield return new WaitForSeconds(.6f);
        foreach (var sprite in attackSprites) {
            anim.SetBool(sprite, false);
        }
    }

    IEnumerator dodgeAnim () {
        yield return new WaitForSeconds(.6f);
        anim.SetBool("Dodge", false);
    } 
    


}
