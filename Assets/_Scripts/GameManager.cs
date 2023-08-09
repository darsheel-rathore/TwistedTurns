using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool gameStarted;
    public int score = 0;

    private const string _HIGH_SCORE = "HighScore";
    private float scoreWaitTime = 0.25f;
    private Coroutine calculateScoreRoutine;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI bestScore;
    [SerializeField] private GameObject gamePlayUI;
    [SerializeField] private GameObject menuUI;

    private void Awake()
    {
        Singleton();
    }

    private void Start()
    {
        // Enable Menu Panel
        menuUI.SetActive(true);
        // Disable Game UI Panel
        gamePlayUI.SetActive(false);

        // Check for high Score
        SaveHighScore();

        // Print the best score on main menu
        PrintBestScore();
    }

    private void Update()
    {
        CheckGameStart();
    }

    // Private Methods
    private void CheckGameStart()
    {
        if (!gameStarted)
        {
            if (Input.anyKey)
            {
                instance.gameStarted = true;
                // Enable game play UI
                gamePlayUI.SetActive(true);
                // Disable Menu Panel
                menuUI.SetActive(false);

                calculateScoreRoutine = StartCoroutine(CalculateScoreRoutine());
            }
        }
    }

    private void Singleton()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void ReloadGameScene()
    {
        // Set score to zero      
        instance.score = 0;

        // set game started to false
        instance.gameStarted = false;

        // Change the bgm
        AudioManager.instance.PlayClip();

        // Load the level
        SceneManager.LoadScene("Game");
    }

    private void SaveHighScore()
    {
        if (PlayerPrefs.HasKey(_HIGH_SCORE))
        {
            if (score > PlayerPrefs.GetInt(_HIGH_SCORE))
            {
                PlayerPrefs.SetInt(_HIGH_SCORE, (int)score);
            }
        }
        else
        {
            PlayerPrefs.SetInt(_HIGH_SCORE, (int)score);
        }
    }

    private void PrintBestScore()
    {
        bestScore.text = "BEST SCORE : " + PlayerPrefs.GetInt(_HIGH_SCORE);
    }

    // Public Methods
    public void PlayerFallen()
    {
        StartCoroutine(PlayerFallenRoutine());
    }

    // Coroutines
    IEnumerator PlayerFallenRoutine()
    {
        // Stop the calculation of the score
        StopCoroutine(calculateScoreRoutine);
        // Check for high score
        SaveHighScore();
        // Wait for some time
        yield return new WaitForSeconds(3f);
        // Reload the level
        ReloadGameScene();
    }

    IEnumerator CalculateScoreRoutine()
    {
        // This will increment the score after every second
        while (true)
        {
            score++;
            scoreText.text = score.ToString();

            yield return new WaitForSeconds(scoreWaitTime);
        }
    }
}
