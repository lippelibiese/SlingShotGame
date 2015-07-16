using UnityEngine;
using System.Collections;

public class Slingshot : MonoBehaviour {

   
    public GameObject prefabNormal;
    public GameObject prefabSplit;
    public GameObject prefabKamikaze;
    public float velocityFactor;
    public Material trailMat;

   	private  GameObject halo;
    private bool aimingMode;
    private GameObject prefabProjectile;

    private GameObject projectile;
    private Vector3 launchPos;
    private string shotType;


void Awake(){
		Transform haloTrans = transform.Find ("launchpoint");
		halo = haloTrans.gameObject;
		halo.SetActive(false);
        launchPos = haloTrans.position;
        shotType = "Normal";
        prefabProjectile = prefabNormal;
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
    GameController.ShotFired();

    

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

        //add trail renderer and adjust its settings

        TrailRenderer tr = projectile.AddComponent<TrailRenderer>();
        tr.material = trailMat;
        tr.startWidth = 0.1f;
        tr.endWidth = 0.1f;
        tr.time = 50;
    }
}

    // function to switch between shot types

    public void shotSelect(string type){

          shotType = type;
        switch (shotType)
        {
            case "Normal":
                prefabProjectile = prefabNormal;
                break;
            case "Split":
                prefabProjectile = prefabSplit;
                break;
            case "Kamikaze":
                prefabProjectile = prefabKamikaze;
                break;
        }

    }

}
