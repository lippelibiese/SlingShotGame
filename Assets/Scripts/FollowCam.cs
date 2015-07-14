using UnityEngine;
using System.Collections;

public class FollowCam : MonoBehaviour
{

    public static FollowCam S;  // Singleton follow Cam instance

    public GameObject poi;
    public float velThresh;

    private float FcamZ;

    public Vector2 minXY;

	Vector3 startpos;

    void Awake()
    {
        velThresh = 0.2f;
        S = this;
        FcamZ = transform.position.z;
		startpos = transform.position;
    }

    void FixedUpdate()
    {
		Vector3 destination;
        // check if poi exists
        if (poi == null) {

			//set the poi to the slingshot
			destination = startpos;
		} 

		else {
			destination = poi.transform.position;
		}

        

		// is poi a projectile?
			if(poi !=null && poi.tag == "bullet")
			// check if it is resting
			if(poi.GetComponent<Rigidbody>().velocity.magnitude < velThresh)
				//set poi to null
				poi = null;

		destination.z = FcamZ;
		
		float tt = 0.02f;
		
		if(tt<1) tt += 0.02f;

        destination.x = Mathf.Max(minXY.x, destination.x);
        destination.y = Mathf.Max(minXY.y, destination.y);

      Vector3 newDestination = Vector3.Lerp (transform.position, destination, tt);

        transform.position = newDestination;

        this.GetComponent<Camera> ().orthographicSize = 10 + destination.y;


    }
}