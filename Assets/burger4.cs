using System.Collections;
using UnityEngine;

public class burger4 : MonoBehaviour
{
    private Renderer burgerRenderer;

    void Start()
    {
        burgerRenderer = GetComponent<Renderer>();

        if (burgerRenderer == null)
        {
            Debug.LogError("Renderer component not found on burger4!");
            return;
        }


        burgerRenderer.material = new Material(burgerRenderer.sharedMaterial);


        StartCoroutine(TurnBlackAfterDelay(5f));
    }

    private IEnumerator TurnBlackAfterDelay(float delay)
    {
        Debug.Log($"burger4 will turn black after {delay} seconds.");
        yield return new WaitForSeconds(delay);

        if (burgerRenderer != null)
        {
            Debug.Log("burger4 is turning black.");
            burgerRenderer.material.color = Color.black;
        }
    }
}
