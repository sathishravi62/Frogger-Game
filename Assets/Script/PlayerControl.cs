using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*<summary> 
The purpos of this PlayerControl Class is to control the player movement,
animation and the collision detection between different obstacle.
</summary>*/


public class PlayerControl : MonoBehaviour
{
    [SerializeField]
    private Animator anim; 
    void Start()
    {
        this.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Movement(); 
        CheckPlayerInRiver();
        Die();
    }

    private void Die() // This function used to disable the player control.
    {
        if(GameManager.Instance.isgameOver) // Checking whether the game is over
        {
            this.enabled = false; // if so disabling the player control.
        }
    }


    /* This function is creted to check whether the player is fell down in the water or move with the obstacle in the river part of the level portion*/
    private void CheckPlayerInRiver()
    {
        if (this.transform.position.y >= 0.5) // Checking whether player reached the River level.
        {
            Collider2D[] col = Physics2D.OverlapCircleAll(this.transform.position, 0.5f); // if so creating a circle cast to get all the collider the player may collided

            foreach (var c in col)// looping throught all the collider
            {
                if (c.gameObject.tag == "Move")// check whether player is colliding with moving object like tutle or log
                {
                    break; // if so break the loop and not checking for any other collider
                }
                else if (c.gameObject.tag == "River" && !Lives.Instance.reduced) // check whether player is colliding with river and whether health reduced in completed
                {
                    GameManager.Instance.CheckGameOver(); // if so reduce the life and check for gameover condition.
                    AudioManager.Instance.PlaySound(AUDIOTYPE.PLUNK); // Playing sound
                }
            }
        }
    }

    private void Movement() // This Function used to move the character and play animation and hop sound
    {
        if (!Lives.Instance.reduced)// checking whether health reduced in completed so we can let new player to move
        {
            if (Input.GetKeyDown(KeyCode.UpArrow)) // Check whether user pressed the Up Arrow
            {
                this.transform.Translate(Vector2.up); // if so move in up dir one unit.
                anim.Play("Up");                      // play UP animation  
                AudioManager.Instance.PlaySound(AUDIOTYPE.HOP); // Playing sound
                ScoreManager.Instance.UpdateScore(10); // Increase the score by 10
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow)) // Check whether user pressed the Down Arrow
            {
                this.transform.Translate(Vector2.down); // if so move in down dir one unit.
                anim.Play("Down");                      // play DOWN animation  
                AudioManager.Instance.PlaySound(AUDIOTYPE.HOP); // Playing sound
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow)) // Check whether user pressed the Right Arrow
            {
                this.transform.Translate(Vector2.right); // if so move in right dir one unit.
                anim.Play("Right");                      // play RIGHT animation  
                AudioManager.Instance.PlaySound(AUDIOTYPE.HOP); // Playing sound
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow)) // Check whether user pressed the Left Arrow
            {
                this.transform.Translate(Vector2.left); // if so move in left dir one unit.
                anim.Play("Left");                      // play Left animation  
                AudioManager.Instance.PlaySound(AUDIOTYPE.HOP); // Playing sound
            }
        }

        Vector3 pos = this.transform.position; // Getting the player postion
        this.transform.position = new Vector3(Mathf.Clamp(pos.x, -7f, 7f), Mathf.Clamp(pos.y, -6.5f, 5.5f), pos.z); // clamping the player position so player can't able to leave the playing area.
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Obstacle") // Checking Collision with Obstacle like car, van, truck 
        {
            GameManager.Instance.CheckGameOver(); // if so reduce the life and check for gameover condition.
            AudioManager.Instance.PlaySound(AUDIOTYPE.SQUASH); // Playing sound
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Move") //  Checking Collision with move object like turtle, log
        {
            this.transform.parent = other.transform; // making child of it.
        }

        if (other.gameObject.tag == "Home") //  Checking Collision with Home object
        {
            if(other.transform.childCount != 1) // Checking whether is home is empty
            {
                GameManager.Instance.SpawnHome(other.transform); // if so call spawnhome function and passing the argument
                ResetPlayer();                                   // calling reset function to start again.
                GameManager.Instance.CheckWin(); // calling CheckWin function.
            }
            else
            {
                this.transform.Translate(Vector2.down); // else home is not empty go back to same position
            }
        }

        if (other.gameObject.tag == "Obstacle") // Checking Collision with Obstacle like Bush 
        {
            GameManager.Instance.CheckGameOver(); // if so reduce the life and check for gameover condition.
            AudioManager.Instance.PlaySound(AUDIOTYPE.SQUASH); // Playing sound
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Move")//  Checking whether Collision state is exit with move object like turtle, log
        {
            this.transform.parent = null; // if so we are no longer child of any object.
        }
    }

    public void ResetPlayer() // This function used to reset the player properties
    {
        if(!GameManager.Instance.isgameOver) // Check whether the game is not over.
        {
            anim.Play("Up"); // playing up animation 
            this.transform.position = GameManager.Instance.playerStartPos; // reset the player pos.
            this.GetComponent<BoxCollider2D>().enabled = true; // enabling the collider
            Lives.Instance.reduced = false; // change the reduced state to false.
        }
    }
}
