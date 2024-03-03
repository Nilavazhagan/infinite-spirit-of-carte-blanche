using UnityEngine;
using System.Collections;

public class ArmedEnemy : MonoBehaviour {
	public GameObject player;
	public GameObject bullet;
	public Transform Spawnpt;
	public float playerDistance;
	public float damping;
	public float speed;
	bool backBool = false;
	bool attackBool = false;
	bool playerDead = false;
	Animator anim;
	const int maxHealth = 320;
	int Health;
	CharacterController cont;
	// Use this for initialization
	void Start () {
		//player = GameObject.FindGameObjectWithTag ("Player");
		anim = GetComponent<Animator> ();
		cont = GetComponent<CharacterController> ();
		transform.position.Set (transform.position.x, 0, transform.position.z);
		Health = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
		if(playerDead == true)
			CancelInvoke ("Attack");
		if(playerDead == false)
		if (Health > 0) {
			if (!anim.GetCurrentAnimatorStateInfo (0).IsName ("run_forward") && !anim.GetCurrentAnimatorStateInfo (0).IsName ("run_backward"))
				GetComponents<AudioSource> () [0].Stop ();
			anim.SetBool ("Back", backBool);
			playerDistance = Vector3.Distance (player.transform.position, transform.position);
			anim.SetFloat ("Distance", playerDistance);
			transform.position.Set (transform.position.x, 0, transform.position.z);
			if (playerDistance < 30f)
				lookAtPlayer ();
			if (playerDistance > 12f && playerDistance < 27f)
				chase ();
			if (playerDistance < 7f && backBool == false)
				backBool = true;
			if (backBool == true) {
				backoff ();
				if (playerDistance >= 11.5f)
					backBool = false;
			}
			if(playerDistance>7f && playerDistance<12f && anim.GetCurrentAnimatorStateInfo (0).IsName ("Idle")){
				if(!IsInvoking())
				InvokeRepeating ("Attack",0f,2f);
			}
			else 
				CancelInvoke ("Attack");
		} else {
			CancelInvoke ("Attack");
			StartCoroutine (Die ());
		}
	}
	void lookAtPlayer(){
		Quaternion rotation = Quaternion.LookRotation (player.transform.position - transform.position);
		transform.rotation = Quaternion.Slerp (transform.rotation, rotation, Time.deltaTime * damping);
	}
	void chase(){
		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("run_forward"))
		transform.Translate (Vector3.forward * speed * Time.deltaTime);
		if(!GetComponents<AudioSource>()[0].isPlaying)
			GetComponents<AudioSource>()[0].Play();
	}
	void backoff(){
		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("run_backward"))
			transform.Translate (Vector3.back * speed * Time.deltaTime);
		if(!GetComponents<AudioSource>()[0].isPlaying)
			GetComponents<AudioSource>()[0].Play();
	}
	void ApplyDamage(int damage){
		if (Health > 0) {
			Health -= damage;
			GetComponents<AudioSource>()[1].Play();
		}
	}
	IEnumerator Die(){
		anim.SetBool ("Dead",true);
		//GetComponents<AudioSource>()[3].Play();
		yield return new WaitForSeconds (5);
		Destroy (this.gameObject);
	}
	void Attack(){
		GameObject clone = (GameObject)Instantiate (bullet, Spawnpt.position, Spawnpt.rotation);
		Destroy (clone.gameObject, 3f);
	}
	void End(){
		playerDead = true;
	}
}
