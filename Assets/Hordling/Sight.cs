using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sight : MonoBehaviour {

	public EnemyController ec;
	void Start () {
		ec=transform.parent.gameObject.GetComponent<EnemyController> ();
	}
	void OnTriggerEnter(Collider other) {
		if (other.tag=="Player")
		{
			ec.hc.TargetAcquired (other.gameObject);
		}
	}
}
