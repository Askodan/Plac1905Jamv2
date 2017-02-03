using UnityEngine;
using System.Collections;

public class WaypointSystem : MonoBehaviour {
private Animator ani;

	public GameObject [] waypoints;
	public int wayNum=-1;
	public int val;
	public UnityEngine.AI.NavMeshAgent nma;
	public float pathEndThreshold = 0.1f;
	public bool hasPath = false, first=true, allowed=true, idleEnded=false, block=false;
	bool AtEndOfPath()
	{
		if(idleEnded)
		{
			idleEnded=false;
			return true;
		}
		hasPath |= nma.hasPath;
		if (hasPath &&  nma.remainingDistance <= nma.stoppingDistance + pathEndThreshold )
		{
			// Arrived
			hasPath = false;
			return true;
		}

		return false;
	}
	void Start () {
		ani = GetComponentInChildren<Animator> ();
		nma = GetComponent<UnityEngine.AI.NavMeshAgent> ();
		StartCoroutine(lazyEnd());
		block=true;
	}
	// Update is called once per frame
	void Update ()
	{
		if((waypoints.Length>0)&&(!block))
		{
			if ((AtEndOfPath ())&&(allowed)) {
				val = (int)(Random.Range(0, waypoints.Length*2));
				if(val>=waypoints.Length)
				{
					if((val>=waypoints.Length)&&(val>1.5*waypoints.Length))
					{
						ani.SetBool("Lazy", true);
						block=true;
						StartCoroutine(lazyEnd());
					}else{
						ani.SetBool("TimeToJump", true);
						block=true;
						StartCoroutine(jumpEnd());
					}
				}else{
					nma.SetDestination (waypoints[val].transform.position);
				}
				/*if (wayNum == waypoints.Length-1)
					wayNum = 0;*/
				first = false;
			}
		}
	}
	/*public GameObject [] waypoints;
	public int wayNum=0;
	public float speed=10, speedRotate=1, rotforwardSpeed=2.5, gravity=10;
	private CharacterController cc;
	// Use this for initialization
	void Start () {
		cc = GetComponent<CharacterController> ();
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetAxis ("Vertical") > 0f) {
			cc.Move (transform.forward * speed * Time.deltaTime + Vector3.down*gravity*Time.deltaTime);
		}
		if()
			transform.Rotate (new Vector3 (0.0f, speedRotate * Time.deltaTime, 0.0f));
	}*/
	IEnumerator lazyEnd()
	{
		yield return new WaitForSeconds(5);
		idleEnded=true;
		block=false;
		ani.SetBool("Lazy", false);
	}
	IEnumerator jumpEnd()
	{
		yield return new WaitForSeconds(2);
		idleEnded=true;
		block=false;
		ani.SetBool("TimeToJump", false);
	}

}
