using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularCarMovement : MonoBehaviour
{
    public Transform pivotPoint;
    public float rotationSpeed = 30f;
    public float radius = 5f;
    public Vector3 stoppingPointOffset;
    public float stopDistance = 0.5f;
    public float stoppingDuration;
    public float totalStoppingTime;
    public float straightMoveDistance = 5f;
    public float straightMoveSpeed = 5f;
    public float turnDuration = 0.5f;

    private static bool allCarsStopped = false;
    private static CircularCarMovement currentStoppingCar = null;
    private static List<CircularCarMovement> nearbyCars = new List<CircularCarMovement>();

    private bool isStopped = false;
    private bool stopPointValid = true;
    private bool isMovingStraight = false;

    void Start()
    {
        StartCoroutine(MoveAlongCircle());
    }

    private IEnumerator MoveAlongCircle()
    {
        while (true)
        {
            if (isMovingStraight)
            {
                yield return MoveTurnMove();
                yield break;
            }

            if (!allCarsStopped || currentStoppingCar == this)
            {
                if (!isStopped)
                {
                    transform.RotateAround(pivotPoint.position, Vector3.forward, rotationSpeed * Time.deltaTime);
                }

                Vector3 stoppingPoint = GetStoppingPoint();

                if (IsCloseToStoppingPoint(stoppingPoint) && stopPointValid && currentStoppingCar == null)
                {
                    if (totalStoppingTime > 0)
                    {
                        currentStoppingCar = this;
                        allCarsStopped = true;

                        StopNearbyCars();

                        float waitTime = Mathf.Min(stoppingDuration, totalStoppingTime);
                        yield return new WaitForSeconds(waitTime);

                        totalStoppingTime -= waitTime;

                        ResumeNearbyCars();
                        allCarsStopped = false;
                        currentStoppingCar = null;

                        stopPointValid = false;
                    }
                    else
                    {
                        isMovingStraight = true;
                    }
                }

                if (!IsCloseToStoppingPoint(stoppingPoint))
                {
                    stopPointValid = true;
                }
            }
            yield return null;
        }
    }

    private Vector3 GetStoppingPoint()
    {
        return pivotPoint.position + stoppingPointOffset;
    }

    private bool IsCloseToStoppingPoint(Vector3 stoppingPoint)
    {
        return Vector3.Distance(transform.position, stoppingPoint) <= stopDistance;
    }

    private void StopNearbyCars()
    {
        nearbyCars.Clear();

        CircularCarMovement[] allCars = FindObjectsOfType<CircularCarMovement>();
        foreach (CircularCarMovement car in allCars)
        {
            if (car != this && Vector3.Distance(car.transform.position, this.transform.position) <= radius)
            {
                nearbyCars.Add(car);
                car.isStopped = true;
            }
        }

        isStopped = true;
    }

    private void ResumeNearbyCars()
    {
        foreach (CircularCarMovement car in nearbyCars)
        {
            car.isStopped = false;
        }

        nearbyCars.Clear();
        isStopped = false;
    }

    private IEnumerator MoveTurnMove()
    {
        Quaternion startRotation = transform.rotation;
        Quaternion firstTurnRotation = startRotation * Quaternion.Euler(0, 0, -90); 
        float turnElapsed = 0f;

        while (turnElapsed < turnDuration)
        {
            turnElapsed += Time.deltaTime;
            transform.rotation = Quaternion.Slerp(startRotation, firstTurnRotation, turnElapsed / turnDuration);
            yield return null;
        }

        Vector3 forwardDirection = transform.up;
        float distanceMoved = 0f;
        while (distanceMoved < straightMoveDistance)
        {
            float step = straightMoveSpeed * Time.deltaTime;
            transform.position += forwardDirection * step;
            distanceMoved += step;
            yield return null;
        }

        startRotation = transform.rotation;
        Quaternion secondTurnRotation = startRotation * Quaternion.Euler(0, 0, 90); 
        turnElapsed = 0f;

        while (turnElapsed < turnDuration)
        {
            turnElapsed += Time.deltaTime;
            transform.rotation = Quaternion.Slerp(startRotation, secondTurnRotation, turnElapsed / turnDuration);
            yield return null;
        }

        forwardDirection = transform.up;

        while (true)
        {
            transform.position += forwardDirection * straightMoveSpeed * Time.deltaTime;
            yield return null;
        }
    }
}
