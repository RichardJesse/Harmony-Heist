using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransitionTrigger : MonoBehaviour
{
    [SerializeField] private string playerTag = "Player";   // Tag of the player
    [SerializeField] private int targetLevelIndex;          // Target scene index
    [SerializeField] private string targetLevelName;        // Target scene name (optional)

    private void Start()
    {
        Debug.Log("Level Script running");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the colliding object has the correct tag
        if (other.CompareTag("Player"))
        {
            Debug.Log("Collision detected with: " + other.name);
            Debug.Log("Player entered the trigger!");
            // Load the specified scene by name or index
            if (!string.IsNullOrEmpty(targetLevelName))
            {
                SceneManager.LoadScene(targetLevelName);   
            }
            else
            {
                SceneManager.LoadScene(targetLevelIndex);  
            }
        }
    }
}
