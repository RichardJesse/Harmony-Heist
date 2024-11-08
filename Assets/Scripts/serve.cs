using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class serve : MonoBehaviour
{

    public static event EventHandler ServeCompleted;
    private Playercharacter playercharactertoserve = null;
    private Playercharacter playercharactertodestroy = null;
    bool isreadytoserve = false;
    bool destroy = false;


    private void Update()
    {
        serveuser();
        Destroy();
    }
    private void OnEnable()
    {
        Playercharacter.onReachingServicePoint += Playercharacter_onReachingServicePoint;
        Playercharacter.onReachingExitPoint += Playercharacter_onReachingExitPoint;
    }

    

    private void OnDisable()
    {
        Playercharacter.onReachingServicePoint -= Playercharacter_onReachingServicePoint;
        Playercharacter.onReachingExitPoint += Playercharacter_onReachingExitPoint;
    }


    private void Playercharacter_onReachingServicePoint(Playercharacter playercharacter)
    {

        playercharactertoserve = playercharacter;
        isreadytoserve = true;
            
         
      
    }

    private void Playercharacter_onReachingExitPoint(Playercharacter obj)
    {
        destroy = true;
        playercharactertodestroy = obj;
    }

    private void serveuser()
    {      // basic  serve functionality will work on it  later
        if (Input.GetKeyDown(KeyCode.B) && isreadytoserve)
        {

            Debug.Log("Player" + playercharactertoserve.gameObject.name + "has been served");
            isreadytoserve = false;

            //fire  of an event to dequeue  the  first element from the queue
            ServeCompleted?.Invoke(this,EventArgs.Empty);

        }
        else if (isreadytoserve)
        {
            Debug.Log("Player" + playercharactertoserve.gameObject.name + " waiting to be served");
        }
    }

    private void Destroy()
    {
        if (destroy)
        {
            if (playercharactertodestroy!=null)
            {

                Debug.Log(playercharactertodestroy.gameObject.name+"has been destroyed");  
                destroy = false;
            }
        }
        
    }
}
