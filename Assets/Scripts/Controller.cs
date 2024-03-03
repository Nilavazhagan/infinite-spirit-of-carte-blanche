using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Controller : MonoBehaviour {
	Animator anim;
	float inputH;
	float inputV;
	public int Life;
	const int Max_Life = 1000;
	public Image bar;
	Image image;
	public bool Dead=false;
	public float sensitivity=10f;
	public static Controller Instance;
	GameObject[] objects;
	GameObject gun;
	public AudioClip grunt;
	public AudioClip death;
	float paused=1f;
	GameObject[] pause_panel;
	public Text level_text;
	float time=0f;
	public Image redbck;

	public AudioSource heart_beat;
	void Start () {
		Life = Max_Life;
		anim = GetComponent<Animator> ();
		image = bar.GetComponent<Image>();
		image.fillAmount = (float)Life / Max_Life;
		objects = GameObject.FindGameObjectsWithTag ("Stop");
		gun = GameObject.FindGameObjectWithTag ("Gun");
		pause_panel = GameObject.FindGameObjectsWithTag ("Pause");
		for(int i=0;i<pause_panel.Length;i++)
		pause_panel[i].SetActive (false);
		redbck.enabled = false;
		if (Application.loadedLevelName.Equals ("Cemetary_01"))
			level_text.text = "Doomvale Cemetary \n Stage01 ";
		if (Application.loadedLevelName.Equals ("Cemetary_02"))
			level_text.text = "Doomvale Cemetary \n Stage02 ";
	}
	
	// Update is called once per frame
	void Update () {
		if (Life <= 200 && paused == 1.0f ) {
			redbck.enabled = true;
			if(!heart_beat.GetComponent<AudioSource>().isPlaying)
			heart_beat.GetComponent<AudioSource>().Play ();
		} else {
			redbck.enabled = false;
			heart_beat.GetComponent<AudioSource>().Stop ();
		}
		objects = GameObject.FindGameObjectsWithTag ("Stop");
		Debug.Log (objects.Length);
		time += Time.deltaTime;
		if (time > 3)
			level_text.text = "";
		checkWin ();
		if (paused == 1f) {
			image.fillAmount = (float)Life / Max_Life;
			if (Dead == false) {
				inputH = Input.GetAxis ("Horizontal") * 40f;
				inputV = Input.GetAxis ("Vertical") * 20f;
				anim.SetFloat ("inputH", inputH);
				anim.SetFloat ("inputV", inputV);
				float moveX = inputV * Time.deltaTime;
				float moveZ = inputH * 0.5f * Time.deltaTime;
				this.transform.position += this.transform.forward * moveX;
				this.transform.position += this.transform.right * moveZ;
				transform.Rotate (0, Input.GetAxis ("Mouse X") * sensitivity, 0);
				if (!GetComponents<AudioSource> () [1].isPlaying)
					GetComponents<AudioSource> () [1].Play ();
				if (inputH == 0 && inputV == 0)
					GetComponents<AudioSource> () [1].Stop ();
			}
			/*if (Input.GetKeyDown (KeyCode.Z))
			Life-=100;*/
			if (Life <= 0) {
				StartCoroutine (DieSound ());
				Dead = true;
				for (int i=0; i<objects.Length; i++)
					objects [i].SendMessage ("End");
				gun.SendMessage ("End");
				level_text.text="Hubert is Extinct";
				StartCoroutine (EndLevel ());
			}
			anim.SetBool ("Dead", Dead);
		}
	if (Input.GetKeyDown (KeyCode.Escape)) {
			if (Time.timeScale == 1.0f) {
				Time.timeScale = 0.0f;
				for (int i=0; i<pause_panel.Length; i++)
					pause_panel [i].SetActive (true);
			} else {
				Time.timeScale = 1.0f;
				for (int i=0; i<pause_panel.Length; i++)
					pause_panel [i].SetActive (false);
			}
		}
			paused = Time.timeScale;
			gun.SendMessage ("Pause",paused,SendMessageOptions.DontRequireReceiver);
			for(int i=0;i<objects.Length;i++)
				objects[i].SendMessage ("Pause",paused,SendMessageOptions.DontRequireReceiver);
	}
	void Damage(int damage){
		Life -= damage;
		GetComponents<AudioSource>()[0].PlayOneShot(grunt);
	}
	void OnTriggerEnter(Collider other){
	if (other.tag == "Health Respawn") {
			IncreaseLife ();
			GetComponents<AudioSource>()[3].Play();
			Destroy (other.gameObject);
		}
		if (other.tag == "Ammo Respawn") {
			GetComponents<AudioSource>()[2].Play();
			gun.SendMessage("IncreaseAmmo",SendMessageOptions.DontRequireReceiver);
			Destroy (other.gameObject);
		}
	}
	void IncreaseLife(){
		Life += 150;
		if (Life > Max_Life)
			Life = Max_Life;
	}
	IEnumerator DieSound(){
		yield return new WaitForSeconds (0f);
		/*GetComponents<AudioSource> () [4].timeSamples = 1;
		GetComponents<AudioSource>()[4].PlayOneShot(death);*/
	}
	void checkWin(){
		if (objects.Length < 1) {
			level_text.text = "Level Cleared";
			StartCoroutine (SwitchLevel());
			PlayerPrefs.SetInt ("Lvl2Unlock", 1);
		}
	}
	IEnumerator SwitchLevel(){
		yield return new WaitForSeconds (5f);
		if (Application.loadedLevelName.Equals ("Cemetary_01"))
			Application.LoadLevel ("Cemetary_02");
		if (Application.loadedLevelName.Equals ("Cemetary_02"))
			Application.LoadLevel ("Main Menu");
	}
	IEnumerator EndLevel(){
		yield return new WaitForSeconds (5f);
		Application.LoadLevel ("Main Menu");
	}
}
