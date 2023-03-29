using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public static void SwitchToEnd(int score)
    {
        if (PlayerManagerHey.score >= 5)
        {
            SceneManager.LoadScene(4);
        }
        else if (PlayerManagerHey.score <= 0)
        {
            SceneManager.LoadScene(3);
        }
        else 
        {
            SceneManager.LoadScene(5);
        }
    
    }
    public void QuitScene()
    {
        Application.Quit();
    }
}