using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;

public class GameManager : MonoBehaviour
{

    public List<GameObject> targets;
    public List<GameObject> mysteryBoxes;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI gameoverText;
    public Button restartButton;
    public GameObject titleScreen;
    public GameObject pauseScreen;
    public float spawnRate = 1.0f;
    private int score;
    private float timeMultiplier = 1;
    private int lives;
    private float timeRemaining = 30f;
    private bool isPaused;
    public bool isGameActive;

    public int getScore()
    {
        return score;
    }    

    public void setScore(int newScore)
    {
        score = newScore;
    }    

    public float getTimeMultiplier()
    {
        return timeMultiplier;
    }    

    public void setTimeMultiplier(float newTimeMultiplier)
    {
        timeMultiplier = newTimeMultiplier;
    }    


    // Start is called before the first frame update
    void Start()
    {

    }

    public void StartGame(int difficulty)
    {
        isGameActive = true;
        score = 0;
        spawnRate /= difficulty;
        timeRemaining = 30f;
        StartCoroutine(SpawnTarget());
        StartCoroutine(SpawnMysteryBox(difficulty));
        UpdateScore(0);
        UpdateLives(3);
        titleScreen.gameObject.SetActive(false);

    }

    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }
    IEnumerator SpawnMysteryBox(int difficulty)
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate * 5 * difficulty);
            int index = Random.Range(0, mysteryBoxes.Count);
            Instantiate(mysteryBoxes[index]);
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score :" + score;
    }

    public void UpdateTimer()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime * timeMultiplier;
            int displayTime = Mathf.RoundToInt(timeRemaining);
            timerText.text = "Time left: " + displayTime;
        }
        else
        {
            timeRemaining = 0;
            isGameActive = false;
            timerText.text = "Time left: 0";
            GameOver();
        }
    }    

    public void UpdateLives(int livesToChange)
    {
        lives += livesToChange;
        livesText.text = "Lives: " + Mathf.Max(livesToChange, 0);
        if (lives <= 0)
        {
            GameOver();
        }
    }

    void ChangePaused()
    {
        if (!isPaused)
        {
            isPaused = true;
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            isPaused = false;
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameActive)
        {
            UpdateTimer();
        }

        if (Input.GetKeyDown(KeyCode.P) && isGameActive)
        {
            ChangePaused();
        }
    }


    public void GameOver()
    {
        isGameActive = false;
        restartButton.gameObject.SetActive(true);
        gameoverText.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
