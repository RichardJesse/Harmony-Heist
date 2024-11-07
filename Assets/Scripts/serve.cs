using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class serve : MonoBehaviour
{
    private Playercharacter playercharactertoserve = null;
    bool isreadytoserve = false;


    private void Update()
    {
        serveuser();
    }
    private void OnEnable()
    {
        Playercharacter.onReachingServicePoint += Playercharacter_onReachingServicePoint;
    }

    private void OnDisable()
    {
        Playercharacter.onReachingServicePoint -= Playercharacter_onReachingServicePoint;   
    }

    private void Playercharacter_onReachingServicePoint(Playercharacter playercharacter)
    {

        playercharactertoserve = playercharacter;
        isreadytoserve = true;
            
         
      
    }

    private void serveuser()
    {      // basic  serve functionality will work on it  later
        if (Input.GetKeyDown(KeyCode.B) && isreadytoserve)
        {

            Debug.Log("Player" + playercharactertoserve.gameObject.name + "has been served");
            isreadytoserve = false;
        }
        else if (isreadytoserve)
        {
            Debug.Log("Player" + playercharactertoserve.gameObject.name + " waiting to be served");
        }
    }
}
