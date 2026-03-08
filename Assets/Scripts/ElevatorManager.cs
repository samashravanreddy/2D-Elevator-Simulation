using UnityEngine;

// This script manages multiple elevators in the building
public class ElevatorManager : MonoBehaviour
{
    // Array that stores all elevator objects in the scene
    public ElevatorController[] elevators;

    // The elevator that is currently selected to respond to requests
    public ElevatorController activeElevator;

    // This function is called when a user presses a floor button
    public void RequestElevator(int floor)
    {
        // Assume the first elevator is the nearest initially
        ElevatorController nearest = elevators[0];

        // Calculate distance between first elevator and requested floor
        float minDistance = Mathf.Abs(elevators[0].currentFloor - floor);

        // Loop through all elevators to find the nearest one
        foreach (var elevator in elevators)
        {
            // Distance between elevator current floor and requested floor
            float distance = Mathf.Abs(elevator.currentFloor - floor);

            // If this elevator is closer than the current nearest
            if (distance < minDistance)
            {
                // Update nearest elevator
                minDistance = distance;
                nearest = elevator;
            }
        }

        // Store the nearest elevator as the active elevator
        activeElevator = nearest;

        // Send the floor request to that elevator
        nearest.AddRequest(floor);

        // Print which elevator is responding
        Debug.Log("Active Elevator: " + nearest.name);
    }

    // This function moves the currently selected elevator to a floor
    public void MoveElevatorToFloor(int floor)
    {
        // If no elevator has been selected yet
        if(activeElevator == null)
        {
            Debug.Log("No elevator selected yet");
            return; // Stop execution
        }

        // Add the requested floor to the elevator's queue
        activeElevator.AddRequest(floor);

        // Show which floor the elevator is moving to
        Debug.Log("Moving to floor " + floor);
    }
}