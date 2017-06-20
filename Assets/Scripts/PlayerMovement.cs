using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public GameObject movement;
	public int m_playerNumber = 0;
	public float speed = 12f;
	public float turnSpeed = 180f;


	public string movementAxisName;
	public string turnAxisName;
	private Rigidbody rb;
	private float movementInputValue;
	private float turnInputValue;

	private void Awake () {
		
		rb = GetComponent<Rigidbody> ();

	}

	private void OnEnable() {

		rb.isKinematic = false;

		movementInputValue = 0f;
		turnInputValue = 0f;

	}

	private void OnDisable () {

		rb.isKinematic = true;
	}

	private void Start () {



		movementAxisName = "Vertical" + m_playerNumber;
		turnAxisName = "Horizontal" + m_playerNumber;

		print (movementAxisName);
		print (turnAxisName);
	}

	private void Update () {

		movementInputValue = Input.GetAxisRaw (movementAxisName);
		turnInputValue = Input.GetAxisRaw (turnAxisName);

	}

	private void FixedUpdate () {
	
		Move ();
		Turn ();

	}

	private void Move() {

		Vector3 movement = transform.forward * movementInputValue * speed * Time.deltaTime;

		rb.MovePosition (rb.position + movement);
	}

	private void Turn () {

		float turn = turnInputValue * turnSpeed * Time.deltaTime;

		Quaternion turnRotation = Quaternion.Euler (0f, turn, 0f);

		rb.MoveRotation (rb.rotation * turnRotation);
	}
}

