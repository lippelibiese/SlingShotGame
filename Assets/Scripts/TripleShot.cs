using UnityEngine;
using System.Collections;

public class TripleShot : MonoBehaviour {

    public GameObject splitPrefab;
    public Vector3 posOffset;

    private GameObject projectile;
    private GameObject splitProjectile;
    private Vector3 currentSpeedVec;
    private Transform lastTrans; 

    void Awake()
    {
        projectile = this.gameObject;
        currentSpeedVec = this.GetComponent<Rigidbody>().velocity;
        lastTrans = projectile.transform;
    }

	void Update () {

        //save the speed and position of the projectile right before it is destroyed
        lastTrans.position = projectile.transform.position;
        currentSpeedVec = this.GetComponent<Rigidbody>().velocity;

	if(Input.GetMouseButtonDown(0))
    
    {
        //check if prejectile is held by player
        if (!projectile.GetComponent<Rigidbody>().isKinematic)
        {
           //destroy source projectile and instantiate 3 other ones with a slight offset so they dont interfere
            Destroy(projectile);

            splitProjectile = Instantiate(splitPrefab, lastTrans.position + posOffset, lastTrans.rotation) as GameObject;
            splitProjectile.GetComponent<Rigidbody>().velocity = currentSpeedVec;

            splitProjectile = Instantiate(splitPrefab, lastTrans.position, lastTrans.rotation) as GameObject;
            splitProjectile.GetComponent<Rigidbody>().velocity = currentSpeedVec;
            //set the poi to the central projectile to have the camera follow the group of three projectiles
            FollowCam.S.poi = splitProjectile;

            splitProjectile = Instantiate(splitPrefab, lastTrans.position - posOffset, lastTrans.rotation) as GameObject;
            splitProjectile.GetComponent<Rigidbody>().velocity = currentSpeedVec;
        }

    }
	}
}
