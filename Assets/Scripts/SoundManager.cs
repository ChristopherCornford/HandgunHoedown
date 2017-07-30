using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	public AudioClip[] gunshots;
	private AudioClip[] gunshot_misses;
	public AudioClip[] punch_hits;
	public AudioClip[] punch_misses;

	/* Private */
	// Audio Sources
	private AudioSource gunshot_source;
	private AudioSource punches_source;

	void Awake () {
		gunshot_source = GameObject.Find("SoundManager/Gunshots").GetComponent<AudioSource>();
		punches_source = GameObject.Find("SoundManager/Punches").GetComponent<AudioSource>();
	}
	
	public void Shoot (){
		gunshot_source.clip = gunshots[Random.Range(0, gunshots.Length)];
		gunshot_source.Play();
	}

	public void Punch (bool Hit){
		if (Hit == true){
			punches_source.clip = punch_hits[Random.Range(0, punch_hits.Length)];
		}
		else {
			punches_source.clip = punch_misses[Random.Range(0, punch_misses.Length)];
		}
		punches_source.Play();
	}
}
