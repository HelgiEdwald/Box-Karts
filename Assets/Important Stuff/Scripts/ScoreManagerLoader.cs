using UnityEngine;

//class that loads a reference prefab of scoreManager, instantiated in the main menu
public class ScoreManagerLoader : MonoBehaviour
{
    //reference to prefab to instantiate
    public GameObject scoreManagerPrefab;

    //method to ensure a score manager alwyas exists
    void Awake()
    {

        if (ScoreManager.Instance == null)

        {
            Instantiate(scoreManagerPrefab);

        }
    
    }

}
