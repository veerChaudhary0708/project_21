# Top-Down Zombie Drifter - Unity Technical Test

## Overview
This is a top-down, portrait-oriented mobile car game developed in Unity 6.3 LTS. The objective is to drive a vehicle using swipe-based mobile controls to hit wandering zombies within a set time limit, maximizing the score before the countdown ends. 

## Controller Adaptation (Prometeo -> Hot Slide)
The core requirement was to adapt the simulation-heavy Prometeo Car Controller into an arcade-style, responsive swipe/drag input system referencing the game *Hot Slide*. 

**Implementation Details:**
* **Input Interception:** Prometeo's default touch UI and keyboard listeners were completely disabled. A custom `HotSlideInput` adapter script was written to interface directly with Unity's native `Input.GetTouch()` API.
* **Swipe-to-Steer Logic:** The adapter logs the initial `TouchPhase.Began` X-coordinate. As the finger moves (`TouchPhase.Moved`), it calculates the horizontal delta. 
* **Deadzone & Centering:** A deadzone threshold was implemented to prevent jittering from micro-movements. Exceeding the deadzone triggers `TurnLeft()` or `TurnRight()`. Releasing the screen or returning to the deadzone calls the reverse-engineered `ResetSteeringAngle()` method to cleanly snap the wheels back to the center.
* **Auto-Acceleration:** To match the *Hot Slide* arcade feel, the car automatically calls `GoForward()` whenever a touch is active on the screen.

## Key Features & Technical Details
* **Active Ragdolls:** NPC zombies utilize the *Stylized Zombie* asset pack. Upon collision with the player vehicle, the NPCs instantly transition from Animator-driven walk cycles to physics-driven active ragdolls using Rigidbody chains.
* **Mobile Optimization:** To meet the strict $\ge58$ FPS requirement on Android devices, `QualitySettings.vSyncCount` was disabled and `Application.targetFrameRate` was hardcoded to 60. 
* **Render Pipeline:** Legacy standard materials from the 3D asset packs were explicitly converted to the Universal Render Pipeline (URP) to prevent mobile shader compilation errors (the "pink material" bug).
* **Game State Management:** A central Game Controller handles the countdown timer, score increments, and clean scene reloading via `SceneManager` upon triggering the End Screen.

## Known Issues & Limitations
* **Prometeo Physics Constraints:** Because the Prometeo asset is built around semi-realistic wheel colliders and friction curves, achieving a pure "arcade" drifting feel (like sliding on ice) requires extensive tuning of the wheel physics materials, which slightly limits the extreme drift angles seen in *Hot Slide*.
* **Ragdoll Clipping:** High-speed collisions occasionally cause minor clipping of the ragdoll limbs through the floor plane, a common limitation of Unity's default physics solver at high velocities without continuous dynamic collision detection enabled.
