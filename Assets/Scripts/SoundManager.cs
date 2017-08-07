using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	public AudioClip[] gunshots;
	public AudioClip[] gunshot_misses;
	public AudioClip[] punch_hits;
	public AudioClip[] punch_misses;
	public AudioClip[] countdown_sounds;
	public AudioClip[] music_tracks;

	/* Private */
	// Audio Sources
	private AudioSource gunshot_source;
	private AudioSource punches_source;
	private AudioSource UI_source;
	[HideInInspector]
	public AudioSource music_source;

	void Awake () {
		gunshot_source = GameObject.Find("SoundManager/Gunshots").GetComponent<AudioSource>();
		punches_source = GameObject.Find("SoundManager/Punches").GetComponent<AudioSource>();
		UI_source = GameObject.Find("SoundManager/UI_Sound").GetComponent<AudioSource>();
		music_source = GameObject.Find("SoundManager/Music").GetComponent<AudioSource>();
	}
	
	public void Shoot (){
		gunshot_source.clip = gunshots[Random.Range(0, gunshots.Length)];
		gunshot_source.Play();
	}

	public void Miss(){
		punches_source.clip = gunshot_misses[Random.Range(0, gunshot_misses.Length)];
		punches_source.Play();
	}

	public void Punch (bool Hit){
		punches_source.Stop();
		if (Hit == true){
			punches_source.clip = punch_hits[Random.Range(0, punch_hits.Length)];
		}
		else {
			punches_source.clip = punch_misses[Random.Range(0, punch_misses.Length)];
		}
		punches_source.Play();
	}
	
	public void CountdownSound(int whichSound){
		switch (whichSound){
			case 4:
				UI_source.clip = countdown_sounds[0];
				break;
			case 3:
				UI_source.clip = countdown_sounds[1];
				break;
			case 2:
				UI_source.clip = countdown_sounds[2];
				break;
			case 1:
				UI_source.clip = countdown_sounds[3];
				break;
		}
		UI_source.Play();
	}

	public void SetMusic(int track){
		switch (track){
			// 0 is the main usual track
			case 0:
				music_source.clip = music_tracks[0];
				break;
			// 1 is the special last round variant
			case 1:
				music_source.clip = music_tracks[1];
				break;
		}
		music_source.Play();
	}
}
