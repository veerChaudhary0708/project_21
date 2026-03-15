using UnityEngine;
using TMPro; 
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour 
{
    public int gameTime = 60; 
    public TMPro.TextMeshProUGUI finalScoreText; // Use TMPro.TextMeshProUGUI if using TextMeshPro
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI fpsText;
    public GameObject endScreen;
    
    private int score = 0;
    private float timer;
    private bool gameEnded = false;

    void Start() 
    {
        timer = gameTime;
        if (endScreen != null) endScreen.SetActive(false);
        if (scoreText != null) scoreText.text = "Hits: 0";
    }

    void Update() 
    {
        if (fpsText != null)
            fpsText.text = "FPS: " + (int)(1f / Time.unscaledDeltaTime); 
        
        if (gameEnded) return;

        timer -= Time.deltaTime;
        
        if (timerText != null)
            timerText.text = "Time: " + Mathf.CeilToInt(timer).ToString();

        if (timer <= 0){
            finalScoreText.text = "Final Score: " + score.ToString(); // Assuming your variable is named 'score'
            EndGame();
        } 
    }

    public void AddScore() 
    {
        score++;
        if (scoreText != null) scoreText.text = "Hits: " + score;
    }

    void EndGame() 
    {
        gameEnded = true;
        if (endScreen != null) endScreen.SetActive(true); 
        Time.timeScale = 0; 
    }

    public void RestartGame() 
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}