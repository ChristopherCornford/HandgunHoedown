using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	[Header("Audio References)")]
	public AudioClip[] gunshots;
	public AudioClip[] gunshot_misses;
	public AudioClip[] punch_hits;
	public AudioClip[] punch_misses;
	public AudioClip[] countdown_sounds;
	public AudioClip[] music_tracks;
	public AudioClip gun_pickup;

	/* Private */
	// Audio Sources
	private AudioSource gunshot_source;
	private AudioSource sfx_source;
	private AudioSource UI_source;
	[HideInInspector]
	public AudioSource music_source;

	void Awake () {
		gunshot_source = GameObject.Find("SoundManager/Gunshots").GetComponent<AudioSource>();
		sfx_source = GameObject.Find("SoundManager/Punches").GetComponent<AudioSource>();
		UI_source = GameObject.Find("SoundManager/UI_Sound").GetComponent<AudioSource>();
		music_source = GameObject.Find("SoundManager/Music").GetComponent<AudioSource>();
	}

	public void PickupGun(){
		sfx_source.clip = gun_pickup;
		sfx_source.Play();
	}
	
	public void Shoot (){
		gunshot_source.clip = gunshots[Random.Range(0, gunshots.Length)];
		gunshot_source.Play();
	}

	public void Miss(){
		sfx_source.clip = gunshot_misses[Random.Range(0, gunshot_misses.Length)];
		sfx_source.Play();
	}

	public void Punch (bool Hit){
		sfx_source.Stop();
		if (Hit == true){
			sfx_source.clip = punch_hits[Random.Range(0, punch_hits.Length)];
		}
		else {
			sfx_source.clip = punch_misses[Random.Range(0, punch_misses.Length)];
		}
		sfx_source.Play();
	}
	
	/* UI Sound */
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

	/* Music Manager */
	public void SetMusic(int track){
		music_source.volume = 1f;
		/* 0 is the main usual track
		1 is the special last round variant
		2 is the end of round cue
		3 is the main menu music */
		if (track == 2){
			music_source.loop = false;
		}
		else {music_source.loop = true;}
		music_source.clip = music_tracks[track];
		music_source.Play();
	}
}
