using UnityEngine;
using System.Collections;

public class Slingshot : MonoBehaviour {

    public GameObject prefabProjectile;
    public float velocityFactor;

	private  GameObject halo;
    private bool aimingMode;

    private GameObject projectile;
    private Vector3 launchPos;


void Awake(){
		Transform haloTrans = transform.Find ("launchpoint");
		halo = haloTrans.gameObject;
		halo.SetActive(false);
        launchPos = haloTrans.position;
}

void OnMouseEnter(){
		//print ("Slingshot:MouseEnter");
		halo.SetActive (true);
	}

void OnMouseExit() {
		//print ("Slingshot:MouseExit");
		halo.SetActive (false);
	}

void OnMouseDown()
{
    //set the game to aiming mode

    aimingMode = true;
   

    // instantiate projectile at launch point
    
    projectile = Instantiate (prefabProjectile) as GameObject;

    // turn off physics in aiming mode

    projectile.GetComponent<Rigidbody>().isKinematic = true;
    projectile.transform.position = launchPos;

}

void Update()
{
    //check for aimingMode

    if (!aimingMode) return;

    if (aimingMode)
    {
        halo.SetActive(true);
    }
    
    // get mouse position and convert it to 2D
    
    Vector3 mousePos2D = Input.mousePosition;
    mousePos2D.z = -Camera.main.transform.position.z;
    Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);
    
    //calculate the distance between launchpoint and mouse position
    
    Vector3 mouseDelta = mousePos3D - launchPos;


    // constrain max distance to radius of sphere collider
    float maxMagnitude = this.GetComponent<SphereCollider>().radius; 

    //print(mousePos3D);
    //print(launchPos);
    
    mouseDelta = Vector3.ClampMagnitude(mouseDelta, maxMagnitude);

    // set projectile to new position and shoot it
    projectile.transform.position = launchPos + mouseDelta;


    if (Input.GetMouseButtonUp(0))
    {
        aimingMode = false;
        projectile.GetComponent<Rigidbody>().isKinematic = false;

        projectile.GetComponent<Rigidbody>().velocity = -mouseDelta * velocityFactor;

        FollowCam.S.poi = projectile;
    }

}
}