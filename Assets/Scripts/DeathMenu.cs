using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    [SerializeField, Tooltip("'MainMenuScene'.")]
    private string mainMenuScene;
    [SerializeField, Tooltip("GameManager gameObject from the scene.")]
    private GameManager gameManager;

    private void Update()
    {
        // restart the game by pressing "enter"
        if (gameObject.activeInHierarchy && Input.GetKeyDown(KeyCode.Return))
        {
            gameManager.ResetGame();
        }
    }

    public void RestartGame()
    {
        gameManager.ResetGame();
    }

    public void QuitToMainMenu()
    {
        SceneManager.LoadScene(mainMenuScene);
    }
}
