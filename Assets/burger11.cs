using System.Collections;
using UnityEngine;


public class burger11 : MonoBehaviour
{
    private Renderer burgerRenderer;
    private bool canBeServed = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get the Renderer component to change the burger's color
        burgerRenderer = GetComponent<Renderer>();

        // Start the coroutine to change the color after 30 seconds
        StartCoroutine(ChangeColorAfterDelay(30f));
    }

    // Update is called once per frame
    void Update()
    {
        // You can add any additional logic here if needed
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // Prevent serving if the color has already changed
        if (canBeServed)
        {
            Playercharacter character = collider.GetComponent<Playercharacter>();
            if (character != null)
            {
                Debug.Log("Burger served to " + character.gameObject.name);
                // Optionally, trigger any events or actions when served
                Destroy(gameObject);
            }
        }
    }

    private IEnumerator ChangeColorAfterDelay(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Change the burger's color to black
        if (burgerRenderer != null)
        {
            burgerRenderer.material.color = Color.black;
        }

        // Disable serving the burger after it turns black
        canBeServed = false;
    }
}
