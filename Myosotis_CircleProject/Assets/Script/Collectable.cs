using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Collectable : MonoBehaviour {
	// Public attributes
	public ParticleSystem collectableParticle;
	public AudioClip Collected
	{
		get
		{	
			return collected;
		}
	}

	public AudioClip Created
	{
		get
		{
			return created;
		}
	}

	// Private attributes
	private AudioClip created;
	private AudioClip collected;

	// Initialization
	void Start () {
		created = new AudioClip ();
		collected = new AudioClip ();
		created = Resources.Load("Sound/create01") as AudioClip;
		collected = Resources.Load("Sound/2cuteBells") as AudioClip;
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void PlayParticle(){
		collectableParticle.Play ();
	}
}
