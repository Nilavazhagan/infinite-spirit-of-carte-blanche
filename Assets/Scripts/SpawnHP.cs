using UnityEngine;
using System.Collections;

public class SpawnHP : MonoBehaviour {
	public int counter=5;
	public GameObject Healthbox;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (counter > 0) {
			counter--;
			float xpos = Random.Range (130f, 387f);
			float zpos = Random.Range (140f, 377f);
			Vector3 position = new Vector3 (xpos, 0.5f, zpos);
			StartCoroutine (Spawn (position));
			} 
	}
	IEnumerator Spawn(Vector3 pos){
		float time = Random.Range (60f, 120f);
		yield return new WaitForSeconds (time);
		Instantiate (Healthbox, pos, Quaternion.LookRotation (Vector3.up));
	}
}
