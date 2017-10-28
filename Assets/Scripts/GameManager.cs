using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	private static GameManager _instance;
	public RectTransform pauseScreen;
	public RectTransform mainMenu;

	public static GameManager Instance 
	{
		get {
			//create logic to create instance
			if (_instance == null) {
				GameObject go = new GameObject ("GameManager");
				go.AddComponent<GameManager> ();
			}

			return _instance;
		}
	}

	public int Score { get; set; }
	public bool IsDead { get; set; }

	void Awake()
	{
		_instance = this;
	}

	void Start()
	{
		Score = 10;
		Time.timeScale = 1;
		pauseScreen.gameObject.SetActive(false);
	}

	void Update()
	{
		if (IsDead) {
			this.TogglePauseMenu ();
		}
	}

	public void TogglePauseMenu()
	{
		pauseScreen.gameObject.SetActive(true);
		Invoke("loadMainMenu", 1.5f);
	}

	public void loadMainMenu()
	{
		Application.LoadLevel ("MainMenu");
	}
}
