using UnityEngine;
using System.Collections;

public class FollowCam : MonoBehaviour
{

    public static FollowCam S;  // Singleton follow Cam instance

    public GameObject poi;

    private float FcamZ;

    public Vector2 minXY;

    void Awake()
    {

        S = this;
        FcamZ = transform.position.z;
    }

    void FixedUpdate()
    {

        // check if poi exists
        if (poi == null) return;

        Vector3 destination = poi.transform.position;
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