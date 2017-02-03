using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
	[HideInInspector]
	public PlayerResults playerRes;
	public float speed;
	public float lifeTime=20f;
	float time;
	// Use this for initialization
	void Start () {
		
	}
	void Update(){
		transform.position += transform.forward * speed * Time.deltaTime;
		time += Time.deltaTime;
		if (time > lifeTime) {
			gameObject.SetActive (false);
		}
	}
	void OnTriggerEnter(Collider col){
		print ("trigger");
		if (col.tag == "Ptoszek") {
			playerRes.ptoszekShoted++;
			playerRes.points+=GameMaster.Instance.pointsForPtoszek;
			print ("ptoszek");
		}
		if (col.tag == "Bobieslaw") {
			playerRes.bobieslawShoted++;
			playerRes.points+=GameMaster.Instance.pointsForBobieslaw;
			print ("bobieslaw");
		}
		if (col.tag == "Player") {
			playerRes.playersShoted++;
			playerRes.points+=GameMaster.Instance.pointsForPlayer;
			print ("player");
		}

	}
}
