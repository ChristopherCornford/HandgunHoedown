using System.Collections;
using System.Collections.Generic;
using UnityEngine;
	
[RequireComponent(typeof(LineRenderer))]

public class LineRend : MonoBehaviour {

	public LineRenderer rend;

	// Use this for initialization
	void Start () {
		rend = GetComponent<LineRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {

		RaycastHit hit;

		if (Physics.Raycast (transform.position, transform.forward, out hit)) {
			if (hit.collider) {
				rend.SetPosition (1, new Vector3 (0, 0, hit.distance));
			}
		} else {
			rend.SetPosition(1, new Vector3( 0, 0 , 100f));
		}
	}
}
