using UnityEngine;

//class to activate power up if the player collides with the object
public class powerUpPickup : MonoBehaviour
{

    //reference to instance of abstract class powerUpModifier
    public powerUpModifier powerUpModifier;

    //check whether you want to destroy the powerup on collision (i.e. item) or not (i.e. boost pad on track)
    public bool destroyOnTrigger;

    //check player collision
    void OnTriggerEnter(Collider other)
    {

        var playerController = other.GetComponent<CarControl>();

        if (playerController != null)
        {

            //activate power up upon collision
            ActivatePowerUp(playerController);

        } 
        
        else 
        {

            Debug.Log("No car component found on " + other.name);
        
        }
        
    }

    //activate power up within player class
    void ActivatePowerUp(CarControl playerController)
    {

        playerController.ApplyPowerUpModifier(powerUpModifier);

        if (destroyOnTrigger)
        {
            
            Destroy(gameObject);
        
        }

    }
    
}
