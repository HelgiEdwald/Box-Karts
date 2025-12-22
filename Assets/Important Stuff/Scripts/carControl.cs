using UnityEngine;

//class for player to control car, also functions as a centralised class to handle game rules
public class CarControl : MonoBehaviour
{
    
    public int lapNumber;

    public int checkpointIndex;

    public GameObject stopwatchGameObject;

    private float currentRearStiffness = 0.5f;

    private float vInput;

    private float hInput;

    private bool spaceInput;

    public bool gameRunning = true;

    private Timer stopwatchTimer;

    [Header("Car Properties")]
    public float motorTorque = 2000f;
    public float brakeTorque = 2000f;
    public float maxSpeed = 20f;
    public float steeringRange = 30f;
    public float steeringRangeAtMaxSpeed = 10f;
    public float centreOfGravityOffset = -1f;

    private WheelControl[] wheels;

    private Rigidbody rigidBody;

    [Header("Assigned Classes")]
    public GameOverScreen GameOverScreen;
    public GameHUD GameHUD;
    public Timer countdown;

    public float forwardSpeed;

    //Start is called before the first frame update
    void Start()
    {
        //establish game having started
        gameRunning = true;

        rigidBody = GetComponent<Rigidbody>();
        rigidBody.constraints = RigidbodyConstraints.None;


        stopwatchTimer = stopwatchGameObject.GetComponent<Timer>();

        // Adjust center of mass to improve stability and prevent rolling
        Vector3 centerOfMass = rigidBody.centerOfMass;
        centerOfMass.y += centreOfGravityOffset;
        rigidBody.centerOfMass = centerOfMass;

        // Get all wheel components attached to the car
        wheels = GetComponentsInChildren<WheelControl>();

        lapNumber = 1;

        checkpointIndex = 0;

    }

    void Update()
    {

        //Get Inputs immediately in Update as getting them in FixedUpdate causes slight delay
        vInput = Input.GetAxis("Vertical");
        hInput = Input.GetAxis("Horizontal");
        spaceInput = Input.GetKey(KeyCode.Space);

    }

    // FixedUpdate is called at a fixed time interval 
    void FixedUpdate()
    {

        // Calculate current speed along the car's forward axis
        forwardSpeed = Vector3.Dot(transform.forward, rigidBody.linearVelocity);
        float speedFactor = Mathf.InverseLerp(0, maxSpeed, Mathf.Abs(forwardSpeed));

        // Reduce motor torque and steering at high speeds for better handling
        float currentMotorTorque = Mathf.Lerp(motorTorque, 0, speedFactor);
        float currentSteerRange = Mathf.Lerp(steeringRange, steeringRangeAtMaxSpeed, speedFactor);
        
        // Determine if the player is accelerating or trying to reverse
        bool isAccelerating = Mathf.Abs(forwardSpeed) < 0.5f || Mathf.Sign(vInput) == Mathf.Sign(forwardSpeed);

        // change stiffness if player is drifting
        float targetStiffness = spaceInput ? 0.5f : 2.0f;
        currentRearStiffness = Mathf.Lerp(currentRearStiffness, targetStiffness, Time.fixedDeltaTime * 5f); 

        //index to only affect rearwheels when drifting
        int index = 0;

        //apply logic to each wheel
        foreach (var wheel in wheels)
        {

            // Check if the countdown is finished
            if (countdown.countDownFinished && gameRunning)
            {
                //proceed with driving logic if the countdown is finished

                // Apply steering to wheels that support steering
                if (wheel.steerable)
                {

                    wheel.WheelCollider.steerAngle = hInput * currentSteerRange;

                }

                //establish rearwheels as the latter two wheels in wheels[]
                bool isRearWheel = index == 2 || index == 3;

                // create new frictioncurves to modify for rear wheels
                if (isRearWheel)
                {

                    WheelFrictionCurve friction = wheel.WheelCollider.sidewaysFriction;

                    //modify rear wheel collider properties if player pressses the space bar
                    if (spaceInput)
                    {

                        wheel.WheelCollider.brakeTorque = brakeTorque * 2;
                        wheel.WheelCollider.motorTorque = 0f;

                        friction.stiffness = currentRearStiffness;
                        friction.extremumSlip = 0.4f;
                        friction.asymptoteSlip = 0.6f;
                        wheel.WheelCollider.sidewaysFriction = friction;

                        index++;
                        continue;

                    }

                    //reset rear wheel collider properties if space bar is not being pressed
                    else
                    {

                        friction.stiffness = 3f;
                        friction.extremumSlip = 0.2f;
                        friction.asymptoteSlip = 0.25f;
                        wheel.WheelCollider.sidewaysFriction = friction;

                        //artifically modify angularvelocity to make drifting more rewarding with less skill
                        Vector3 angVel = rigidBody.angularVelocity;
                        angVel.y = Mathf.Lerp(angVel.y, 0f, Time.fixedDeltaTime * 2f);
                        rigidBody.angularVelocity = angVel;
                        
                    }
                }

                // Normal torque/brake logic
                if (isAccelerating)
                {

                    // Apply torque to motorized wheels
                    if (wheel.motorized)
                    {
                        wheel.WheelCollider.motorTorque = vInput * currentMotorTorque;
                        
                    }

                    // Release brakes when accelerating
                    wheel.WheelCollider.brakeTorque = 0f;

                }

                else
                {

                    // Apply brakes when reversing direction
                    wheel.WheelCollider.motorTorque = 0f;
                    wheel.WheelCollider.brakeTorque = Mathf.Abs(vInput) * brakeTorque;

                }

            }
            
            else
            {

                // set motortorque to zero and braketorque to original value if the countdown has not finished
                wheel.WheelCollider.motorTorque = 0f;
                wheel.WheelCollider.brakeTorque = brakeTorque;

            }

            //update index to see which wheel is being affected for drifting
            index++;

        }
        
    }

    //apply the powerup modifer
    public void ApplyPowerUpModifier(powerUpModifier powerUpModifier)
    {

        powerUpModifier.Activate(gameObject);

    }

    //call method to stop game
    public void GameOver()
    {
        rigidBody.constraints = RigidbodyConstraints.FreezeAll;
        //stop lap timer and get last value
        stopwatchTimer.StopTimer();
        float timeValue = stopwatchTimer.currentTime;

        //disable Heads Up Display upon game finished and enable game over screen
        GameHUD.SetUp();
        GameOverScreen.SetUp(timeValue);

        //establish game being finished
        gameRunning = false;

    }

}