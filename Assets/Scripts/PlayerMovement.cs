using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public Vector3 gunShot;
	public GameObject movement;
	public int m_playerNumber = 0;
	public float speed = 12f;
	public float turnSpeed = 180f;


	public string movementKeyAxisName;
	public string strafeKeyAxisName;
	public string turnKeyAxisName;
	public string aimKeyAxisName;
	public string actionKeyAxisName;
	public string dashKeyAxisName;

	public string movementJoyAxisName;
	public string strafeJoyAxisName;
	public string turnJoyAxisName;
	public string aimJoyAxisName;
	public string actionJoyAxisName;
	public string dashJoyAxisName;

	private Rigidbody rb;
	private LineRenderer line;

	private float movementKeyInputValue;
	private float strafeKeyInputValue;
	private float turnKeyInputValue;

	private float movementJoyInputValue;
	private float strafeJoyInputValue;
	private float turnJoyInputValue;

	private void Awake () {
		
		rb = GetComponent<Rigidbody> ();

	}

	private void OnEnable() {

		rb.isKinematic = false;

		movementKeyInputValue = 0f;
		strafeKeyInputValue = 0f;
		turnKeyInputValue = 0f;

		movementJoyInputValue = 0f;
		strafeJoyInputValue = 0f;
		turnJoyInputValue = 0f;
	}

	private void OnDisable () {

		rb.isKinematic = true;
	
	}

	private void Start () {

		line = this.GetComponent<LineRenderer> ();
		line.enabled = false;

		movementKeyAxisName = "Player" + m_playerNumber + "KeyMove";
		strafeKeyAxisName = "Player" + m_playerNumber + "KeyStrafe";
		turnKeyAxisName = "Player" + m_playerNumber + "KeyTurn";
		aimKeyAxisName = "Player" + m_playerNumber + "KeyAim";
		actionKeyAxisName = "Player" + m_playerNumber + "KeyAction";
		dashKeyAxisName = " Player" + m_playerNumber + "KeyDash";

		movementJoyAxisName = "Player" + m_playerNumber + "JoyMove";
		strafeJoyAxisName = "Player" + m_playerNumber + "JoyStrafe";
		turnJoyAxisName = "Player" + m_playerNumber + "JoyTurn";
		aimJoyAxisName = "Player" + m_playerNumber + "JoyAim";
		actionJoyAxisName = "Player" + m_playerNumber + "JoyAction";
		dashJoyAxisName = " Player" + m_playerNumber + "JoyDash";

	}

	private void Update () {

		movementKeyInputValue = Input.GetAxisRaw (movementKeyAxisName);
		strafeKeyInputValue = Input.GetAxisRaw (strafeKeyAxisName);
		turnKeyInputValue = Input.GetAxisRaw (turnKeyAxisName);

		movementJoyInputValue = Input.GetAxisRaw (movementJoyAxisName);
		strafeJoyInputValue = Input.GetAxisRaw(strafeJoyAxisName);
		turnJoyInputValue = Input.GetAxisRaw (turnJoyAxisName);

	}

	private void FixedUpdate () {

		line.enabled = false;
	
		Move ();
		Turn ();

		if (Input.GetButtonDown(actionKeyAxisName)) {
			
			Action ();
		}
		if (Input.GetButtonDown(actionJoyAxisName)) {

			Action ();
		}
		if (Input.GetButton (aimKeyAxisName)) {
			
			Aim ();
		}
	}

	private void Move() {

		Vector3 keyMovement = transform.forward * movementKeyInputValue * speed * Time.deltaTime;
		Vector3 keyStrafe = transform.right * strafeKeyInputValue * speed * Time.deltaTime;

		Vector3 joyMovement = transform.forward * -movementJoyInputValue * speed * Time.deltaTime;
		Vector3 joyStrafe = transform.right * strafeJoyInputValue * speed * Time.deltaTime;;

		rb.MovePosition (rb.position + keyMovement);
		rb.MovePosition (rb.position + keyStrafe);
		rb.MovePosition (rb.position + joyMovement);
		rb.MovePosition (rb.position + joyStrafe);
	}

	private void Turn () {

		float keyTurn = turnKeyInputValue * turnSpeed * Time.deltaTime;

		Quaternion keyTurnRotation = Quaternion.Euler (0f, keyTurn, 0f);

		rb.MoveRotation (rb.rotation * keyTurnRotation);

		float joyTurn = turnJoyInputValue * turnSpeed * Time.deltaTime;

		Quaternion joyTurnRotation = Quaternion.Euler (0f, joyTurn, 0f);

		rb.MoveRotation (rb.rotation * joyTurnRotation);
	}
	private void Action() {
		RaycastHit hit;
		Ray gunShot = new Ray (transform.position, transform.forward);


		if (Physics.Raycast (gunShot, out hit, 100f) ){
			print ("Boom, you're dead!");
		}

	}

	private void Aim () {


		line.enabled = true;
		
		Ray aimLine = new Ray (transform.position, transform.forward);		
		Debug.DrawRay(transform.position, transform.forward, Color.black);

	}
}

