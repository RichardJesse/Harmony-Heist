using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularCarMovement : MonoBehaviour
{
    public Transform pivotPoint;               // The center point around which cars rotate
    public float rotationSpeed = 30f;          // Speed of rotation (degrees per second)
    public float radius = 5f;                  // Radius of the circular path
    public Vector3 stoppingPointOffset;        // Offset from pivotPoint for the shared stopping point
    public float stopDistance = 0.5f;          // Distance at which cars stop at the stopping point
    public float stoppingDuration;             // Unique stop duration for this car

    private static bool allCarsStopped = false;          // Global flag to stop all cars
    private static CircularCarMovement currentStoppingCar = null; // Tracks which car is stopping

    void Start()
    {
        // Start the movement coroutine for each car
        StartCoroutine(MoveAlongCircle());
    }

    private IEnumerator MoveAlongCircle()
    {
        while (true)
        {
            // Only move the car if allCarsStopped is false
            if (!allCarsStopped || currentStoppingCar == this)
            {
                // Rotate around the pivot point in a 2D context (Z-axis rotation)
                transform.RotateAround(pivotPoint.position, Vector3.forward, rotationSpeed * Time.deltaTime);

                // Check if this car is close enough to the shared stopping point
                Vector3 stoppingPoint = GetStoppingPoint();
                float distanceToStop = Vector3.Distance(transform.position, stoppingPoint);

                // If this car is at the stopping point and no other car is stopping
                if (distanceToStop <= stopDistance && currentStoppingCar == null)
                {
                    // Set this car as the one that stops all cars
                    currentStoppingCar = this;
                    allCarsStopped = true;
                    yield return new WaitForSeconds(stoppingDuration); // Wait for this car's stop duration

                    // Allow all cars to resume movement after the stop duration
                    allCarsStopped = false;
                    currentStoppingCar = null;
                }
            }

            yield return null; // Continue to the next frame
        }
    }

    private Vector3 GetStoppingPoint()
    {
        // Calculate the stopping point position relative to the pivot point
        return pivotPoint.position + stoppingPointOffset;
    }
}
