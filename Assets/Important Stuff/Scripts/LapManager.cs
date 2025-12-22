using System.Collections.Generic;
using UnityEngine;
using TMPro;

//to update lap if player has collided with all checkpoints in list and end the game if player has completed all laps
public class LapManager : MonoBehaviour
{
    //references to text to display, checkpoint instances and how many laps the race should be
    [Header("References")]
    public List<Checkpoint> checkpoints;
    public int totalLaps;
    public TextMeshProUGUI currentLapText;

    private CarControl player;
    private TimerManager timerManager;

    //Start is called before the first frame update
    void Start()
    {
        
        //find instance of player and timer manager in scene (only one exists)
        player = (CarControl)FindFirstObjectByType(typeof(CarControl));
        timerManager = (TimerManager)FindFirstObjectByType(typeof(TimerManager));

    }

    //method to update lap upon player collision
    private void OnTriggerEnter (Collider collision)
    {
        
        if (collision.gameObject.GetComponent<CarControl>())
        {
    
            CarControl player = collision.GetComponent<CarControl>();
            if (player.checkpointIndex == checkpoints.Count)
            {
    
                player.checkpointIndex = 0;
                player.lapNumber++;

                if (player.lapNumber > totalLaps)
                {
    
                    player.GameOver();
    
                }
    
            }
    
        }
   
    }

    //display lap text once the stopwatch timer has started
    void Update()
    {
   
        if (timerManager.stopwatchStarted)
        {
   
            currentLapText.text = $"Lap: {player.lapNumber}/ {totalLaps}";
   
        }

    }
    
}