using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercharacter : MonoBehaviour
{

    [SerializeField] private float speed;
     private float targetPositionX;
    private bool ismoving;
    private bool servingpoint;
    
     void Update()
    {  if (ismoving)
        {
            automaticplayermove();
        }
    }

    public void  SetTargetPositionX(float positionX,bool servingpoint)
    {
        targetPositionX = positionX;
        ismoving = true;
    }
    public void automaticplayermove()
    {
        
        Vector2 movement = new Vector2(targetPositionX, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, movement, speed *Time.deltaTime);

        if (transform.position.x == targetPositionX) {

            Debug.Log("Player reached their respective target positions");
            if(servingpoint )
            {  
                //invoking the serving method
                Debug.Log("Player has reached its serving position");
            }
            ismoving = false;
        }
        else
        {
            Debug.Log("Player moving towards target direction");
        }

    }
}
