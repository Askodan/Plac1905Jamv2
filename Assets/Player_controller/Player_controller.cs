using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_controller : MonoBehaviour {

	public Animator animator;
	public float speedForward = 16.0f;
	public float speedBack = 8.0f;
	public float speedSides = 8.0f;
	public float rotationSpeedHor = 200.0f;
	public float rotationSpeedVer = 100.0f;

	private float maxHP = 100.0f;
	public float HP = 100.0f;

	public GameObject gun;
	[HideInInspector]
	public Shoot shooter;
	private List<Shoot> availableWeapon;
	private Vector3 motion;
	private float maxYSpeed = -40.0f;
	private float currentYSpeed;
	private bool canJump;
	public float cantJumpTime = 3;
	public float time2pick;
	public bool isAlive = true;
	[HideInInspector]
	public PlayerResults playerResults;
	void Awake(){
		playerResults = gameObject.AddComponent<PlayerResults> ();
	}
	// Use this for initialization
	void Start () {
		availableWeapon = new List<Shoot> ();
		Cursor.lockState = CursorLockMode.Locked;
		currentYSpeed = maxYSpeed;
		canJump = true;
		HP = maxHP;
		isAlive = true;
		StartCoroutine (GivePointsForLive ());
	}
	IEnumerator GivePointsForLive(){
		while (true) {
			yield return new WaitForSeconds (1);
			playerResults.points += GameMaster.Instance.pointsForSecond;
		}
	}
	// Update is called once per frame
	void Update ()
	{
		WSADcontrol ();
		mouseControl();
	}
	void WSADcontrol ()
	{
		CharacterController controller = GetComponent<CharacterController> ();
		///keyboard
		if (Input.GetKeyDown (KeyCode.R)) {reload();}
		if (Input.GetKeyDown (KeyCode.F)) {pickWeapon();}
		if (controller.isGrounded)
			if (Input.GetKeyDown (KeyCode.Space)) {jump();}

		///movement
		float x = Input.GetAxis ("Horizontal");
		float z = Input.GetAxis ("Vertical");
		//for jump
		currentYSpeed -= 15.0f*Time.deltaTime;
		if(currentYSpeed<maxYSpeed)
			currentYSpeed = maxYSpeed;

		motion = Vector3.Normalize (new Vector3 (x, 0.0f, z));
		if(motion.magnitude > 0)
			animator.SetBool("Walking",true);
		else
			animator.SetBool("Walking",false);
		if (z > 0)
			motion.z *= speedForward;
		else
			motion.z *= speedBack;
		motion.x *= speedSides;
		motion.y = currentYSpeed;
		
		motion = this.transform.rotation * motion;
		controller.Move (motion * Time.deltaTime);
		//print(motion);
	}
	void mouseControl ()
	{
		///buttons
		if (Input.GetMouseButton(0)) {
			shoot ();
		}
		///rotating
		this.transform.RotateAround(this.transform.position, new Vector3(0,1,0),Input.GetAxis ("Mouse X")*rotationSpeedHor*Time.deltaTime);

		//print(gunTransform.localRotation.eulerAngles.x);
		gun.transform.Rotate(-Input.GetAxis ("Mouse Y")*rotationSpeedVer*Time.deltaTime, 0, 0,Space.Self);
		float anglex = Mathf.Clamp(gun.transform.rotation.eulerAngles.x>180?gun.transform.rotation.eulerAngles.x-360:gun.transform.rotation.eulerAngles.x, -45f, 45f);
		gun.transform.localRotation =Quaternion.Euler(anglex, 0f, 0f);
	}
	void shoot()
	{
		if(shooter)
			shooter.Shot ();
	}
	void reload()
	{
		//print("Reload");
	}
	void pickWeapon()
	{
		//print("Drop");
		if (availableWeapon.Count>0) {
			animator.SetTrigger("Picking");
			StartCoroutine (PICK());
		}
	}
	IEnumerator PICK(){
		yield return new WaitForSeconds(time2pick);
		if (shooter) {
			shooter.gameObject.SetActive (false);
		}
		shooter = availableWeapon[0];
		shooter.playerRes = playerResults;
		Transform parent = availableWeapon [0].transform.parent;
		availableWeapon[0].transform.parent = gun.transform;
		availableWeapon[0].transform.localPosition = Vector3.zero;
		gun.GetComponent<GunPointer> ().nowGun = availableWeapon[0].transform;
		shooter.GetComponent<Collider> ().enabled = false;

		availableWeapon.RemoveAt (0);
		parent.gameObject.SetActive (false);
	}
	void jump ()
	{	//print("jump");
		if (canJump) {
			currentYSpeed = 15.0f;
			StartCoroutine (jumpTime ());
			animator.SetTrigger("Jumping");
		}
	}
	IEnumerator jumpTime()
	{
		canJump = false;
		yield return new WaitForSeconds(cantJumpTime);
		canJump = true;
	}


	public void takeDMG (float dmg)
	{
		HP -= dmg;
		if (HP <= 0) {
			HP = 0;
			isAlive = false;
		}
	}
	public float getHP()
	{	return HP;

	}

	void OnTriggerEnter(Collider col){
		Shoot shot = col.gameObject.GetComponent<Shoot> ();
		if(shot&&!availableWeapon.Contains(shot))
			availableWeapon.Add (shot);
	}

	void OnTriggerExit(Collider col){
		Shoot shot = col.gameObject.GetComponent<Shoot> ();
		if (availableWeapon.Contains(shot)) {
			availableWeapon.Remove(shot);
		}
	}
}
