using UnityEngine;

public class Burger1 : MonoBehaviour
{
    // Reference to the Renderer to change the color of the burger
    private Renderer burgerRenderer;

    void Start()
    {
        // Get the Renderer component of the GameObject this script is attached to
        burgerRenderer = GetComponent<Renderer>();

        // Invoke the ChangeColor method after 5 seconds
        Invoke("ChangeColor", 20f);
    }

    void ChangeColor()
    {
        // Change the burger's color to black
        if (burgerRenderer != null)
        {
            burgerRenderer.material.color = Color.black;
        }
    }
}
