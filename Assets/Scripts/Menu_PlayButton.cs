using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Menu_PlayButton : MonoBehaviour {
	public ScrollRect sr;
	GameObject[] hide;
	GameObject[] unhide;
	GameObject[] exit;
	GameObject[] setting;
	GameObject load;
	GameObject level;
	GameObject lvl2;
	public Toggle t1;
	bool muteaudio;
	public GameObject controls;
	void Awake(){
		Cursor.visible = true;
	}
	void Start(){
		level = GameObject.FindGameObjectWithTag ("Level_Select");
		hide=GameObject.FindGameObjectsWithTag("UI_Hide");
		unhide=GameObject.FindGameObjectsWithTag("UI_UnHide");
		for (int j=0; j<unhide.Length; j++)
			unhide [j].SetActive (false);
		load = GameObject.Find ("Loading");
		load.SetActive (false);
		exit = GameObject.FindGameObjectsWithTag ("Exit");
		for (int i=0; i<exit.Length; i++)
		exit[i].SetActive (false);
		setting = GameObject.FindGameObjectsWithTag ("Settings");
		for (int i=0; i<setting.Length; i++)
			setting [i].SetActive (false);
		//controls = GameObject.FindGameObjectWithTag ("Controls");
		level.SetActive(false);
		lvl2 = GameObject.FindGameObjectWithTag("Lvl_02");
		if (PlayerPrefs.GetInt ("Lvl2Unlock") == 1)
			lvl2.GetComponent<Button>().interactable = true;
	}
	void Update(){
		if (sr.IsActive ()&& sr.verticalNormalizedPosition!=0f)
			sr.verticalNormalizedPosition -= 0.001f;
		else
			sr.verticalNormalizedPosition = 1f;
		muteaudio = t1.isOn;
		AudioListener.pause = muteaudio;
	}
	public void EnableIntro(){
		for (int i=0; i<hide.Length; i++)
			hide [i].SetActive (false);
		for (int j=0; j<unhide.Length; j++)
			unhide [j].SetActive (true);
		level.SetActive (false);
	}
	public void DisableIntro(){
		for (int i=0; i<hide.Length; i++)
			hide [i].SetActive (true);
		for (int j=0; j<unhide.Length; j++)
			unhide [j].SetActive (false);
	}
	public void Play(){
		for (int i=0; i<hide.Length; i++)
			hide [i].SetActive (false);
		level.SetActive (true);
	}
	public void Level02(){
		load.SetActive (true);
		Application.LoadLevel ("Cemetary_02");
	}
	public void Level01(){
		for (int i=0; i<hide.Length; i++)
			hide [i].SetActive (false);
		for (int j=0; j<unhide.Length; j++)
			unhide [j].SetActive (false);
		load.SetActive (true);
		Application.LoadLevel ("Cemetary_01");
	}
	public void clickExit(){
		//for (int i=0; i<exit.Length; i++)
		//	exit[i].SetActive (true);
		Application.Quit ();
	}
	public void clickYes(){
		Application.Quit ();
	}
	public void clickNo(){
		for (int i=0; i<exit.Length; i++)
			exit[i].SetActive (false);
	}
	public void clickSetting(){
		for (int i=0; i<setting.Length; i++)
			setting [i].SetActive (true);
		controls.SetActive (false);
	}
	public void clickControls(){
	if(controls.activeSelf)
			controls.SetActive (false);
		else
			controls.SetActive (true);
	}
	public void DisableSetting(){
		for (int i=0; i<setting.Length; i++)
			setting [i].SetActive (false);
	}
}
