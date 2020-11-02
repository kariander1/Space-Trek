using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Level : MonoBehaviour
{
    public void LoadStartMenu()
    {
       
        SceneManager.LoadScene(0);
    }
    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
        FindObjectOfType<GameSession>().ResetGame();
    }
    public void LoadGameOver()
    {
        StartCoroutine(GameOverCoroutine());
    
    }
    public void QuitGame()
    {
        Application.Quit();

    }
    IEnumerator GameOverCoroutine()
    {
        yield return new WaitForSeconds(2.482f);
        SceneManager.LoadScene("Game Over");
    }
}
