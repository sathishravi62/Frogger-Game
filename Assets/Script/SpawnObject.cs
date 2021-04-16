using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnMode // To Determain whether want to spawn single object or multiple object
{
    SINGLE_OBJECT,
    MULTIPLE_OBJECT,
}


/* This class used to spawn the obstacle in the game with the help of ObjectPoll Class
 */

public class SpawnObject : MonoBehaviour
{

    public MoveDir moveDir; // use to store in which direction the spawned object need to move.
    public SpawnMode spawnMode;// To check whether single or multiple object need to spawn.
    public GameObject[] spawnObjects;// store the Array of object information need to spawn
    [Range(0.1f,30f)]
    public float spawnTimeMin;// use to tell how offen the object need to spawn 
    [Range(0.1f, 30f)]
    public float spawnTimeMax; // use to tell how offen the object need to spawn 
    public float moveSpeed; // use to tell how much speed the object need to move.
    public float startSpawnTime;
   
    private void Start()
    {
        CreatePoll(); // Call this function to create the poll for the object.
        Invoke("Spawn", startSpawnTime);// All the spawn function after some time interval.
    }

    private void CreatePoll() //  this function is used to create the poll for the object using spawnObjects data.
    {
        foreach (GameObject item in spawnObjects)
        {
            Objectpoll.Instance.CreatePool(item, 5);
        }
    }
    public void Spawn() // This Funtion is used get the index based on the sapwnmode and trigger the sapwn function
    {

        if (spawnMode == SpawnMode.MULTIPLE_OBJECT) // checking whether the spawn type is Multiple
        {
            int index = Random.Range(0, spawnObjects.Length); // IF so getting an random value which will be act as an index for an array
            SpawnObjects(index); // calling SapwnObjets function to spawn actual value based on index value passed as an argument
            Invoke("Spawn", Random.Range(spawnTimeMin, spawnTimeMax)); // Again invoking the same spawn function after some time 
        }
        else // if Single spawn type
        {
            SpawnObjects(0); // calling SapwnObjets function to spawn actual value based on index value passed as an argument 
            Invoke("Spawn", Random.Range(spawnTimeMin, spawnTimeMax)); // Again invoking the same spawn function after some time 
        }
    }

    private void SpawnObjects(int index) // This function is used to spawn actual object with the help of index value which is passed as an argument
    {
        GameObject temp = Objectpoll.Instance.GetObjectFromPoll(spawnObjects[index]); // calling this function to get the object from the poll list using the index value and  store it in the tempe variable
        temp.transform.position = this.transform.position;// Changing the spawned object position to the sapwn position
        temp.GetComponent<MoveObstacle>().SetDir(moveDir,moveSpeed); // accessing the MoveObstacle class and calling the SetDir function and passing the arugment to move that object 
        temp.SetActive(true); // Activating the spawned object
    }
}
