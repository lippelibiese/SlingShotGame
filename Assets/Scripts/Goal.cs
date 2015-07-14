using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {

	//Static field accessible from anywhere

	public static bool goalMet;

	void OnTriggerEnter(Collider other){

		//check if the obj is projectile

		if (other.tag == "bullet"){

			goalMet = true;
			Color MatGoalCol= this.GetComponent<Renderer>().material.color;
			MatGoalCol.a = 1;

			this.GetComponent<Renderer>().material.color = MatGoalCol;
		// if so, set goalmet to true

		// also set the goals alpha to a higher opacity

		// use Renderer component

	}
}
}		  