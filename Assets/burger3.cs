using System.Collections;
using UnityEngine;

public class burger3 : MonoBehaviour
{
    private Renderer burgerRenderer;

    void Start()
    {
        burgerRenderer = GetComponent<Renderer>();

        if (burgerRenderer == null)
        {
            Debug.LogError("Renderer component not found on burger3!");
            return;
        }

        Debug.Log("Starting TurnBlackAfterDelay coroutine.");
        StartCoroutine(TurnBlackAfterDelay(40f)); // 40 seconds delay
    }

    private IEnumerator TurnBlackAfterDelay(float delay)
    {
        Debug.Log($"Waiting for {delay} seconds.");
        yield return new WaitForSeconds(delay);

        burgerRenderer.material = new Material(burgerRenderer.sharedMaterial);


        if (burgerRenderer != null)
        {
            Debug.Log("Changing color to black.");
            burgerRenderer.material.color = Color.black;
        }
    }
}
