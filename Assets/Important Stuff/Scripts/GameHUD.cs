using UnityEngine;
using TMPro;

public class GameHUD : MonoBehaviour
{

    //assign references in inspector to displayed text and to get player speed
    [Header("References")]
    public TextMeshProUGUI speedText;
    public CarControl player;
    public Timer countdown;

    //method to disable game hud
    public void SetUp()
    {

        if (gameObject == null)
        {
            return;
        }

        gameObject.SetActive(false);
    
    }

    void Update()
    {

        //display speed after countdown has finished
        if (countdown.countDownFinished)
        {

            //ensure negative numbers (speed in the wrong direction) aren't displayed in HUD
            if (player.forwardSpeed < 0)
            {

                speedText.text = "0 km/h";

            }

            else
            {

                float speedInKmh = player.forwardSpeed * 3.6f;
            
                speedText.text = $"{Mathf.RoundToInt(speedInKmh)} km/h";

            }

        }
        
    }

}
