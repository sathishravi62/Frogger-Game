using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

/*
 * This Class is used to control the over all state of the game.
 */
public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Used for Singleton purpose
    public bool isgameOver;// hold the gameover state
    public GameObject player; // This variable to use to hold the player data
    [HideInInspector]
    public Vector2 playerStartPos; // This variable use to store the player start pos

    [Header("Home Properties")]
    public GameObject[] homeImage; // This array used store the image which will display in the UI;
    public GameObject homePrefab; // This variable used to store the prafab of the home frog.
    private int homeCount; // This variable store the cout of home the frog reached 

    [Header("Time Counter Properties")]
    public float countDownTime; // This variable used to store the countdown time.
    private float _currentTime;// this variable store the current time.
    public Image countDownTimeBar; // This variable used to update the curren time in UI

    [Header("Game Start Properties")]
    public GameObject startText; // Start Game Text
    public bool startgame; // Check whether game is started;
    float startCount = 6; // Start countDown
    private void Awake()
    {
        // Creating Singleton pattern
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
        playerStartPos = player.transform.position; // Getting player start position
        _currentTime = countDownTime; // Setting the currentime.
        
    }

    private void Update()
    {
        CountDown(); // This Function is called for time CountDown
       StartCoroutine(GameStart()); // This Function is called for start time CountDown
    }
    public void CheckGameOver() // This Function is used to check whether the game is over or not and reduce lives
    {
        Lives.Instance.ReduceLives(); // calling Reduce Lives function from Lives class.

        if (Lives.Instance.IsDead()) // Checking whether the player is dead
        {
            player.GetComponent<Animator>().Play("Die"); // Playing dead animation
            player.GetComponent<BoxCollider2D>().enabled = false;// Disabling player collider.
            isgameOver = true;// setting gameover variable value to true.
            startText.SetActive(true);
            startText.GetComponentInChildren<TMP_Text>().text = "YOU LOST";
            Invoke("RestartGame", 2f);

        }
        else //If the player is not dead
        {
            player.GetComponent<Animator>().Play("Die"); // Playing dead animation
            player.GetComponent<BoxCollider2D>().enabled = false;// Disabling player collider.
        }
    }

    public void SpawnHome(Transform pos) // This function used spawn the home if player reached using the argument
    {
        Instantiate(homePrefab, pos.position, Quaternion.identity, pos); // Spawning the home 
        homeImage[homeCount].SetActive(false); // Updating the UI
        homeCount++; // Increment the value by 1
        ScoreManager.Instance.UpdateScore(50);// update the score
        startText.SetActive(true);
        startText.GetComponentInChildren<TMP_Text>().text = "TIME:" + (int)_currentTime;
        ScoreManager.Instance.UpdateScore((int)_currentTime * 10); // Update the score by remaining time 
        _currentTime = countDownTime; // Reseting the Time
        
    }

    public void CheckWin() // This function is used to check the win condition
    {
        if(homeCount == 5) // Checking whether the player reached all the home
        {
            ScoreManager.Instance.UpdateScore(1000); // Updating the score.
            startText.SetActive(true);
            startText.GetComponentInChildren<TMP_Text>().text = "YOU WIN";
            isgameOver = true;
            Invoke("RestartGame", 2f);
        }
    }

    public void CountDown() // This function is used for CountDown
    {
        if (!Lives.Instance.reduced && startgame) // Checking whether currently lives is not reducing
        {
            _currentTime -= Time.deltaTime; // Decrement the time by detaTime
            countDownTimeBar.fillAmount = _currentTime / countDownTime; // Updating the UI
        }
        if(_currentTime <= 0) //Checking if time reached less than zero
        {
            CheckGameOver(); // Calling Gameover Function
            AudioManager.Instance.PlaySound(AUDIOTYPE.TIME); // Playing sound
            _currentTime = countDownTime; // Resetting the timer.
        }
    }

    IEnumerator GameStart() // This Function used to Start the game by countdown
    {

        if (!startgame) // if game not started
        {
            startCount -= Time.deltaTime; // Start the count down
            int temp = (int)startCount; // Converting to integer

            if (temp <= 0) // if time is less than or equal to zero
            {
                temp = 0; 
                startText.GetComponentInChildren<TMP_Text>().text = "START"; // Display the start game Text
                yield return new WaitForSeconds(0.5f); // wait for 0.5 second
                startgame = true;// start the game
                player.GetComponent<PlayerControl>().enabled = true;// giving the player control back.
                startText.SetActive(false); // deactivting the start text
                StopCoroutine(GameStart()); // stop the coroutine
            }
            startText.GetComponentInChildren<TMP_Text>().text = temp.ToString(); // update the start count down to UI
        }
        
    }

    void RestartGame() // Use to restart the Game
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Restart the current scene
    }
}
