using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

	public Texture backgroundTexture;

	void OnGUI(){
		//Display our Background Texture 
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height),backgroundTexture);

		Time.timeScale = 0;

		//buttons
		if (GUI.Button (new Rect(Screen.width*0.10f, Screen.height * .30f, Screen.width * .15f, Screen.height * .1f), "Play game")) {
			Application.LoadLevel ("Scene");
			Time.timeScale = 1;
		};

		if (GUI.Button (new Rect(Screen.width*.74f, Screen.height * .30f, Screen.width * .15f, Screen.height * .1f), "Quit")) {
			Application.Quit();
		};
	}
}
