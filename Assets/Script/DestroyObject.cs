using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This class only used for destroying the small corcodile
 */
public class DestroyObject : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Move" || other.gameObject.tag == "Obstacle")
        {
            this.gameObject.SetActive(false);
        }
    }
}
