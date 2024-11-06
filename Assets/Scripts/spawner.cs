using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class spawner : MonoBehaviour
{


     [SerializeField] private float[] targetPositionsX;
     [SerializeField] private GameObject[] playerCharacterPrefabs;
     private Queue<GameObject> characterqueue = new Queue<GameObject>();
     private float spawnDelay =2f;
     private HashSet<string> spawnedPrefabs = new HashSet<string>();

    


    void Start()
    {
       StartCoroutine(spawnObjects());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator spawnObjects()
    {
        while (true)
        {        // Gnerate a character  randomly from any position in the array

             GameObject prefab = playerCharacterPrefabs[Random.Range(0, playerCharacterPrefabs.Length)];

                  //assign the prefab name to a variable to check for duplicate spawning
             
            string prefabName = prefab.name;

                   //use a hashset to store collection of unique spawned items
            if (!spawnedPrefabs.Contains(prefabName))
            {
                  //if unique instantiate the object and add to the 
                GameObject character = Instantiate(prefab, transform.position, Quaternion.identity);
                spawnedPrefabs.Add(prefabName);
                if (character != null)
                {
                    Debug.Log("Character spawned successfully!");
                    // add character to the queue for serving
                    characterqueue.Enqueue(character);
                   
                    int queueIndex = characterqueue.Count - 1;

                    //serving point
                   
                    //assign  target position based on  the queue position
                    character.GetComponent<playercharacter>().SetTargetPositionX(targetPositionsX[queueIndex]);
                }
            }
            else
            {
                Debug.Log("Character already spawned");
            }
           
            yield return new WaitForSeconds(spawnDelay);
        }
    }
    
     
   
    
}
