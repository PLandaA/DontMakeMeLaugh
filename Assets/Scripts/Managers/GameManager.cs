using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager instance;
    [SerializeField] TextMeshProUGUI stageLivesText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI powerUpPercentageText;
    [SerializeField] GameObject [] powerUpDuckImages;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject player;
    [SerializeField] GameObject playerDeath;
    [SerializeField] GameObject pausePanel;
    [SerializeField] Transform spawnVFXPos;

    [SerializeField] AudioClip Muerte;
    [SerializeField] AudioClip Daño;

    [SerializeField] GameObject explosionVFX;

    GameObject [] enemies;
    public int stageLives = 3;
    public int score = 0;
    public bool isDead = false;
    public int powerUpPercentage = 0;

    public bool isPause = false;


    private void Awake () {
        Time.timeScale = 1;

        instance = this;
    }

    void Start () {

    }

    // Update is called once per frame
    void Update () {
        stageLivesText.text = "" +  stageLives;
        scoreText.text = "" +  score;
        powerUpPercentageText.text = powerUpPercentage + "%";

        if (stageLives <= 0) {
            SoundManager.instance.setSound(Muerte);
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            if (enemies == null) {
                Debug.Log("No enemies Found");
                return;

            }
            foreach (var enemy in enemies) {
                Destroy(enemy);
            }
            player.SetActive(false);

            playerDeath.SetActive(true);
            StartCoroutine(waitForDeath());


        }
        if (Input.GetKeyDown(KeyCode.P)) {
            if (!isPause) {
                pause();
            } else {

                Unpause();
            }

        }
        OnPlayerDied();


    }

    public void takeLives () {
        stageLives--;
        SoundManager.instance.setSound(Daño);
    }

    public void increaseScore () {
        spawnVFX();
        score++;
        isDead = true;

    }

    private void pause () {
        Time.timeScale = 1;
        isPause= true;


    }

    private void Unpause () {
        Time.timeScale = 0;
        isPause= false;

    }

    public void increasePowerUpPercentage () {
        powerUpPercentage += 20;

    }

    public void continueGame () {
        SceneManager.LoadScene("GameScene");
    }

    public void exitGame () {
        Application.Quit();
    }

    public void spawnVFX () {
        GameObject explosion = Instantiate(explosionVFX, spawnVFXPos);
        StartCoroutine(timeToDisappear(explosion));


    }

    IEnumerator timeToDisappear (GameObject explosion) {
        yield return new WaitForSeconds(.5f);
        Destroy(explosion);
    }

    public void destroyAllEnemies () {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies == null) {
            Debug.Log("No enemies Found");
            return;

        }
        foreach (var enemy in enemies) {
            Destroy(enemy);
        }
        foreach (var image in powerUpDuckImages) {
            image.SetActive(true);
        }
        StartCoroutine(destroyPowerUpImage());
    }

    IEnumerator destroyPowerUpImage () {
        yield return new WaitForSeconds(2);
        foreach (var image in powerUpDuckImages) {
            image.SetActive(false);
        }
    }

    IEnumerator waitForDeath () {
        yield return new WaitForSeconds(1.5f);
        gameOverScreen.SetActive(true);
        Time.timeScale = 0;

    }

    void OnPlayerDied () {
        int savedScore = PlayerPrefs.GetInt("HighScore");
        if (stageLives <= 0) {
            gameOverScreen.SetActive(true);

            Time.timeScale = 0;

            if (score > savedScore) {
                PlayerPrefs.SetInt("HighScore", score);
            }
        }



    }
}
