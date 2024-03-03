using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {
	GameObject player;
	public float playerDistance;
	public float damping;
	public float speed;
	Animator anim;
	//Animator playeranim;
	BoxCollider box;
	string trigger="Attack";
	bool transition2;
	bool attackflag;
	bool playerDead=false;
	int i;
	bool paused=false;
	bool chaseBool=false;
	public AudioClip scream;
	public int Health=400;
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		anim = GetComponent<Animator> ();
		//playeranim = player.GetComponent<Animator> ();
		box = gameObject.GetComponentInChildren<BoxCollider> ();
		attackflag = false;
		transition2 = false;
	}
	void Update () {
		if (paused == false) {
			if (!anim.GetCurrentAnimatorStateInfo (0).IsName ("standing_run_forward"))
				GetComponents<AudioSource> () [1].Stop ();
			if (playerDead == true) {
				anim.SetBool ("Idle", true);
				for (i=1; i<=5; i++) {
					trigger = "Attack" + i.ToString ();
					anim.SetBool (trigger, false);
				}
			} else {
				box.enabled = false;
				if (Health > 0) {
					playerDistance = Vector3.Distance (player.transform.position, transform.position);
					if(chaseBool==false)
					anim.SetFloat ("Distance", playerDistance);
					if(playerDistance>22f && chaseBool==true){
						lookAtPlayer ();
						anim.SetFloat ("Distance", 21f);
						chase ();
						StartCoroutine (Delay());
					}
					if (playerDistance < 25f)
						lookAtPlayer ();
					if (playerDistance > 3f && playerDistance < 22f) {
					    chaseBool = false;
						chase ();
					}
					if ((playerDistance < 3f) && (anim.GetCurrentAnimatorStateInfo (0).IsName ("standing_idle"))) {
						int Trigger = Random.Range (1, 5);
						for (i=1; i<=5; i++) {
							trigger = "Attack" + i.ToString ();
							if (i == Trigger)
								anim.SetBool (trigger, true);
							else
								anim.SetBool (trigger, false);
						}
						box.enabled = true;
					} 
				} else {
					StartCoroutine (Die ());
				}
				transition2 = (anim.GetAnimatorTransitionInfo (0).IsName ("standing_idle -> standing_melee_attack_360_high")) || (anim.GetAnimatorTransitionInfo (0).IsName ("standing_idle -> standing_melee_attack_360_low")) || (anim.GetAnimatorTransitionInfo (0).IsName ("standing_idle -> standing_melee_attack_backhand")) || (anim.GetAnimatorTransitionInfo (0).IsName ("standing_idle -> standing_melee_attack_downward")) || (anim.GetAnimatorTransitionInfo (0).IsName ("standing_idle -> standing_melee_attack_horizontal"));
				if ((transition2) && (playerDistance < 3f) && attackflag == false) {
					attackflag = true;
					StartCoroutine (DoDamage ());
				}
			}
		}
	}
		void lookAtPlayer(){
		Quaternion rotation = Quaternion.LookRotation (player.transform.position - transform.position);
		transform.rotation = Quaternion.Slerp (transform.rotation, rotation, Time.deltaTime * damping);
	}
	void chase(){
		if(anim.GetCurrentAnimatorStateInfo (0).IsName ("standing_run_forward"))
		transform.Translate (Vector3.forward * speed * Time.deltaTime);
		if(!GetComponents<AudioSource>()[1].isPlaying)
			GetComponents<AudioSource>()[1].Play();
    }
	void ApplyDamage(int damage){
		if (Health > 0) {
			Health -= damage;
			StartCoroutine (shout ());
			anim.SetBool ("Hit", true);
			StartCoroutine (Reset ());
			chaseBool = true;
		}
		//anim.SetBool ("Chase",chaseBool);
	}
	IEnumerator Die(){
		anim.SetBool ("Dead",true);
		//GetComponents<AudioSource>()[3].Play();
		yield return new WaitForSeconds (5);
		Destroy (gameObject);
	}
	IEnumerator DoDamage(){
		yield return new WaitForSeconds (1f);
		if (!anim.GetBool ("Hit")) {
			GetComponents<AudioSource> () [2].Play ();
			if(playerDistance < 3f){
				player.SendMessage ("Damage", 75, SendMessageOptions.DontRequireReceiver);
			}
		}
		yield return new WaitForSeconds (1f);
		attackflag = false;
	}
	IEnumerator shout(){
		yield return new WaitForSeconds (0.25f);
		GetComponent<AudioSource> ().PlayOneShot (scream);
	}
	void End(){
		playerDead = true;
	}
	IEnumerator MakeSound(){
		yield return new WaitForSeconds (0.2f);
		GetComponents<AudioSource>()[2].Play();
	}
	void Pause(float p){
		if (p == 1.0f)
			paused = false;
		else
			paused = true;
	}
	IEnumerator Delay(){
		yield return new WaitForSeconds(3f);
		chaseBool = false;
	}
	IEnumerator Reset(){
		yield return new WaitForSeconds (0.5f);
		anim.SetBool ("Hit", false);
	}
}