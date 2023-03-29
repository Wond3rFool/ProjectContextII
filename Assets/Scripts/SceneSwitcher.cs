using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void SwitchToEnd(int score) 
    {
        if (PlayerManagerHey.score >= 10)
        {
            SceneManager.LoadScene("");
        }
        else if (PlayerManagerHey.score <= 0)
        {
            SceneManager.LoadScene("");
        }
        else 
        {
            SceneManager.LoadScene("");
        }
    
    }
    public void QuitScene()
    {
        Application.Quit();
    }
}