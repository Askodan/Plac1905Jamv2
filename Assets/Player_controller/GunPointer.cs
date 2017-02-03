using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPointer : MonoBehaviour {
	public Transform targetPosition;
	public Transform nowGun;
	public Transform targetRotation;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = targetPosition.position;
		if(nowGun)
			nowGun.LookAt (targetRotation.position);
	}
}
