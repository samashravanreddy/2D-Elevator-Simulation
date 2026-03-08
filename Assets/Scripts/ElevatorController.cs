using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ElevatorController : MonoBehaviour
{
    // Speed at which elevator moves between floors
    public float speed = 300f;

    public int currentFloor = 0;

    // The elevator object that moves up and down
    public RectTransform elevatorCar;

    // Array storing the positions of each floor
    public RectTransform[] floors;

    public TMP_Text displayText;

    // Used to check if elevator is currently stopped at a floor
    bool isStopping = false;

    // How long elevator stays at a floor
    float stopDuration = 2f;

    // List that stores requested floors (queue system)
    private List<int> requestQueue = new List<int>();

    // Floor where the elevator is currently moving toward
    int targetFloor;

    void Update()
    {
        // If there are requests and elevator is not stopping,
        // move to the first requested floor
        if (requestQueue.Count > 0 && !isStopping)
        {
            MoveToFloor(requestQueue[0]);
        }

        UpdateDisplay();
    }

    // Function to move elevator to a specific floor
    void MoveToFloor(int floorIndex)
    {
        // Set the target floor
        targetFloor = floorIndex;

        // Get the Y position of the target floor
        float targetY = floors[floorIndex].anchoredPosition.y;

        // Current elevator position
        Vector2 pos = elevatorCar.anchoredPosition;

        // Distance between elevator and target floor
        float distance = Mathf.Abs(pos.y - targetY);

        // Default speed
        float dynamicSpeed = speed;

        // Slow down when elevator is close to the floor
        if (distance < 50f)
            dynamicSpeed = speed * 0.4f;

        // Smoothly move elevator toward target floor
        pos.y = Mathf.MoveTowards(pos.y, targetY, dynamicSpeed * Time.deltaTime);

        // Apply the new position
        elevatorCar.anchoredPosition = pos;

        // If elevator reached the floor, start stop coroutine
        if (Mathf.Abs(pos.y - targetY) < 1f && !isStopping)
        {
            StartCoroutine(StopAtFloor(floorIndex));
        }
    }

    // Coroutine to simulate elevator stopping and doors opening
    IEnumerator StopAtFloor(int floorIndex)
    {
        // Mark elevator as stopping
        isStopping = true;

        currentFloor = floorIndex;

        yield return new WaitForSeconds(stopDuration);

        // Remove the completed request from queue
        requestQueue.RemoveAt(0);

        // Elevator can move again
        isStopping = false;
    }

    // Update elevator display panel
    void UpdateDisplay()
    {
        // Find the nearest floor to elevator position
        int visibleFloor = GetNearestFloor();

        // Default direction symbol
        string direction = "-";

        // Determine movement direction
        if (targetFloor > visibleFloor)
            direction = "↑";  // going up
        else if (targetFloor < visibleFloor)
            direction = "↓";  // going down
        else if (isStopping)
            direction = "-";  // stopped

        // Update UI text
        displayText.text = direction + "\n" + "Floor " + visibleFloor + " ";
    }

    // Function called when a floor button is pressed
    public void AddRequest(int floor)
    {
        // Prevent duplicate floor requests
        if (!requestQueue.Contains(floor))
        {
            requestQueue.Add(floor);
        }
    }

    // Finds the closest floor to the elevator's current position
    int GetNearestFloor()
    {
        float minDistance = Mathf.Abs(elevatorCar.anchoredPosition.y - floors[0].anchoredPosition.y);
        int nearestFloor = 0;

        // Check distance to every floor
        for (int i = 1; i < floors.Length; i++)
        {
            float distance = Mathf.Abs(elevatorCar.anchoredPosition.y - floors[i].anchoredPosition.y);

            // Update nearest floor if closer
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestFloor = i;
            }
        }

        // Return the closest floor
        return nearestFloor;
    }
}