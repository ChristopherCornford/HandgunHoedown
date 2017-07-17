using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLerp : MonoBehaviour {
	// https://docs.unity3d.com/ScriptReference/Vector3.Lerp.html
    public Transform startMarker;
    public Transform endMarker;
    public float speed = 0.15F;
    private float startTime;
    private float journeyLength;
	private float fracJourney;
    void Start() {
        startTime = Time.time;
        journeyLength = Vector3.Distance(startMarker.position, endMarker.position);
    }
    void Update() {
        float distCovered = (Time.time - startTime) * speed;
        fracJourney = distCovered / journeyLength;
        transform.position = Vector3.Lerp(startMarker.position, endMarker.position, fracJourney);
    }
}
