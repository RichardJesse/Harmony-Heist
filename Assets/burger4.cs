using System.Collections;
using UnityEngine;

public class burger4 : MonoBehaviour
{
    private Renderer burgerRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get the Renderer component of the burger
        burgerRenderer = GetComponent<Renderer>();

        if (burgerRenderer == null)
        {
            Debug.LogError("Renderer component not found on burger4!");
            return;
        }

        // Start the coroutine to turn the burger black after 50 seconds
        StartCoroutine(TurnBlackAfterDelay(50f));
    }

    private IEnumerator TurnBlackAfterDelay(float delay)
    {
        Debug.Log($"burger4 will turn black after {delay} seconds.");

        // Wait for the specified delay (50 seconds)
        yield return new WaitForSeconds(delay);

        // Change the burger's color to black
        if (burgerRenderer != null)
        {
            Debug.Log("burger4 is turning black.");
            burgerRenderer.material.color = Color.black;
        }
    }
}
