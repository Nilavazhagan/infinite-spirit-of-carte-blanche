using UnityEngine;
using System.Collections;

public class BulletMovement : MonoBehaviour {
	public GameObject player;
	Vector3 dir;
	int accuracy = 3;
	// Use this for initialization
	void Awake () {
		player = GameObject.Find ("player");
		Vector3 tempposition = player.transform.position;
		float randx = Random.Range (-accuracy, accuracy);
		float randz = Random.Range (-accuracy, accuracy);
		tempposition.x += randx;
		tempposition.y = 2f;
		tempposition.z += randz; 
		transform.rotation = Quaternion.LookRotation (tempposition - transform.position);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.forward * Time.deltaTime * 100f);
		//transform.position += transform.forward * Time.deltaTime * 50f;
	}
	void OnTriggerEnter(Collider other){
		if(other.CompareTag ("Player"))
		other.SendMessage ("Damage", 50, SendMessageOptions.DontRequireReceiver);
	}
}
