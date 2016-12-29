using UnityEngine;
using System.Collections;

public class Galaxy : MonoBehaviour {
	public ParticleSystem galaxyParticle;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public void Appear(){
		galaxyParticle.Play ();
	}

	public void Disappear(){
		galaxyParticle.Stop (true);
	}

	public bool IsAlive(){
		return galaxyParticle.IsAlive ();
	}

	public void SetPosition(Vector2 newPosition){
		this.transform.position = newPosition;
	}    
} 