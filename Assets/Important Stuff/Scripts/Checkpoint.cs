using UnityEngine;
public class Checkpoint : MonoBehaviour
{
    //index to assign new checkpoint to player
    public int index;

    //increase index if player collides with box collider component of checkpoint instance
    private void OnTriggerEnter(Collider collision)
    {

        if(collision.gameObject.GetComponent<CarControl>())
        
        {        
            CarControl player = collision.gameObject.GetComponent<CarControl>();

            //update checkpoint index of player if 1 less than index of collided checkpoint
            if (player.checkpointIndex == index - 1)
        
            {
                player.checkpointIndex = index;
        
            }
        
        }

    }

}