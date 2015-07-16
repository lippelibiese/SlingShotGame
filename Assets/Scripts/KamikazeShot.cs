using UnityEngine;
using System.Collections;

public class KamikazeShot : MonoBehaviour {

    private float currentSpeed;
    private GameObject projectile;
    

    void Start()
    {
        //initialize default values for variables
        projectile = this.gameObject;
        currentSpeed = projectile.GetComponent<Rigidbody>().velocity.magnitude;
        
    }
    
    void Update()
    {
        
        //check for mouse input by player
        if (Input.GetMouseButtonDown(0))
            
        {           
            //get current speed of projectile
            currentSpeed = projectile.GetComponent<Rigidbody>().velocity.magnitude;

            //set the velocity to zero and apply currentSpeed to y direction
            projectile.GetComponent<Rigidbody>().velocity = new Vector3(0, -currentSpeed,0);           
           
        }
       

    }
}
