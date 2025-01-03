using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercharacter : MonoBehaviour
{
     [SerializeField] private PlayerDataSO PlayerData;
    [SerializeField] private float speed;
    
    
      private float targetPositionX;
      private bool ismoving;
      private float servingplace;
      private bool isexiting;
      private int queueEntryTime = -1;
      private int ServicePointEntryTime = 0;
    //event 
    public static event Action<Playercharacter> onReachingServicePoint;
    public static event Action<Playercharacter> onReachingExitPoint;
    public static event Action<Playercharacter> onReachingQueuePoint;
    
    
     void Update()
    {  if (ismoving)
        {
            automaticplayermove();
        }
    }

    public void  SetTargetPositionX(float positionX,float servingpoint)
    {
        targetPositionX = positionX;
        servingplace = servingpoint;
        ismoving = true;
        isexiting = false;
    }

    public void SetExitPosition(float exitposition)
    {
        targetPositionX = exitposition;
        ismoving = true;
        isexiting = true;
        
    }
    public void automaticplayermove()
    {
        
        Vector2 movement = new Vector2(targetPositionX, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, movement, speed *Time.deltaTime);

        if (Mathf.Approximately(transform.position.x, targetPositionX)) {
            if (isexiting)
            {
                // Debug.Log(PlayerData.playername + "has exited the game scene");
                //fire of an event to destroy the game object
                onReachingExitPoint?.Invoke(this);
            }
             if (IsAtServingPlace())
            {

                  
                //fire of the event to be handled by serving logic
                onReachingServicePoint?.Invoke(this);
              
            }
             
            
               
               
                onReachingQueuePoint?.Invoke(this);
               

            
           

           
            
            ismoving = false;
        }
        else
        {
            Debug.Log(PlayerData.playername + "moving towards target direction");
        }

    }
      public  bool IsAtServingPlace()
    {
        return Mathf.Approximately(targetPositionX, servingplace);
    }

    public PlayerDataSO GetPlayerData()
    {
        return PlayerData;
    }

    public int SetQueueEntryTime()
    {
        if (queueEntryTime< 0)
        {
              queueEntryTime = Mathf.FloorToInt( Time.time);
            
           
        }
          return queueEntryTime;
    }
     

    public int SetServicePointEntryTime()
    {
        ServicePointEntryTime = Mathf.FloorToInt(Time.time);
        return ServicePointEntryTime;
    }

    

}
