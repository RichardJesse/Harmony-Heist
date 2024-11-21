using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class PlayerDataSO : ScriptableObject
{
    public Transform prefab;
    public string playername;
    public int arrivalTime;
    public int ServingpointArrivalTime;
    public int servedTime;
    public  int turnaroundTime;
    public  int  waitingTime;
    public int burstTime;


    public void Start()
    {
        arrivalTime = 0;
          servedTime = 0;
          turnaroundTime = 0;
          waitingTime = 0;
           burstTime = 0;
          ServingpointArrivalTime = 0;
    }
}

