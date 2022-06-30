using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public readonly int defaultLastLevel = 1;
    private int _levelIndex;
    private static bool loaded = false;
    private static int _currentLevel = 1;
    [SerializeField] private TextMeshProUGUI levelText;

    private void Awake()
    {
        _levelIndex = SceneManager.GetActiveScene().buildIndex;
    }
    void Start()
    {
        levelText.text = "Level:" + _currentLevel.ToString();
        LoadGame();
        SaveGame();
        PlayerPrefs.SetInt("CurrentLevel", _currentLevel);
    }

    void Update()
    {   
        scoreText.text = "Score : " + ATM.score;
    }
    public void NextLevel(string newGameLevel)
    {
        //Save game and score for next level.
        _currentLevel++;

        SceneManager.LoadScene(newGameLevel);
       
        _levelIndex = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("Game Saved");
    }

    public void RestartGame()
    {
        //Restart level.
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadGame()
    {
        //Load game on startup.
        if (!loaded)
        {
            loaded = true;
            _levelIndex = PlayerPrefs.GetInt("SavedScene", defaultLastLevel);
            SceneManager.LoadScene(_levelIndex);
            ATM.score = PlayerPrefs.GetInt("Player Score");
            _currentLevel = PlayerPrefs.GetInt("CurrentLevel");
        }
    }
    public void SaveGame()
    {
        //Save game.
        PlayerPrefs.SetInt("SavedScene", _levelIndex);
        PlayerPrefs.SetInt("Player Score", ATM.score);
        Debug.Log("Score Loaded");
    }

    public void ResetWholeGame()
    {
        ATM.score = 0;
        SceneManager.LoadScene("Level1");
        _currentLevel = 1;
    }
}
