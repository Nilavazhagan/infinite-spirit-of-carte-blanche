using UnityEngine;
using System.Collections;

public class CursorChange : MonoBehaviour {

	public Texture2D crosshair;

	void Start () {
		Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {
		Cursor.visible = false;
		if (Time.timeScale == 0f)
			Cursor.visible = true;
	}
	void OnGUI() {
		float xMin = Screen.width/2 - (crosshair.width / 20);
		float yMin = Screen.height/2  - (crosshair.height / 20);
		GUI.DrawTexture(new Rect(xMin, yMin, crosshair.width/10, crosshair.height/10), crosshair);
		}
}
