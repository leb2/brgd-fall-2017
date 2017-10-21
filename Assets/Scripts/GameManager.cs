using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	private static GameManager _instance;
	public RectTransform pauseScreen;

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
		//pauseScreen = GameObject.GetComponent<GameManager>;
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
			Debug.Log ("Dead!");
			this.TogglePauseMenu ();
		}
	}

	public void TogglePauseMenu()
	{
		Time.timeScale = 0;
		pauseScreen.gameObject.SetActive(true);
		Debug.Log ("Pause!");
	}
}
