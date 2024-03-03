using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BulletScript : MonoBehaviour {
	GameObject player;
	public GameObject Effect;
	public float damage = 40;
	public GameObject BulletTracer;
	ParticleSystem Tracer;
	public GameObject BulletTracer1;
	ParticleSystem Tracer1;
	Animator anim;
	public AudioSource shoot;
	public AudioSource empty;
	AudioSource[] audios;
	public int ammo=240;
	public int clip=30;
	public Text AmmoText;
	public bool isReloading=false;
	bool playerDead=false;
	int temp;
	bool paused=false;
	void Start(){
		player = GameObject.FindGameObjectWithTag ("Player");
		anim = player.GetComponent<Animator> ();
		Tracer = BulletTracer.GetComponent<ParticleSystem> ();
		Tracer.enableEmission = false;
		Tracer1 = BulletTracer1.GetComponent<ParticleSystem> ();
		Tracer1.enableEmission = false;
		audios=GetComponents<AudioSource>();
		shoot = audios [0];
		empty = audios [1];
		AmmoText.text = clip.ToString () + "/" + ammo.ToString ();
	}
	void Update(){
		if (paused==false) {
			if (playerDead == false) {
				Tracer.enableEmission = false;
				Tracer1.enableEmission = false;
				RaycastHit hit;
				Ray ray = Camera.main.ScreenPointToRay (new Vector3 (Screen.width * 0.5f, Screen.height * 0.5f, 0f));
				if (!isReloading) {
					AmmoText.text = clip.ToString () + "/" + ammo.ToString ();	
					if (clip > 0) {
						if (Input.GetMouseButtonDown (0)) {
							clip--;
							shoot.Play ();
							if (anim.GetCurrentAnimatorStateInfo (0).IsName ("idle_aiming"))
								Tracer.enableEmission = true;
							else
								Tracer1.enableEmission = true;
							if (Physics.Raycast (ray, out hit, 60)) {
								Object sparks = Instantiate (Effect, hit.point, Quaternion.LookRotation (hit.normal));
								Destroy (sparks, 1f);                               
								hit.transform.SendMessage ("ApplyDamage", damage, SendMessageOptions.DontRequireReceiver);
							
								/*Quaternion rot = Quaternion.LookRotation (Tracer.transform.position - hit.transform.position);
								Tracer.transform.rotation = Quaternion.Lerp (Tracer.transform.rotation,rot,0.1f);
								Tracer.enableEmission = true; */
							}
						}
					} else if ((ammo > 0 && clip == 0)) {
						isReloading = true;
						StartCoroutine (Reload ());
					} else if (ammo == 0 && clip == 0) {
						if (Input.GetMouseButtonDown (0))	
							empty.Play ();
					}
					if ((Input.GetKeyDown (KeyCode.R)) && clip < 30) {
						isReloading = true;
						StartCoroutine (Reload ());
					}
				}
			}	
		}
	}
	IEnumerator Reload(){
		temp = 30 - clip;
		if (ammo < temp)
			temp = ammo;
		clip += temp;
		ammo -= temp;
		AmmoText.text = "Reloading...";
		yield return new WaitForSeconds(3);
		isReloading = false;
	}
	void End(){
		playerDead = true;
	}
	void IncreaseAmmo(){
		ammo += 50;
		if (ammo > 240)
			ammo = 240;
	}
	void Pause(float p){
		if (p == 1.0f)
			paused = false;
		else
			paused = true;
	}
}

