using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This class is use to move the object in given Direction 
 * To check whether the object left the game area
 * if so disable the object
 */

public enum MoveDir
{
    RIGHT,
    LEFT
}
public class MoveObstacle : MonoBehaviour
{
   
    private float speed = 1; // To ajust the object moving speed
    private MoveDir moveDir; // To get in which direction the object need to move
    private Vector2 dir; // direction value

    private void Update()
    {
        this.transform.Translate(dir * speed * Time.deltaTime); // Moving the object based on the direction and speed
        Deactivate(); // Calling deactivate function to deactivate the object if going out side of the game area.
    }

    public void SetDir(MoveDir moveDir,float speed = 1) // this function is used to set the Direction and find the right value for that direction
    {
        if (moveDir == MoveDir.LEFT) // if Left Direction
        {
            this.moveDir = moveDir; // assigning the value
            dir = Vector2.left; // assigning the direction value.
        }
        else // if Right Direction
        {
            this.moveDir = moveDir; // assigning the value
            dir = Vector2.right; // assigning the direction value.
        }

        this.speed = speed; // assigning the speed value.
    }

    void Deactivate()
    {

        // Calculating the size of the object

        float sizeX = 0; // Creating and assining a value to calculate the total object size.
        if (this.transform.childCount != 0) // Object is having child 
        {
            for (int i = 0; i < this.transform.childCount; i++) // if so looping throught all the child
            {
                sizeX += this.transform.GetChild(i).GetComponent<SpriteRenderer>().size.x; // adding the size of every child object 
            }
        }
        else // Object is not having any child
        {
            sizeX = this.GetComponent<SpriteRenderer>().size.x; // adding the size of one object 
        }
       

        if (moveDir == MoveDir.LEFT) // If moving in Left direction
        {
            if (this.transform.position.x < (-9 - sizeX)) // check the player postion whether it went out of the game area.
            {
                this.gameObject.SetActive(false); // if so deactive the object
            }
        }
        else  // If moving in Right direction
        { 
            if (this.transform.position.x > (9 + sizeX))  // check the player postion whether it went out of the game area.
            { 
                this.gameObject.SetActive(false); // if so deactive the object
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Move" || other.gameObject.tag == "Obstacle")
        {
            this.gameObject.SetActive(false);
        }
    }
}
