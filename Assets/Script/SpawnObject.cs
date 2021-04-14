using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnMode // To Determain whether want to spawn single object or multiple object
{
    SINGLE_OBJECT,
    MULTIPLE_OBJECT,
}

[System.Serializable]
public struct SpawnObjectStruct // To store the information about the Spawnobject and its poll list
{
    public string Name; // Name of the object
    public GameObject spawnObject; // Object need to spawned
    [HideInInspector]
    public List<GameObject> pollEvent; // List used for Object polling
}

/* This class used to spawn the obstacle in the game.
 * This class create is own object polling for the obstacle object and use it to spawn obstacle
 */

public class SpawnObject : MonoBehaviour
{

    public MoveDir moveDir; // use to store in which direction the spawned object need to move.
    public SpawnMode spawnMode;// To check whether single or multiple object need to spawn.
    public SpawnObjectStruct[] spawnObjects;// store the Array of object information need to spawn
    public float spawnTime; // use to tell how offen the object need to spawn 
    public float moveSpeed; // use to tell how much speed the object need to move.
    public float startSpawnTime;
    private void Start()
    {
        CreatePoll(); // Call this function to create the poll for the object.
        Invoke("Spawn", startSpawnTime);// All the spawn function after some time interval.
    }

    private void CreatePoll() //  this function is used to create the poll for the object using spawnObjects data.
    {
        for (int i = 0; i < spawnObjects.Length; i++) // looping throught all the objects in the array.
        {
            spawnObjects[i].pollEvent = new List<GameObject>(); // Initializing the Poll List 
            for (int j = 0; j < 5; j++) // creating a loop which will run 5 times.
            {
                CreateObject(i); // Calling this Create function and passing the index for the spawnObjects array to create object 
            }

        }
    }

    private GameObject CreateObject(int index)  // this function is used to create object and add to poll List.
    {
        GameObject temp = Instantiate(spawnObjects[index].spawnObject, this.transform.position, Quaternion.identity, this.transform); // Creating the Object
        temp.SetActive(false); // deactivating the created object so that you can see it in the game
        spawnObjects[index].pollEvent.Add(temp);// adding the created object to the list

        return temp; // Returing the created object.
    }

    public GameObject GetObjectFromPoll(int index) // This Function use to get the object from the poll list with the help of argument
    {
        // Using the argument index value getting the right data.
        foreach (GameObject gm in spawnObjects[index].pollEvent)  // Looping throught the pollevent
        {
            if (!gm.activeInHierarchy) // Checking whether the object is not active
            {
                return gm; // if so returning that object so we can spawn the object.
            }
        }
        // if all the object in list is being used that create a new object Using CreateObject function and use the return object 
        return CreateObject(index); 
    }


    public void Spawn() // This Funtion is used get the index based on the sapwnmode and trigger the sapwn function
    {

        if (spawnMode == SpawnMode.MULTIPLE_OBJECT) // checking whether the spawn type is Multiple
        {
            int index = Random.Range(0, spawnObjects.Length); // IF so getting an random value which will be act as an index for an array
            SpawnObjects(index); // calling SapwnObjets function to spawn actual value based on index value passed as an argument
            Invoke("Spawn", Random.Range(spawnTime - 0.6f, spawnTime)); // Again invoking the same spawn function after some time 
        }
        else // if Single spawn type
        {
            SpawnObjects(0); // calling SapwnObjets function to spawn actual value based on index value passed as an argument 
            Invoke("Spawn", Random.Range(spawnTime - 1f, spawnTime)); // Again invoking the same spawn function after some time 
        }
    }



    private void SpawnObjects(int index) // This function is used to spawn actual object with the help of index value which is passed as an argument
    {
        GameObject temp = GetObjectFromPoll(index); // calling this function to get the object from the poll list using the index value and  store it in the tempe variable
        temp.transform.position = this.transform.position;// Changing the spawned object position to the sapwn position
        temp.GetComponent<MoveObstacle>().SetDir(moveDir,moveSpeed); // accessing the MoveObstacle class and calling the SetDir function and passing the arugment to move that object 
        temp.SetActive(true); // Activating the spawned object
    }
}
