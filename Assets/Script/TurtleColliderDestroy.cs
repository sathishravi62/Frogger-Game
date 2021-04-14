using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This class is used to Deactivate and Activate the collider which attached to the Object 
 */
public class TurtleColliderDestroy : MonoBehaviour
{
    public float destroyTime = 1.4f; // Deactivate Time
    public float activateTime = 0.2f; // Activate Time
    private void Start()
    {
        InvokeRepeating("DisableCollider", destroyTime, destroyTime); // Using Invoke Repeating Function to repeat the DisableCollider function  
    }

    void DisableCollider() // Use to Deactive the collider
    {
        this.GetComponent<BoxCollider2D>().enabled = false; // Deactivting the collider
        Invoke("EnabelCollider",activateTime); // using Invoke function to Call Enable Collider after given time.
    }

    void EnabelCollider() // Use to Activating the collider
    {
        this.GetComponent<BoxCollider2D>().enabled = true;// activating the collider
    }
}
