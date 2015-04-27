using UnityEngine;
using System.Collections;

public class Slingshot : MonoBehaviour {

	private  GameObject halo;

void Awake(){
		Transform haloTrans = transform.Find ("launchpoint");
		halo = haloTrans.gameObject;
			halo.SetActive(false);

}

void OnMouseEnter(){
		//print ("Slingshot:MouseEnter");
		halo.SetActive (true);
	}

void OnMouseExit() {
		//print ("Slingshot:MouseExit");
		halo.SetActive (false);
	}

}