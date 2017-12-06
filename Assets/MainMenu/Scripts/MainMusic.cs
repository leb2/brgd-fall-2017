using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMusic : MonoBehaviour {
	
	public AudioClip mainTheme;
	private AudioSource source;
//	private static bool _started = false;


	private void Start()
	{
//		DontDestroyOnLoad(this.gameObject);
		source = GetComponent<AudioSource> ();
//		if (!_started)
//		{
            source.Play();//OneShot (mainTheme, 1.0f);
//			_started = true;
//		} 
		
	}

	void Awake()
	{
//		DontDestroyOnLoad(this.gameObject);
//		source = GetComponent<AudioSource> ();
//		if (!_started)
//		{
//            source.Play();//OneShot (mainTheme, 1.0f);
//			_started = true;
//		} 
	}

	void Update() 
	{
		
	}
}
