using UnityEngine;

public class HotSlideInput : MonoBehaviour
{
    private PrometeoCarController car;
    private float touchStartX = 0f;
    public float deadzone = 20f; 

    void Start()
{
    // Force the engine to run at 60 FPS on mobile
    QualitySettings.vSyncCount = 0;
    Application.targetFrameRate = 60;

    car = GetComponent<PrometeoCarController>();
    car.useTouchControls = false; 
}

    void Update()
    {
        // Check if there is at least one finger touching the screen
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // Get the first finger that touched

            // When the finger first hits the glass
            if (touch.phase == TouchPhase.Began)
            {
                touchStartX = touch.position.x;
            }
            // While the finger is sliding or holding still
            else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                car.GoForward(); // Auto-accelerate like Hot Slide
                float deltaX = touch.position.x - touchStartX;

                if (deltaX < -deadzone)
                {
                    car.TurnLeft();
                }
                else if (deltaX > deadzone)
                {
                    car.TurnRight();
                }
                else
                {
                    car.ResetSteeringAngle(); 
                }
            }
        }
        else // When the player lifts their finger off the screen
        {
            car.ThrottleOff();
            car.ResetSteeringAngle(); 
        }
    }
}