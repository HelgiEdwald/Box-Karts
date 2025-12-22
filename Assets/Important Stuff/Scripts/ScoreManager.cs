using UnityEngine;
using System.Collections.Generic;

//class for a score manager that's instantiated in the main menu by SceneManagerLoader and persists between games to keep track of highest score
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public List<float> highScores = new List<float>();

    //method to create score manager and create a new one if one exists.
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //method to add score to highScores
    public void AddScore(float score)
    {
        highScores.Add(score);
    }

    //get lowest score added to highScores
    public float GetHighestScore()
    {
        return highScores.Count > 0 ? Mathf.Min(highScores.ToArray()) : 0f;
    }
}
