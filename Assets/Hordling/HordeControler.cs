using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HordeControler : MonoBehaviour {
	public GameObject [] members;
	public GameObject waypointParent;
	public GameObject [] hordeWaypoints;
	private int cnt = 0;
	public int minionNumber;
	public GameObject [] minion;
	public GameObject player;

	void Awake()
	{
		GenerateMinions ();
	}

	void GenerateMinions()
	{
		members = new GameObject[minionNumber];

		for (int i = 0; i < minionNumber; i++) {
			GameObject Minion = Instantiate(minion[Random.Range(0,1)],Random.insideUnitSphere*3+transform.position,transform.rotation,transform);
			members [i] = Minion;
		}
	}
	public void TargetAcquired (GameObject player)
	{
		foreach (Transform child in transform)
		{
			EnemyController ec = child.gameObject.GetComponent<EnemyController>();
			if(ec!=null)
				ec.TargetAcquired(player);
		}
	}

	// Use this for initialization
	void Start () {
		hordeWaypoints = new GameObject [waypointParent.transform.childCount];
		int i=0;
		foreach (Transform child in waypointParent.transform)
		{
			hordeWaypoints[i++]=child.gameObject;
		}
	}
	/*bool check (int [] usedIndexes, int index)
	{
		bool ok=true;
		for(int j=0;j<usedIndexes.Length;j++)
		{
			if(index==usedIndexes[j])
			{
				ok=false;
			}
		}
		return ok;
	}*/
	void Update()
	{
		if(cnt<members.Length)
		{
			/*GameObject [] privateWayArray=new GameObject[hordeWaypoints.Length];
			int [] usedIndexes = new int [hordeWaypoints.Length];
			int index=-1;
			for(int i=0;i<privateWayArray.Length;i++)
			{
				do{

					index=(int)(Random.Range(0, hordeWaypoints.Length));
					privateWayArray[i]=hordeWaypoints[index];
				}while(!check(usedIndexes,index));
				usedIndexes[i]=index;
			}*/
			members[cnt].GetComponent<WaypointSystem>().waypoints=hordeWaypoints;
			cnt++;
		}
	}
}
