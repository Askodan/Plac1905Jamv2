using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
	//public GameObject [] player;
	public HordeControler hc;
	public GameObject targetedPlayer=null;

	private Animator ani;

	public WaypointSystem ws;
	public UnityEngine.AI.NavMeshAgent nma;
	public float range = 100, losingRange = 200;
	public bool agsActive=false;
	// Use this for initialization
	void Start () {
		ani = GetComponentInChildren<Animator> ();
		ws = GetComponent<WaypointSystem> ();
		nma = GetComponent<UnityEngine.AI.NavMeshAgent> ();
		hc = transform.parent.gameObject.GetComponent<HordeControler> ();
		StartCoroutine(checkPlayerPos());
	}
	public void TargetAcquired (GameObject player)
	{
		agsActive=true;
		targetedPlayer=player;
	}

	// Update is called once per frame
	void Update () {
	}
	IEnumerator checkPlayerPos()
	{
		while (true) {
			// suspend execution for 2 seconds
			yield return new WaitForSeconds (0.5f);
			if(targetedPlayer!=null)
			{
				ws.allowed = false;
				nma.SetDestination(targetedPlayer.transform.position+transform.forward/2);
			}
			/*for(int i=0;i<GameMaster.Instance.players.Length;i++)
			{
				if (!agsActive&&((GameMaster.Instance.players[i].transform.position - gameObject.transform.position).sqrMagnitude < range)) {
					agsActive = true;
					ws.allowed = false;
					nma.SetDestination(GameMaster.Instance.players[i].transform.position);
					StartCoroutine (leave());
				}
			}*/ /*else {
				if ((player.transform.position - gameObject.transform.position).sqrMagnitude < losingRange) {
					agsActive = false;
					ws.allowed = true;
					nma.stoppingDistance = 3;
				}
			}*/
		}
	}
	IEnumerator leave()
	{
		// suspend execution for 5 seconds
		yield return new WaitForSeconds(30);
		for(int i=0;i<GameMaster.Instance.players.Length;i++)
		{
			if ((GameMaster.Instance.players[i].transform.position - gameObject.transform.position).sqrMagnitude < range) {
				nma.stoppingDistance = 0;
			}else{
				agsActive = false;
				ws.allowed = true;
				nma.SetDestination (ws.waypoints [ws.wayNum].transform.position);
			}
		}
	}
}
