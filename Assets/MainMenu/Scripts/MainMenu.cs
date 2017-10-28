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
		if (GUI.Button (new Rect(Screen.width*.25f, Screen.height * .35f, Screen.width * .5f, Screen.height * .1f), "Play game")) {
			Application.LoadLevel ("Scene");
			Time.timeScale = 1;
		};

		if (GUI.Button (new Rect(Screen.width*.25f, Screen.height * .5f, Screen.width * .5f, Screen.height * .1f), "Quit")) {
			Application.Quit();
		};
	}
}
