using UnityEngine;

//class to manage the two timers
public class TimerManager : MonoBehaviour
{

    //references to game objects with timer components
    [Header("References")]
    public GameObject countdownGameObject;
    public GameObject stopwatchGameObject;

    private Timer countdownTimer;
    private Timer stopwatchTimer;

    //control start of stopwatch so its sequential to countdown
    public bool stopwatchStarted = false;

    void Start()
    {

        // Get the Timer component from each GameObject
        countdownTimer = countdownGameObject.GetComponent<Timer>();
        stopwatchTimer = stopwatchGameObject.GetComponent<Timer>();

        // Ensure the stopwatch is not running initially
        stopwatchTimer.StopTimer();

    }

    void Update()
    {

        // Check if the countdown has finished and if the stopwatch hasn't started yet
        if (!stopwatchStarted && countdownTimer.countDownFinished)
        {
            stopwatchStarted = true;
            stopwatchTimer.StartTimer();
        }

    }

}