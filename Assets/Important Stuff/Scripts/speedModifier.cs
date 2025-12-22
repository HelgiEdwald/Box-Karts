using UnityEngine;
[CreateAssetMenu(menuName = "Powerup Effect/Speed Modifier")]

//implementation of powerUpModifier that adds a boost in velocity to game object
public class speedModifier : powerUpModifier
{
    //reference to desired boost to modify in inspector
    public float boostForce = 10f;

    //override method of Activate in abstract class that applies boost to a game object rigid body
    public override void Activate(GameObject target)
    {
        Rigidbody rigidbody = target.GetComponent<Rigidbody>();
        
        if (rigidbody != null)
        {
            
            Vector3 boost = target.transform.forward * boostForce;
            rigidbody.AddForce(boost, ForceMode.VelocityChange);

        }
        else
        {
            
            Debug.LogWarning("No Rigidbody found on target for thrust.");
        
        }
    
    }

}