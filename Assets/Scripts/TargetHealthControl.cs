using UnityEngine;
using System.Collections;

public class TargetHealthControl : MonoBehaviour {

	public int targetHealth;
	public GameObject healthText;

	// Use this for initialization

	public void IncrementHealth(){
		if (targetHealth < 9){
			targetHealth++;
			healthText.GetComponent<TextMesh> ().text = targetHealth.ToString();
		}
	}

	public void DecrementHealth(){
		if (targetHealth > 0){
			targetHealth--;
			healthText.GetComponent<TextMesh> ().text = targetHealth.ToString();
		}
	}
	void Start(){
		healthText.GetComponent<TextMesh> ().text = targetHealth.ToString();
	}
}
