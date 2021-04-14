using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/*
 * This class is used for store the current and high score of the game 
 */
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance; // Used for Singleton

    public int score; // hold the player current score
    public int highScore;//hold the ovverall high score of the game

    public TMP_Text scoreText,highScoreText; // Used for UI

    private void Awake()
    {
        // Creating singleton class
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0); // Getting the saved score if their or setting it to zero
        highScoreText.text = highScore.ToString(); // Updating the UI
    }
    public void UpdateScore(int value) // This function is used Update the score
    {
        score += value; // updating the score
        scoreText.text = score.ToString(); // Updating the UI

        if(score > highScore) // Checking whether current score is greater than high score
        {
            highScore = score; // setting the current score as highscore
            highScoreText.text = highScore.ToString(); // Updating the UI
            PlayerPrefs.SetInt("HighScore", highScore); // Saving the HighScore
        }


    }

}
