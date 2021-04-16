using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This class create Object poll for every obstacle
 */
public class Objectpoll : MonoBehaviour
{
    public static Objectpoll Instance; // Used for singleton
    public List<GameObject> pollList = new List<GameObject>(); // Use to store created object

    private void Awake()
    {
        // Creating an Singleton pattern
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    public void CreatePool(GameObject pollObject,int count) // This function is used to create a poll from the given object
    {
        if(CheckAlreayInList(pollObject)) // checking wether that object already exit in the list
        {
            return; // if so dont create the object
        }
        for (int i = 0; i < count; i++)
        {
            CreateObject(pollObject);  // creating the object
        }
    }

    private GameObject CreateObject(GameObject objectPoll) // this function used to create an poll object  and add it to the list
    {
        GameObject temp = Instantiate(objectPoll, this.transform.position, Quaternion.identity);
        temp.SetActive(false); // deactivating the created object so that you can see it in the game
        temp.name = objectPoll.name;
        temp.transform.parent = this.transform;
        pollList.Add(temp);// adding the created object to the list
        return temp; // Returing the created object.
    }

    public GameObject GetObjectFromPoll(GameObject pollObject) //  This Funtion used to get the object from the list with the help of argument;
    {
        foreach (GameObject item in pollList) // Looping throught the list
        {
            if(item.name == pollObject.name && !item.activeInHierarchy) // checking is not active and getting that object
            {
                return item; // returing that object
            }
        }

        return CreateObject(pollObject); // if no object left to use create new one.
    }
    private bool CheckAlreayInList(GameObject checkObject) // This function used to check wether object is in the list.
    {
        if (pollList.Count == 0) return false; // If list is empty the return false.

        foreach (GameObject item in pollList) // looping througth list 
        {
            if(item.name == checkObject.name) // check the name is already in the list
            {
                return true; // return true
            }
        }
        return false; // returen false.
    }
}
