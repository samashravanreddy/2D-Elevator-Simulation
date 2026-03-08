using UnityEngine;

public class FloorButton : MonoBehaviour
{
    public int floorNumber;

    public ElevatorManager manager;

    public void CallElevator()
    {
        manager.RequestElevator(floorNumber);
    }
}