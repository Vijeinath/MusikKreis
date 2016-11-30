using UnityEngine;
using System.Collections;

public class PlanetEarth : MonoBehaviour {
	public float RotationSpeed = 30f;
	private float degree = 0f;
	private float degreeDelta = 0.5f;
	private float radius = 2.5f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(0, 0, Time.deltaTime*RotationSpeed);
		Orbit ();

	}

	void Orbit(){
		this.transform.position = new Vector2 (Mathf.Cos (degree * Mathf.PI / 180)*radius,Mathf.Sin (degree * Mathf.PI / 180)*radius);
		degree = (degree + degreeDelta) %360;
	}
}
