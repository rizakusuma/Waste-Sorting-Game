using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Extensione.Audio;

public class GameManager : MonoBehaviour
{
    public string levelID;
    public string nextLevelID;

    public bool isGameOver { get; private set; } = false;

    private bool _isGamePaused = false;
    public bool isGamePaused
    {
        get
        {
            return _isGamePaused;
        }
        private set
        {
            _isGamePaused = value;
            if (_isGamePaused) Time.timeScale = 0;
            else Time.timeScale = 1;
        }
    }

    public bool isPlaying { get { return !isGameOver && !isGamePaused; } }

    public int gameScore { get; private set; }
    public int lastHighscore { get; private set; }
    public int remainingLife { get; private set; }
    public float remainingTime { get; private set; }

    [SerializeField] private int lifeCount = 3;
    [SerializeField] private int scoreMultiplier = 10;
    [SerializeField] private float timeLeft = 120;

    public TrashReceiver trashReceiver;
    public Trashbin trashbin;
    public TrashbinController trashbinController;
    public TrashDestroyer trashDestroyer;

    [SerializeField] private GameObject gamePlayUI;
    [SerializeField] private GameObject gamePausedUI;
    [SerializeField] private GameObject gameOverZeroTimeUI;
    [SerializeField] private GameObject gameOverZeroLifeUI;

    [SerializeField] private AudioClip rightTrashSFX;
    [SerializeField] private AudioClip wrongTrashSFX;

    private void Start()
    {
        remainingLife = lifeCount;
        remainingTime = timeLeft;

        trashReceiver.OnTrashDestroy.AddListener(DestroyTrash);
        trashDestroyer.OnTrashDestroy.AddListener(DestroyTrashMiss);

        isGamePaused = false;
    }

    private void OnDestroy()
    {
        isGamePaused = false;
        Time.timeScale = 1;
    }

    private void Update()
    {
        if (isPlaying)
        {
            remainingTime -= Time.deltaTime;

            if (remainingTime < 0)
            {
                GameOverZeroTime();
            }
        }
    }

    private void IncreaseScore()
    {
        if (isPlaying)
        {
            gameScore += scoreMultiplier;
        }
    }

    private void DecreaseLife()
    {
        if (isPlaying)
        {
            remainingLife--;
            if (remainingLife < 0)
            {
                GameOverZeroLife();
            }
        }
    }

    private void GameOverZeroLife()
    {
        if (isPlaying)
        {
            gameOverZeroLifeUI?.SetActive(true);
            GameOver();
        }
    }

    private void GameOverZeroTime()
    {
        if (isPlaying)
        {
            gameOverZeroTimeUI?.SetActive(true);
            GameOver();
        }
    }

    private void GameOver()
    {
        if (isPlaying)
        {
            isGameOver = true;
            SaveHighscore();

            gamePlayUI.SetActive(false);
            gamePausedUI.SetActive(false);

            trashbinController.Moveable = false;
        }
    }

    private void SaveHighscore()
    {
        string levelHighscoreKey = $"{levelID}_score";
        lastHighscore = gameScore;

        if (PlayerPrefs.HasKey(levelHighscoreKey))
        {
            lastHighscore = PlayerPrefs.GetInt(levelHighscoreKey);
        }

        if (gameScore >= lastHighscore)
        {
            lastHighscore = gameScore;
            PlayerPrefs.SetInt(levelHighscoreKey, gameScore);
            PlayerPrefs.Save();
        }
    }

    public void DestroyTrash(Trash trash)
    {
        if (isPlaying)
        {
            if (trash)
            {
                if (trash.trashType == trashbin.trashType)
                {
                    IncreaseScore();
                    AudioManager.Instance?.PlaySFXOnce(rightTrashSFX);
                }
                else
                {
                    DecreaseLife();
                    AudioManager.Instance?.PlaySFXOnce(wrongTrashSFX);
                }

                Destroy(trash.gameObject);
            }
        }
    }

    public void DestroyTrashMiss(Trash trash)
    {
        if (isPlaying)
        {
            if (trash)
            {
                DecreaseLife();
                AudioManager.Instance?.PlaySFXOnce(wrongTrashSFX);
                Destroy(trash.gameObject);
            }
        }
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(nextLevelID);
    }

    public void GotoMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void RetryLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void PauseGame()
    {
        isGamePaused = true;
        gamePlayUI.SetActive(false);
        gamePausedUI.SetActive(true);

        trashbinController.Moveable = false;
    }

    public void UnpauseGame()
    {
        isGamePaused = false;
        gamePlayUI.SetActive(true);
        gamePausedUI.SetActive(false);

        trashbinController.Moveable = true;
    }    
}
