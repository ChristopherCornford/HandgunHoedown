using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour {

	public AudioClip[] gunshots;
	private AudioSource gunshot_source;

	// Use this for initialization
	void Awake () {
		gunshot_source = this.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Shoot (){
		gunshot_source.clip = gunshots[Random.Range(0, gunshots.Length)];
		gunshot_source.Play();
	}
}
