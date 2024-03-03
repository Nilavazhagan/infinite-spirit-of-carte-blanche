using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Fade : MonoBehaviour {
	public GameObject panel;
	Color temp;
	void Start () {
		temp=panel.GetComponent<Image>().color;

	}
	void Update () {
		StartCoroutine (FadeTo (0.0f, 2.0f));
		StartCoroutine (LoadMenu ());
	}
	IEnumerator FadeTo(float aValue, float aTime)	{
		float alpha = temp.a;
		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime){
			Color newColor = new Color(0, 0, 0, Mathf.Lerp(alpha,aValue,t));
			temp = newColor;
			panel.GetComponent<Image>().color=temp;
			yield return null;
		}
	}
	IEnumerator LoadMenu(){
		yield return new WaitForSeconds (2.5f);
		Application.LoadLevel ("Main Menu");
	}
}