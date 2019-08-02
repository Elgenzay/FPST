using UnityEngine;
using System.Collections;

public class HitMarker : MonoBehaviour {

	private float deathtimer;

	// Use this for initialization
	void Start () {
		deathtimer = Time.time + 2f;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (deathtimer <= Time.time) {
			Destroy (this.gameObject);
		}
	}
}
