using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] float sceneLoadDelay = 2f;
    ScoreKeeper scoreKeeper;
    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void LoadGame()
    {
        scoreKeeper.ResetScore();
        SceneManager.LoadScene("Game");
    }
    public void LoadGameOver()
    {
        StartCoroutine(WaitOnLoad("GameOver", sceneLoadDelay));
        SceneManager.LoadScene("GameOver");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator WaitOnLoad(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}
