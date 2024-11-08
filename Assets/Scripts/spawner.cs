using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class spawner : MonoBehaviour
{

      [SerializeField] private float exitpoint;
     [SerializeField] private float[] targetPositionsX;
     [SerializeField] private GameObject[] playerCharacterPrefabs;
     private Queue<GameObject> characterqueue = new Queue<GameObject>();
     private float spawnDelay =2f;
     private HashSet<string> spawnedPrefabs = new HashSet<string>();
     private bool queueDequeue = false;
      GameObject dequeuedgameobject;




    void Start()
    {
       StartCoroutine(spawnObjects());
    }

    // Update is called once per frame
    void Update()
    {
        if (queueDequeue)
        {
            //After dequeuing the object  we remove him from the line
            //and update the other players to  move to their new positions
            dequeuePlayer();
             StartCoroutine(updatePlayerMovement());

            queueDequeue = false;

           
        }
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
                   
                    int queueIndex = QueueIndex();

                     

                    //assign  target position based on  the queue position
                    character.GetComponent<Playercharacter>().SetTargetPositionX(targetPositionsX[queueIndex], targetPositionsX[0]);
                }
            }
            else
            {
                Debug.Log("Character already spawned");
            }
           
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private void OnEnable()
    {
        serve.ServeCompleted += Serve_ServeCompleted;
    }

    private void Serve_ServeCompleted(object sender, System.EventArgs e)
    {
       queueDequeue = true; 
    }

    private void OnDisable()
    {
        serve.ServeCompleted -= Serve_ServeCompleted;
    }

   

    private  void dequeuePlayer()
    {
        if (characterqueue.Count > 0) {
              dequeuedgameobject = characterqueue.Dequeue();
            dequeuedgameobject.GetComponent<Playercharacter>().SetExitPosition(exitpoint);
             Debug.Log("Dequeued Player: " + dequeuedgameobject.name); 

           
            
        }
        else
        {
            Debug.Log("The queue is empty, no player to dequeue.");
        }


    }
    private int QueueIndex()
    {
        return characterqueue.Count - 1;
    }

    private IEnumerator updatePlayerMovement()
    {
        List<GameObject> charactersToMove = new List<GameObject>(characterqueue);
        int newIndex = 0;
        foreach (var character in charactersToMove) {

            if(newIndex == 0)
            {
                yield return new WaitForSeconds(0.9f);
                character.GetComponent<Playercharacter>().SetTargetPositionX(targetPositionsX[newIndex], targetPositionsX[0]);
              
            }

            character.GetComponent<Playercharacter>().SetTargetPositionX(targetPositionsX[newIndex], targetPositionsX[0]);
           
              newIndex++;

            yield return new WaitForSeconds(0.9f);

        }
    }

}






