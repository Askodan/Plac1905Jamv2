using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceWeapons : MonoBehaviour {
	public GameObject[] prefabs;
	public float[] density;
	// Use this for initialization
	void Start () {
		//wymyśliłem sobie metodę losowania z różnymi gęstościami prawdopodobieństwa :D
		if (density.Length != prefabs.Length) {
			Debug.LogWarning ("Density has length that differs from the length of the weapons, will use equal densities.");
			density = new float[prefabs.Length];
			for (int i = 0; i < density.Length; i++) {
				density [i] = 1f;
			}
		} 
		float[] distribuanta = new float[density.Length];
		float sum = 0;
		for (int i = 0; i < density.Length; i++) {
			sum += density [i];
			distribuanta [i] = sum;
		}

		Transform[] children = GetComponentsInChildren<Transform> ();
		foreach (Transform child in children) {
			if(child!=transform){
				int index = getRandomItem (Random.Range (0f, sum), distribuanta);
				if (index != -1) {
					Instantiate (prefabs [index], child.position, child.rotation, child);
				} else {
					Debug.LogWarning ("nie udało się wylosować...");
				}
			}
		}
	}
	int getRandomItem(float here, float[] distribuanta){
		for (int i = 0; i < density.Length; i++) {
			if (here <= distribuanta [i]) {
				return i;
			}
		}
		return -1;
	}
}
