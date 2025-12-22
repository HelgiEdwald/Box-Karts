using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//class to to display game over screen with the player's lap time, high score and indication whether the new lap time is the best score and if not, display best score
public class GameOverScreen : MonoBehaviour
{

    //references to text to display
    [Header("References")]
    public Text pointsText;
    public TextMeshProUGUI controlsText;
    public TextMeshProUGUI highScoreText;

    private ScoreManager scoreManager;

    private float highScore;

    private string highScoreString;

    //function to enable game over screen
    public void SetUp(float score)
    {
        //access already instantiated scoreManager to see previous scores
        scoreManager = ScoreManager.Instance;

        gameObject.SetActive(true);

        pointsText.text = "Time: "+ score.ToString("F2") + " Seconds!";
        
        scoreManager.AddScore(score);
        
        highScore = scoreManager.GetHighestScore();
        
        highScoreString = highScore.ToString("F2");
        
        if (score == highScore)
        {
            highScoreText.text = "New High Score!";
        }
        
        else
        {
            highScoreText.text = $"High Score: {highScoreString} seconds";
        }
        
        controlsText.text = "";

    }

    //reload game if restart button clicked, method called in inspector of restart button
    public void RestartButton()
    {
        SceneManager.LoadScene("Game");
        
    }

    //load main menu if exit button clicked, method called in inspector of exit button
    public void ExitButton()
    {
        SceneManager.LoadScene("MainMenu");

    }
}
