using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMusic : MonoBehaviour {
	
	public AudioClip mainTheme;
	private AudioSource source;

	void Awake()
	{
		DontDestroyOnLoad(this.gameObject);
		source = GetComponent<AudioSource> ();
		source.Play();//OneShot (mainTheme, 1.0f);
	}

	void Update() 
	{
		
	}
}
