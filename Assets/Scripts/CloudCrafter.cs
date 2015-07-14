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
	void Update(){
		// iterate through all cloud instance
			foreach (GameObject cloudinst in cloudInstances){
				// get the position and scale
			float scaleVal= cloudinst.transform.localScale.x;
			Vector3 cPos = cloudinst.transform.position;

			cPos.x -= Time.deltaTime *cloudSpeedMult * scaleVal;
				
				//check if a clouds x pos is too small
				if(cPos.x < cloudPosMin.x){
				 //set it to maximum x

				cPos.x = cloudPosMax.x;
			}
			cloudinst.transform.position = cPos;
		}


}
}

























