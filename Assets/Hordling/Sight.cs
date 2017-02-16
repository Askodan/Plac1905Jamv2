using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sight : MonoBehaviour {

	private EnemyController ec;
	void Start () {
		ec=transform.parent.gameObject.GetComponent<EnemyController> ();
	}
	void OnTriggerEnter(Collider other) {
		if (other.tag=="Player")
		{
			if(ec.targetedPlayer==null)
				ec.hc.TargetAcquired (other.gameObject);
		}
	}
}
