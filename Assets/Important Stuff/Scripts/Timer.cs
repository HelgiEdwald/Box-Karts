using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Collections;

//configurable class that can count up (stopwatch) or down (countdown) infinitely or to a desired limit
public class Timer : MonoBehaviour
{

    //clarify instance to differentiate between stopwatch timer and countdown timer instances
    public static Timer Instance { get; private set; }
    
    //method that creates enums for formatting (whole numbers needed for countdown, tenth decimals for laptime, hundreths for debugging)
    public enum TimerFormats
    {
        Whole,
        TenthDecimal,
        HundrethsDecimal
    }

    //references to text to display, time to count from, whether to stop upon limit being reached and formatting
    [Header("Component")]
    public TextMeshProUGUI timerText;
    

    [Header("Timer Settings")]
    public float currentTime;
    public bool countDown;

    [Header("Limit Settings")]
    public bool hasLimit;
    public float timerLimit;

    [Header("Format Settings")]
    public bool hasFormat;
    public TimerFormats format;
    private Dictionary<TimerFormats, string> timeFormats = new Dictionary<TimerFormats, string>();

    public bool countDownFinished = false;

    //Start is called before the first frame update
    void Start()
    {

        //add formats to enum dictionary
        timeFormats.Add(TimerFormats.Whole, "0");
        timeFormats.Add(TimerFormats.TenthDecimal, "0.0");
        timeFormats.Add(TimerFormats.HundrethsDecimal, "0.00");
    
    }

    //bool to check status of timer
    public bool isRunning = true; 

    // Update is called once per frame, counting logic defined
    void Update()
    {

        if (!isRunning)
        {
            return; 
        }

        currentTime = countDown ? currentTime -= Time.deltaTime : currentTime += Time.deltaTime;

        if (hasLimit && ((countDown && currentTime <= timerLimit) || (!countDown && currentTime >= timerLimit)))
        {
            currentTime = timerLimit;
            SetTimerText();
            enabled = false;   
        }

        SetTimerText();
    
    }

    //display text based on properties
    private void SetTimerText()
    {
        
        if (hasLimit && countDown && currentTime < 1)
        {
            timerText.text = "GO!";
            countDownFinished = true;
            StartCoroutine(HideTimerAfterDelay(3f));
            return;
        }

        timerText.text = hasFormat ? currentTime.ToString(timeFormats[format]) : currentTime.ToString();

    }

    //method to hide the timer after a delay
    private IEnumerator HideTimerAfterDelay(float delay)
    {

        yield return new WaitForSeconds(delay);
        timerText.enabled = false;
    
    }

    //methods to stop and start timer
    public void StopTimer()
    {
    
        isRunning = false;
        
    }

    public void StartTimer()
    {
    
        isRunning = true;
    
    }

    //method to define instance
    void Awake()
    {
    
        Instance = this;
    
    }

}