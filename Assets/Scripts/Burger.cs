using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Burger : MonoBehaviour
{
    Playercharacter character;

    public static event Action<Playercharacter> OnBurgerServed;

    
    private bool isDragging = false;

    void OnMouseDown()
    {
        Debug.Log("burger");
        isDragging = true;
    }

    void OnMouseUp()
    {
        
        isDragging = false;
    }

    void Update()
    {
        if (isDragging)
        {
            
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = transform.position.z;

          
            transform.position = mousePosition;
        }
    }

    
    private void OnTriggerEnter2D(Collider2D collider)
    {
         character = collider.GetComponent<Playercharacter>();
        if (character != null) {
            Debug.Log("Burger served to " + character.gameObject.name);
            OnBurgerServed?.Invoke(character);
            Destroy(gameObject);
            
        }
    }

  

}