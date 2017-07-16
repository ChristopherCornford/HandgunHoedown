using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	/* Public */
	[Header("Player Variables")]
	private Vector3 gunShot;
	public GameObject movement;
	public int m_playerNumber = 0;
	public float speed = 12f;
	public float turnSpeed = 180f;

	/* Private */
	// Components
	private PlayerShooting shooting;
	private Animator cowboy_anim;

	// Axis Names
	private string movementKeyAxisName;
	private string strafeKeyAxisName;
	private string turnKeyAxisName;
	private string aimKeyAxisName;
	private string actionKeyAxisName;
	private string dashKeyAxisName;

	private string movementJoyAxisName;
	private string strafeJoyAxisName;
	private string turnJoyAxisName;
	private string aimJoyAxisName;
	private string actionJoyAxisName;
	private string dashJoyAxisName;

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
		shooting = GetComponent<PlayerShooting>();
		cowboy_anim = GetComponent<Animator>();
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

		// How the fuck do we detect when someone is or is not moving?
		// cowboy_anim.SetBool("isMoving", true);
		Move ();
		Turn ();

		if (Input.GetButtonDown(actionKeyAxisName)) {
			Action();
		}
		if (Input.GetButtonDown(actionJoyAxisName)) {

			Action();
		}
		if (Input.GetButton (aimKeyAxisName)) {
			
			Aim ();
		}
	}

	private void Move() {
		
		Vector3 keyMovement = transform.forward * movementKeyInputValue * speed * Time.deltaTime;
		Vector3 keyStrafe = transform.right * strafeKeyInputValue * speed * Time.deltaTime;

		Vector3 joyMovement = transform.forward * -movementJoyInputValue * speed * Time.deltaTime;
		Vector3 joyStrafe = transform.right * strafeJoyInputValue * speed * Time.deltaTime;

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

	// Consider moving this function out of this script into the playershooting?
	private void Action() {
		RaycastHit hit;
		Ray gunShot = new Ray (transform.position, transform.forward);
		
		// This activates the gunshot method in PlayerShooting to make the sound
		shooting.Shoot();

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

