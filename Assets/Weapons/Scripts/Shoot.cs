using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {
	[HideInInspector]
	//tu trzeba wsadzić skrypt, należący do gracza, który pdoniesie broń
	public PlayerResults playerRes;
	public Projectile projectile;
	public float shootFreq = 5;
	public Vector2 minMaxBank;
	public int bankSize = 10;
	public Transform spawnPoint_projectile;
	public int used = 0;
	Projectile[] projectiles;
	// Use this for initialization
	void Start () {
		if (projectile) {
			bankSize = (int)Random.Range(minMaxBank.x, minMaxBank.y);
			projectiles = new Projectile[bankSize];
			for (int i = 0; i < bankSize; i++) {
				projectiles [i] = Instantiate (projectile);
				projectiles [i].transform.SetParent (transform);
				projectiles [i].gameObject.SetActive (false);
			}
		}
	}

	public void Shot () {
		if (!firing) {
			StartCoroutine(Fire ());
		}
	}

	bool firing;
	IEnumerator Fire(){
		firing = true;
		if (used < bankSize) {
			if (!projectiles [used].gameObject.activeSelf) {
				projectiles [used].gameObject.SetActive (true);
				projectiles [used].playerRes = playerRes;
				projectiles [used].transform.position = spawnPoint_projectile.position;
				projectiles [used].transform.rotation = spawnPoint_projectile.rotation;

				used++;
			}
		}
		yield return new WaitForSeconds (1.0f/shootFreq);
		firing = false;
	}

}
