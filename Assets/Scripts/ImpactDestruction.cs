using UnityEngine;
using System.Collections;

public class ImpactDestruction : MonoBehaviour {


    private float impactForce;

    public float objectHealth;
    public float destroyForce;
    public float damage;
    
   

	

    void OnCollisionEnter(Collision col)
    {
        impactForce = col.relativeVelocity.magnitude;
        
        if (impactForce > destroyForce)
        {
            objectHealth -= damage;
        }

        if (objectHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
