using UnityEngine;

//abstract class so that this class can be reused as per tutorial for powerups other than speed (unnecessary for this project scope as there is only 1 but might be useful in future development)
public abstract class powerUpModifier : ScriptableObject
{

    public abstract void Activate(GameObject target);

}