using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class serve : MonoBehaviour
{
    [SerializeField] private GameObject Characterspawner;
    public static event EventHandler ServeCompleted;
    Playercharacter character;
    Playercharacter queued;
    Playercharacter characterserved = null;
    Playercharacter playercharactertoserve = null;
    private Playercharacter playercharactertodestroy = null;
    private Queue<GameObject> characterqueue = null;
    
    bool isreadytoserve = false;
    bool destroy = false;
    private PlayerDataSO player;
   
   

    private void Start()
    {
        characterqueue = Characterspawner.GetComponent<spawner>().GetQueue();
    }
    private void Update()
    {
       
        serveuser();
       // Destroy();
       //  Debug.Log( characterqueue.Count);
       
    }
    private void OnEnable()
    {
        Playercharacter.onReachingServicePoint += Playercharacter_onReachingServicePoint;
        Playercharacter.onReachingExitPoint += Playercharacter_onReachingExitPoint;
        Playercharacter.onReachingQueuePoint += Playercharacter_onReachingQueuePoint;
        
        Burger.OnBurgerServed += Burger_OnBurgerServed;
      
    }

    

    private void OnDisable()
    {
        Playercharacter.onReachingServicePoint -= Playercharacter_onReachingServicePoint;
        Playercharacter.onReachingExitPoint -= Playercharacter_onReachingExitPoint;
        Playercharacter.onReachingQueuePoint -= Playercharacter_onReachingQueuePoint;
    }

    private void Playercharacter_onReachingQueuePoint(Playercharacter Queuecharacter)
    {
      Queuecharacter.GetPlayerData().arrivalTime =Queuecharacter.SetQueueEntryTime();
        Debug.Log(Queuecharacter.GetPlayerData().playername + "arrival Time : T =" +
           Queuecharacter.GetPlayerData().arrivalTime +"Seconds");
    }

    private void Burger_OnBurgerServed(Playercharacter servedplayer)
    {
        characterserved = servedplayer;
    }


    private void Playercharacter_onReachingServicePoint(Playercharacter playercharacter)
    {

        playercharactertoserve = playercharacter;
        playercharacter.GetPlayerData().ServingpointArrivalTime = playercharacter.SetServicePointEntryTime();
        Debug.Log(playercharacter.GetPlayerData().playername + " At ServicePointTime:T =" +
            playercharacter.SetServicePointEntryTime()+"Seconds");
        isreadytoserve = true;
       



    }

    private void Playercharacter_onReachingExitPoint(Playercharacter obj)
    {
        destroy = true;
        playercharactertodestroy = obj;
    }

    private void serveuser()
    {      // basic  serve functionality will work on it  later
        if (characterserved==playercharactertoserve && isreadytoserve)
        {
          
           
            
           player =  characterserved.GetPlayerData();
                              
            player.servedTime =  Mathf.FloorToInt(Time.time);

            player.turnaroundTime = player.servedTime - player.arrivalTime;
            player.burstTime = player.servedTime - player.ServingpointArrivalTime;
            player.waitingTime = player.turnaroundTime - player.burstTime;



            Debug.Log(  player.playername + " who " +
                 " arrived at"+player.arrivalTime+" and served when T=" +
                 player.servedTime+"has been served after"+player.turnaroundTime +"seconds and his" +
                    "waiting time  has been "+player.waitingTime+"seconds and his burst time was "
                    +player.burstTime);
            isreadytoserve = false;
           
            //fire  of an event to dequeue  the  first element from the queue
            ServeCompleted?.Invoke(this,EventArgs.Empty);

        }
       
        else if (isreadytoserve)
        {
            Debug.Log(playercharactertoserve.GetPlayerData().playername + " waiting to be served");
        }
    }

  /*  private void Destroy()
    {
        if (destroy)
        {
            if (playercharactertodestroy != null)
            {

                Debug.Log(playercharactertodestroy.GetPlayerData().playername + "has exited the kitchen");
                Destroy(playercharactertodestroy.gameObject);
                destroy = false;
            }
        }

    }*/
   

    
    
}

