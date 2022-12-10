using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseManager : MonoBehaviour
{
    /*
     * Wgranie danej sceny
     */

    public void LoadSceneFromString(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }

    /*
     * Wyjdz z gry
     */

    public void ExitGame()
    {
        Application.Quit();
    }
}
