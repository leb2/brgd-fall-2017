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
		source.PlayOneShot (mainTheme, 1.0f);
	}

	void Update() 
	{
		//source.PlayOneShot (mainTheme, 1.0f);
	}
}
