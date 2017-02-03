using UnityEngine;
using System.Collections;

public class SpringTarget : MonoBehaviour {
    public Transform target;
    public Vector3 rotOffset;
    public Transform kulka;

    Vector3 positionInit;
    Quaternion rotInit;
    Vector3 scaleInit;
    float sqrdist;
    // Use this for initialization
	void Start () {
        positionInit = target.localPosition;
        rotInit = transform.localRotation;
        scaleInit = transform.localScale;
        sqrdist = (target.localPosition).sqrMagnitude;
	}
	
	// Update is called once per frame
	void Update () {
        transform.localRotation = Quaternion.Euler(rotOffset) * Quaternion.LookRotation(target.localPosition)* Quaternion.Inverse(rotInit);
        kulka.position = target.position;
        transform.localScale = Vector3.one*Mathf.Sqrt((target.localPosition).sqrMagnitude/sqrdist);
	}
}
