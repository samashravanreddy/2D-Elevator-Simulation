# Unity Elevator Simulation (Multiple Lifts)

Features:

- 3 elevators
- 4 floors (Ground, 1, 2, 3)
- Nearest elevator selection
- Individual request queue per elevator
- Smooth elevator movement
- Real-time floor display
- Direction indicator

How It Works:

1. User presses the FLOOR button to call an elevator.
2. The ElevatorManager finds the nearest elevator.
3. Elevator moves smoothly to the requested floor.
4. User presses LIFT button to move elevator to target floor.
5. The Elevator stops on Floor for 2 seconds.
5. Elevator display updates floor and direction.

Technologies Used:

- Unity Engine
- C#
- Unity UI (Canvas system)

Project Structure:

Scripts:
- ElevatorManager.cs
- ElevatorController.cs
- FloorButton.cs
- LiftButton.cs

Live WebGL build:
https://samgame06.itch.io/2d-elevator-simulation

