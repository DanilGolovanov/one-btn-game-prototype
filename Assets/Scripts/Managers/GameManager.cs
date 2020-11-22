using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Variables

    [SerializeField, Tooltip("Level Manager gameObject.")]
    private Transform levelManager;
    // initial position of the pipe generator
    private Vector3 pipeStartPoint;
    // all pipes in the scene
    private ObjectDestroyer[] pipes;

    [SerializeField, Tooltip("Bird gameObject with attached BirdController script.")]
    private BirdController bird;
    // initial position of the bird
    private Vector3 birdStartPoint;

    private ScoreManager scoreManager;

    [Tooltip("Death Menu gameObject with attached DeathMenu script.")]
    public DeathMenu deathMenu;

    #endregion

    #region Default Methods
    private void Start()
    {
        // get initial values
        pipeStartPoint = levelManager.position;
        birdStartPoint = bird.transform.position;
        scoreManager = FindObjectOfType<ScoreManager>();
    }
    #endregion

    #region Custom Methods
    /// <summary>
    /// Stop scoring points, make bird invisible and activate death menu when bird dies.
    /// </summary>
    public void FinishGame()
    {
        // stop scoring points when player dies
        scoreManager.scoreIncreasing = false;
        // when player dies make bird invisible
        bird.gameObject.SetActive(false);
        // activate death screen when player dies
        deathMenu.gameObject.SetActive(true);
    }

    /// <summary>
    /// Reset player stats and position of player and pipe generator to initial values,
    /// destroy previously generated pipes and deactivate death menu.
    /// </summary>
    public void ResetGame()
    {
        // deactivate death screen
        deathMenu.gameObject.SetActive(false);
        // destroy all platforms that were created previously 
        pipes = FindObjectsOfType<ObjectDestroyer>();
        for (int i = 0; i < pipes.Length; i++)
        {
            pipes[i].gameObject.SetActive(false);
        }
        // reset position of player
        bird.transform.position = birdStartPoint;
        // reset position of platform generator
        levelManager.position = pipeStartPoint;
        // make player visible again
        bird.gameObject.SetActive(true);
        // set default rotation of the bird
        bird.gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
        // reset the score
        scoreManager.scoreCount = 0;
        scoreManager.scoreIncreasing = true;
    }
    #endregion
}
