using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour {

	
	private AudioSource gunshot_source;

	// Use this for initialization
	void Awake () {
		gunshot_source = this.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


}
