using UnityEngine;
using System.Collections;

public class CameraSwitch : MonoBehaviour {
	public Camera cam1;
	public Camera cam2;
	GameObject[] hide;
	int i;
	// Use this for initialization
	void Start () {
		cam1.enabled = true;
		cam2.enabled = false;
		hide = GameObject.FindGameObjectsWithTag ("Hide");
	}
		
	// Update is called once per frame
	void Update () {
	/*if (Input.GetKeyDown (KeyCode.Tab)) {
			cam1.enabled = !cam1.enabled;
			cam2.enabled = !cam2.enabled;
			}*/
	if (cam1.enabled) {
			for (i=0; i<hide.Length; i++)
				hide [i].SetActive (false);
		}
		if (cam2.enabled) {
			for (i=0; i<hide.Length; i++)
				hide [i].SetActive (true);
		}
	}
}
