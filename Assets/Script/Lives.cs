

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/*
 * This Class is use to store the data related player Lives
 */
public class Lives : MonoBehaviour
{
    public int totalLives; // This variable tell what is the total lives of the player
    private int currentLives;// This variable store current lives of the player

    public TMP_Text livesText; // This variable use to display the Current lives of the player
    public bool reduced; // This variable use to control the reducing lives more than ones.
    public static Lives Instance; // This variable is used for Singleton pattern.

    private void Awake()
    {
        // Creating an singleton pattern
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
        // Setting value
        currentLives = totalLives; // Assigning currentLive to TotalLives 
        livesText.text =  currentLives.ToString();// Updating the UI;
    }
    public void ReduceLives() // This Function is used to Reduce player lives by 1
    {
        currentLives -= 1; // Reducing player lives
        reduced = true; // Setting Reduced value to true
        livesText.text =  currentLives.ToString(); // Updating the UI;
    }

    public bool IsDead() // This function is used to check whether player is dead or not 
    { 
        return currentLives <= 0; // if player live become zero then it will return true else false
    }
}
