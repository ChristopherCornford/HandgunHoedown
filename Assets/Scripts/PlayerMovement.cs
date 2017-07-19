using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	/* Managers */
	private GameManager gamemanager;
	private UI_Manager UI_Manager;
	
	/* Public */
	[Header("Player Variables")]
	private Vector3 gunShot;
	public GameObject movement;
	public int m_playerNumber = 0;
	public float speed = 12f;
	public float turnSpeed = 180f;
	public bool isAiming;

	//Picking up Gun
	[Header("Gun References & Variables")]
	public GameObject gunHolder;
	public GameObject gunPickup;
	public GameObject gun;
	private bool hasGun;
	public int bulletCount;

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
	private string turnJoyAxisNameX;
	private string turnJoyAxisNameY;
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
	private float turnJoyInputValueX;
	private float turnJoyInputValueY;

	private void Awake () {
		
		rb = GetComponent<Rigidbody> ();
		shooting = GetComponent<PlayerShooting>();
		cowboy_anim = GetComponent<Animator>();
		gamemanager = GameObject.Find("/Managers/GameManager").GetComponent<GameManager>();
		UI_Manager = GameObject.Find("/Managers/UI_Manager").GetComponent<UI_Manager>();
	}

	private void OnEnable() {

		rb.isKinematic = false;

		movementKeyInputValue = 0f;
		strafeKeyInputValue = 0f;
		turnKeyInputValue = 0f;

		movementJoyInputValue = 0f;
		strafeJoyInputValue = 0f;
		turnJoyInputValueX = 0f;
		turnJoyInputValueY = 0f;
	}

	private void OnDisable () {

		rb.isKinematic = true;
	
	}

	private void Start () {
		gunHolder.SetActive(false);
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
		turnJoyAxisNameX = "Player" + m_playerNumber + "JoyTurnX";
		turnJoyAxisNameY = "Player" + m_playerNumber + "JoyTurnY";
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
		turnJoyInputValueX = Input.GetAxisRaw (turnJoyAxisNameX);
		turnJoyInputValueY = Input.GetAxisRaw (turnJoyAxisNameY);

		if (Input.GetButtonDown (actionKeyAxisName)) {
			Action();
		}
		if (Input.GetButtonDown (actionJoyAxisName)) {

			Action();
		}
		if (Input.GetButton (aimKeyAxisName)) {

			Aim ();
		}
		if (Input.GetButton (aimJoyAxisName)) {
			print ("maybe?");
			Aim ();
		}
		if (Input.GetButtonDown (dashKeyAxisName)) {
			Dash ();
		}
		if (Input.GetButtonDown (dashJoyAxisName)) {
			Dash ();
		}
			
		StartCoroutine (checkForAiming(0.1f));
		isAiming = false;
	}

	private void FixedUpdate () {

		line.enabled = false;

		// How the fuck do we detect when someone is or is not moving?
		// Once we can do that, I can set animation parameters
		// cowboy_anim.SetBool("isMoving", true);
		Move ();
		Turn ();


	}

	public void Move() {
		
		Vector3 keyMovement = transform.forward * movementKeyInputValue * speed * Time.deltaTime;
		Vector3 keyStrafe = transform.right * strafeKeyInputValue * speed * Time.deltaTime;

		Vector3 joyMovement = transform.forward * -movementJoyInputValue * speed * Time.deltaTime;
		Vector3 joyStrafe = transform.right * strafeJoyInputValue * speed * Time.deltaTime;

		rb.MovePosition (rb.position + keyMovement);
		rb.MovePosition (rb.position + keyStrafe);
		rb.MovePosition (rb.position + joyMovement);
		rb.MovePosition (rb.position + joyStrafe);

		if (movementKeyInputValue != 0) {
			cowboy_anim.SetBool ("isMoving", true);
		} else {
			cowboy_anim.SetBool("isMoving", false);
		}
		if (movementJoyInputValue != 0) {
			cowboy_anim.SetBool ("isMoving", true);
		} else {
			cowboy_anim.SetBool("isMoving", false);
		}
	}

	private void Turn () {

		float keyTurn = turnKeyInputValue * turnSpeed * Time.deltaTime;

		Quaternion keyTurnRotation = Quaternion.Euler (0f, keyTurn, 0f);

		rb.MoveRotation (rb.rotation * keyTurnRotation);

		float joyTurnX = turnJoyInputValueX * turnSpeed * Time.deltaTime;
		float joyTurnY = turnJoyInputValueY * turnSpeed * Time.deltaTime;

		Quaternion joyTurnRotation = Quaternion.Euler (joyTurnX, 0.0f, joyTurnY);

		rb.MoveRotation (rb.rotation * joyTurnRotation);


		/*Vector3 nextDir = new Vector3 (turnJoyInputValueX, turnJoyInputValueY);
		if (nextDir != Vector3.zero) {
			transform.rotation = Quaternion.LookRotation (nextDir);
		}*/
	}

	// Consider moving this function out of this script into the playershooting?
	private void Action() {
		if ((hasGun == true) && (bulletCount > 0)){
			RaycastHit hit;
			Ray gunShot = new Ray (transform.position, transform.forward);
		
			// This activates the gunshot method in PlayerShooting to make the sound
			shooting.Shoot ();

			if (Physics.Raycast (gunShot, out hit, 100f)) {
				print ("Boom, you're dead!");
				hit.transform.SendMessage ("YouAreDead");
				gamemanager.RoundEnd(m_playerNumber);
			}
			bulletCount -= 1;
			UI_Manager.removeBullets(m_playerNumber, bulletCount);
		}
	}

	private void Aim () {
		isAiming = true;
		line.enabled = true;
		
		Ray aimLine = new Ray (transform.position, transform.forward);		
		Debug.DrawRay(transform.position, transform.forward, Color.black);

	}
	public void Dash () {
		Vector3 dash = (transform.forward * Time.deltaTime * (speed * 2));
		rb.MovePosition (rb.position + dash);

	}
	public IEnumerator checkForAiming (float sec) {

		switch (isAiming) {
		case true:
			speed = 6f;
			turnSpeed = 90f;
			break;

		case false:
			speed = 12f;
			turnSpeed = 180f;
			break;

		}
		yield return new WaitForSeconds (sec);
	}

	void OnTriggerEnter (Collider collider) {

		if ((collider.gameObject.tag == "Gun") && (hasGun == false)) {
			print ("Got It!");
			gunHolder.SetActive(true);
			Destroy (gunPickup);
			hasGun = true;
			bulletCount = 6;
			cowboy_anim.SetBool("hasGun", true);
			UI_Manager.GiveBullets(m_playerNumber);
		}
	}
	void YouAreDead () {
		cowboy_anim.SetTrigger ("isDead");
	}
}

