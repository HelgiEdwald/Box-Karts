using UnityEngine;
using UnityEngine.SceneManagement;

//class to display game title and two buttons that loads the game and quits the application or play in editor, respectively
public class MainMenuScreen : MonoBehaviour
{
    
    public void PlayButton()
    {

        SceneManager.LoadScene("Game");
    
    }

    public void ExitButton()
    {
    
        Quit();
    
    }

    public void Quit() 
    {
    
    #if UNITY_STANDALONE
        Application.Quit();
    #endif
    
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #endif
    
    }

}
