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

	public void Play(){
		galaxyParticle.Play ();
	}

	public void Stop(){
		galaxyParticle.Stop (true);
	}

	public bool IsAlive(){
		return galaxyParticle.IsAlive ();
	}

	public void SetPosition(Vector2 newPosition){
		galaxyParticle.transform.position = newPosition;
	}

	public void SetActive(bool active){
		this.gameObject.SetActive (active);
	}
} 