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
		
	// Private attributes
	private AudioClip collected;

	// Initialization
	void Start () {
		collected = new AudioClip ();
		collected = Resources.Load("Sound/2cuteBells") as AudioClip;
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void PlayParticle(){
		collectableParticle.Play ();
	}
}
