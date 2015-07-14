using UnityEngine;
using System.Collections;

public class ImpactDestruction : MonoBehaviour {


    private float impactForce;
    private float objectHealth;
    
    public float destroyForce;
    public float damage;
    
   

	// Use this for initialization
	void Start () {
        objectHealth = 30;
        damage = 10;
	}

    void OnCollisionEnter(Collision col)
    {
        impactForce = col.relativeVelocity.magnitude;
        print("impactForce " + impactForce);
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
