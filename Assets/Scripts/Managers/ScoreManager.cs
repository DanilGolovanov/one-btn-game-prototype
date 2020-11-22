using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    #region Variables

    [Header("Score")]
    [SerializeField, Tooltip("Text gameObject where the current score will be shown.")]
    private Text scoreText;
    [Tooltip("The actual value of the current score.")]
    public int scoreCount;
    [Tooltip("Indicates if score is need to be keep increasing (stops increasing when player dies).")]
    public bool scoreIncreasing = true;

    [Header("High Score")]
    [SerializeField, Tooltip("Text gameObject where the best score will be shown.")]
    private Text bestScoreText;
    [SerializeField, Tooltip("The actual value of the best score.")]
    private int bestScoreCount;

    #endregion

    #region Default Methods
    private void Start()
    {
        // get stored best score if player played the game before
        if (PlayerPrefs.HasKey("BestScore"))
        {
            bestScoreCount = PlayerPrefs.GetInt("BestScore");
        }
    }

    private void Update()
    {
        SetBestScore();
        UpdateScore();
        UpdateBestScore();
    }
    #endregion

    #region Custom Methods
    /// <summary>
    /// Change best score if current score of the player is bigger than the previous best score.
    /// </summary>
    private void SetBestScore()
    {
        if (scoreCount > bestScoreCount)
        {
            bestScoreCount = scoreCount;
            // save best score
            PlayerPrefs.SetInt("BestScore", bestScoreCount);
        }
    }

    /// <summary>
    /// Keep adding points if player is alive.
    /// </summary>
    public void AddScore()
    {
        if (scoreIncreasing)
        {
            scoreCount++;
        }
    }

    /// <summary>
    /// Update UI text element of the best score.
    /// </summary>
    private void UpdateBestScore()
    {
        bestScoreText.text = "Best: " + bestScoreCount;
    }

    /// <summary>
    /// Update UI text element of the current score.
    /// </summary>
    private void UpdateScore()
    {
        scoreText.text = scoreCount.ToString();
    }
    #endregion
}
