using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Pause_Menu : MonoBehaviour {

	public GameObject pause_panel;
	public GameObject quit;
	public GameObject setting1;
	public Toggle t1;
	bool muteaudio;
	public GameObject controls;
	void Start () {
		//pause_panel = GameObject.FindGameObjectsWithTag ("Pause");
		//quit = GameObject.FindGameObjectWithTag ("Quit");
		quit.SetActive (false);
		//setting1 = GameObject.FindGameObjectsWithTag ("Settings1");
		//for (int i=0; i<setting1.Length; i++)
			setting1 .SetActive (false);
	}
	void Update(){
		muteaudio = t1.isOn;
		AudioListener.pause = muteaudio;
	}
	public void OnResume(){
		//for (int i=0; i<pause_panel.Length; i++)
			pause_panel .SetActive (false);
		Time.timeScale = 1.0f;
	}
	public void OnQuit(){
		//quit.SetActive (true);
		Application.LoadLevel ("Main Menu");
	}
	public void OnQuitYes(){
		Application.LoadLevel ("Main Menu");
	}
	public void OnQuitNo(){
		quit.SetActive (false);
	}
	public void clickSetting(){
		//for (int i=0; i<setting1.Length; i++)
			setting1 .SetActive (true);
		controls.SetActive (false);
		//for (int i=0; i<pause_panel.Length; i++)
			pause_panel .SetActive (false);
	}
	public void clickControls(){
		if(controls.activeSelf)
			controls.SetActive (false);
		else
			controls.SetActive (true);
	}
	public void DisableSetting(){
		//for (int i=0; i<setting1.Length; i++)
			setting1 .SetActive (false);
		//for (int i=0; i<pause_panel.Length; i++)
			pause_panel .SetActive (true);
	}
}
