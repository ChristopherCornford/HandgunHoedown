using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {

	/* Managers */
	//public PlayerManager playerManager;
	private GameManager GameManager;
	private UI_Manager UI_Manager;
	private GunSpawn GunSpawn;
	
	/* Public */
	[Header("Player Variables")]
	private Vector3 gunShot;
	public GameObject movement;
	public int m_playerNumber = 0;
	public float speed = 12f;
	public float turnSpeed = 180f;
	public bool canBeStunned;
	[Range (0, 10)]
	public float walkSpeed;
	[Range (0, 10)]
	public float aimSpeed;
	[Range (0, 15)]
	public float sprintSpeed;
	[Range (0, 3)]
	public float stunTimer;

	[Header("Sprint Varibles")]
	public float sprintCD = 1.5f;
	public bool isAbleToSprint;
	public bool isSprinting;
	public bool canAim;
	public bool isAiming;
	public Slider slider;
	public Image fillImage;
	public Color fillColor;
	public Color playerOneColor;
	public Color playerTwoColor;

	//Picking up Gun
	[Header("Gun References & Variables")]
	public GameObject gunHolder;
	public GameObject gunPickup;
	public ParticleSystem gunParticle;
	public bool hasGun;
	public int bulletCount;

	/* Private */
	// Components
	private SoundManager SoundManager;
	public Animator cowboy_anim;

	// Axis Names
	private string movementKeyAxisName;
	private string strafeKeyAxisName;
	private string turnKeyAxisName;
	private string aimKeyAxisName;
	private string actionKeyAxisName;


	private string movementJoyAxisName;
	private string strafeJoyAxisName;
	private string turnJoyAxisNameX;
	private string turnJoyAxisNameY;
	private string aimJoyAxisName;
	private string actionJoyAxisName;


	private Rigidbody rb;
	public LineRenderer line;
	public GameObject lineRend;

	public float horizontal;
	public float verticle;
	private float movementKeyInputValue;
	private float strafeKeyInputValue;
	private float turnKeyInputValue;
	private float movementJoyInputValue;
	private float strafeJoyInputValue;
	private float turnJoyInputValueX;
	private float turnJoyInputValueY;

	private void Awake () {
		rb = GetComponent<Rigidbody> ();
		cowboy_anim = GetComponent<Animator>();
		GameManager = GameObject.Find("/Managers/GameManager").GetComponent<GameManager>();
		UI_Manager = GameObject.Find("/Managers/UI_Manager").GetComponent<UI_Manager>();
		GunSpawn = GameObject.Find ("/Managers/GunSpawner").GetComponent<GunSpawn> ();
		SoundManager = GameObject.Find ("/Managers/SoundManager").GetComponent<SoundManager> ();
		line = lineRend.GetComponent<LineRenderer> ();
	}

	private void OnEnable() {

		rb.isKinematic = false;
		sprintCD = 1.5f;
		SetSprintUI ();

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
		canBeStunned = true;

		movementKeyAxisName = "Player" + m_playerNumber + "KeyMove";
		strafeKeyAxisName = "Player" + m_playerNumber + "KeyStrafe";
		turnKeyAxisName = "Player" + m_playerNumber + "KeyTurn";
		aimKeyAxisName = "Player" + m_playerNumber + "KeyAim";
		actionKeyAxisName = "Player" + m_playerNumber + "KeyAction";

		movementJoyAxisName = "Player" + m_playerNumber + "JoyMove";
		strafeJoyAxisName = "Player" + m_playerNumber + "JoyStrafe";
		turnJoyAxisNameX = "Player" + m_playerNumber + "JoyTurnX";
		turnJoyAxisNameY = "Player" + m_playerNumber + "JoyTurnY";
		aimJoyAxisName = "Player" + m_playerNumber + "JoyAim";
		actionJoyAxisName = "Player" + m_playerNumber + "JoyAction";
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
			Aim ();
		}

		if (Input.GetButtonUp("Pause")){
			GameManager.Pause();
		}

		if (sprintCD == 1.5f) {
			isAbleToSprint = true;
		}
		if (sprintCD <= 0f) {
			isAbleToSprint = false;
		}
			
		StartCoroutine (checkForAiming(0.1f));
		isAiming = false;
		canAim = true;
		StartCoroutine (checkForSprinting(0.1f));
		isSprinting = false;

	}

	void FixedUpdate () {
		
		Move ();
		Turn ();
	}

	public void Move() {

		if(strafeJoyInputValue != 0 || movementJoyInputValue != 0) {
			horizontal = strafeJoyInputValue * Time.deltaTime * speed * 5;
			verticle = movementJoyInputValue * Time.deltaTime * speed * 5;

			Vector3 moveDirection = new Vector3 (strafeJoyInputValue, 0, movementJoyInputValue);
			Vector3 moveAngle = new Vector3 (0, 0, 45);
			transform.position += moveDirection * speed * Time.deltaTime;
		}


		Vector3 keyMovement = Vector3.forward * movementKeyInputValue * speed * Time.deltaTime;
		Vector3 keyStrafe = Vector3.right * strafeKeyInputValue * speed * Time.deltaTime;

		rb.MovePosition (rb.position + keyMovement);
		rb.MovePosition (rb.position + keyStrafe);

		if (movementKeyInputValue != 0) {
			cowboy_anim.SetBool ("isMoving", true);
		} else if (strafeKeyInputValue != 0) {
			cowboy_anim.SetBool ("isMoving", true);
		} else {
			cowboy_anim.SetBool("isMoving", false);
		}

		if (movementJoyInputValue != 0) {
			cowboy_anim.SetBool ("isMoving", true);
		} else if (strafeJoyInputValue != 0) {
			cowboy_anim.SetBool ("isMoving", true);
		} else {
			cowboy_anim.SetBool("isMoving", false);
		}
	}

	private void Turn () {

		float keyTurn = turnKeyInputValue * turnSpeed * Time.deltaTime;

		Quaternion keyTurnRotation = Quaternion.Euler (0f, keyTurn, 0f);

		rb.MoveRotation (rb.rotation * keyTurnRotation);

		if(turnJoyInputValueX != 0 || turnJoyInputValueY != 0){
			float joyTurnX = turnJoyInputValueX * turnSpeed * Time.deltaTime;
			float joyTurnY = -turnJoyInputValueY * turnSpeed * Time.deltaTime;

			float angle = Mathf.Atan2 (joyTurnX, joyTurnY) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler (new Vector3(0, angle));
	
		}
	}
	private void Action() {

		if (hasGun == false) {
			RaycastHit punch;
			Ray throwPunch = new Ray (transform.position, transform.forward);
			if (Physics.Raycast (throwPunch, out punch, 1.75f)) {
				punch.transform.SendMessage ("Punch");
			}
			cowboy_anim.SetTrigger ("isPunching");
			SoundManager.Punch(false);
		}

		if ((hasGun == true) && (bulletCount > 0)){
			bulletCount -= 1;
			StartCoroutine (checkForBullets (0.1f));
			UI_Manager.removeBullets(m_playerNumber, bulletCount);
			SoundManager.Shoot ();
			gunParticle.Play();
			RaycastHit hit;
			Ray gunShot = new Ray (transform.position, transform.forward);
		
			if (Physics.Raycast (gunShot, out hit, 100f)) {
				if (hit.transform.tag == "Cowboy") {
					hit.transform.SendMessage ("YouAreDead");
					canAim = false;
					line.enabled = false;
					StartCoroutine(GameManager.RoundEnd(m_playerNumber));
				}
				else {SoundManager.Miss();}
			}	
		}
	}

	private void Aim () {
		if (hasGun == true && canAim == true) {
			isAiming = true;
			RaycastHit aimLine;

			if (Physics.Raycast (transform.position, transform.forward, out aimLine)) {
				if (aimLine.collider) {
					line.SetPosition (0, new Vector3 (0, 0, aimLine.distance));
				}
			} else {
				line.SetPosition(0, new Vector3( 0, 0 , 100f));
			}
		
		}

		if ((hasGun == false) && (isAbleToSprint == true)){
			isSprinting = true;
		}

	}
	
	public IEnumerator checkForAiming (float sec) {

		switch (isAiming) {
		case true:
			line.enabled = true;
			speed = aimSpeed;
			turnSpeed = 90f;
			break;

		case false:
			line.enabled = false;
			speed = walkSpeed;
			turnSpeed = 180f;
			break;

		}
		yield return new WaitForSeconds (sec);
		//isAiming = false;
	}
	
	public IEnumerator checkForSprinting (float sec){

		switch(isSprinting){
		case true:
			speed = sprintSpeed;
			sprintCD -= Time.deltaTime;
			SetSprintUI ();
			break;

		case false:
			if (isAiming == true) {
				speed = aimSpeed;
			} else {
				speed = walkSpeed;
			}
			sprintCD += Time.deltaTime;
			SetSprintUI ();
			if (sprintCD >= 1.5f) {
				sprintCD = 1.5f;
			}
			break;
		}
		yield return new WaitForSeconds (sec);
	}

	public IEnumerator checkForBullets ( float sec) {

		if (bulletCount == 0 ) {
			Reset ();
			GunSpawn.numGuns--;
			StartCoroutine(GunSpawn.SpawnGun(GameManager.Gun_Spawn_Wait));
			}
		yield return new WaitForSeconds (0.1f);
	}

	void OnTriggerEnter (Collider collider) {
		if ((collider.gameObject.tag == "Gun") && (hasGun == true)) {
			bulletCount = 6;
			UI_Manager.giveBullets(m_playerNumber);
			GunSpawn.numGuns--;
			StartCoroutine(GunSpawn.SpawnGun(GameManager.Gun_Spawn_Wait));
		}
		if ((collider.gameObject.tag == "Gun") && (hasGun == false)) {
			cowboy_anim.SetBool("hasGun", true);
			gunHolder.SetActive(true);
			hasGun = true;
			bulletCount = 6;
			UI_Manager.giveBullets(m_playerNumber);
		}
	}
	void YouAreDead () {
		cowboy_anim.SetTrigger ("isDead");
	}

	void Punch () {
		SoundManager.Punch(true);
		cowboy_anim.SetTrigger ("isPunched");
		if (canBeStunned == true) {
			if (hasGun == true) {
				Reset ();
				GunSpawn.numGuns--;
				StartCoroutine(GunSpawn.SpawnGun(GameManager.Gun_Spawn_Wait));
			}
			StartCoroutine (Stun (0.01f));
		}
	}

	public void Reset(){
		cowboy_anim.Play("Idle");
		cowboy_anim.SetBool("hasGun", false);
		hasGun = false;
		gunHolder.SetActive (false);
		bulletCount = 0;
		UI_Manager.removeBullets(m_playerNumber, bulletCount);
	}

	public void SetSprintUI() {
		slider.value = sprintCD;
		switch (m_playerNumber) {
		case 1:
			fillColor = playerOneColor;
			break;

		case 2:
			fillColor = playerTwoColor;
			break;
		}
		fillImage.color = fillColor;
	}

	public IEnumerator Stun (float sec) {
		SoundManager.Punch(true);
		canBeStunned = false;
		this.enabled = false;
		yield return new WaitForSeconds (stunTimer);
		this.enabled = true;
		yield return new WaitForSeconds (1.0f);
		canBeStunned = true;
	}
}

