using UnityEngine;
using System.Collections;

public class CloudCrafter : MonoBehaviour {


    // inspector field
    public int numClouds = 40;

    public Vector3 cloudPosMin;
    public Vector3 cloudPosMax;

    public float cloudScaleMin = 1.0f;
    public float cloudScaleMax = 5.0f;

    public float cloudSpeedMult = 0.5f;

    public GameObject[] cloudPrefabs;

    // internal field
    private GameObject[] cloudInstances;
   

    void Awake()
    {
        //create an Array large enough to store all cloud instances
        cloudInstances = new GameObject[40];
        //find the cloud anchor in the hierachy

       GameObject anchor = GameObject.Find("Clouds");

        //iterate through array and create a cloud for each slot
       GameObject cloud;
        for(int cloudinst=0; cloudinst<cloudInstances.Length; cloudinst++)
        {
            //randomly pick one of the cloud prefabs

            int prefabNum = Random.Range(0, cloudPrefabs.Length);
            

           //create that instance
            cloud = Instantiate(cloudPrefabs[prefabNum]);

            //position and scale the cloud(randomly)
            Vector3 cPos = Vector3.zero;
            cPos.x = Random.Range(cloudPosMin.x,cloudPosMax.x);
            cPos.y = Random.Range(cloudPosMin.y,cloudPosMax.y);

            float scaleU = Random.value;
            float scaleVal = Mathf.Lerp(cloudScaleMin, cloudScaleMax, scaleU);

            cPos.y = Mathf.Lerp(cloudPosMin.y, cPos.y, scaleU);

            cPos.z = 100 - 90 * scaleU;
           //apply the changes to our instance
            cloud.transform.position = cPos;
            cloud.transform.localScale = Vector3.one * scaleVal;

            //make cloud child of anchor

            cloud.transform.parent = anchor.transform;

          //put the cloud into our instances array
            cloudInstances[cloudinst] = cloud;

        }
    }
}
