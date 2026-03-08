using UnityEngine;

public class LiftButton : MonoBehaviour
{
    public int targetFloor;
    public ElevatorManager manager;

    public void MoveElevator()
    {
        manager.MoveElevatorToFloor(targetFloor);
    }
}