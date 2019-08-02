using UnityEngine;
using System.Collections;

public class TargetMachine : MonoBehaviour {

	public GameObject machineTarget;

	public float screenSize;

	public short frequency;//1 2
	public short size;//3 4
	public short duration;//5 6
	public short height;//7 8

	public int score;
	public GameObject scoreText;

	public GameObject freqText;
	public GameObject sizeText;
	public GameObject durText;
	public GameObject heightText;

	public float trueHeight;
	public float trueSize;
	public float trueDuration;
	public float trueFrequency;

	private TextMesh scoreTextMesh;
	private float freqTimestamp;

	private float CalcTrueFreq(short val){
		return 1.1f - (val * 0.1f);
	}
	private float CalcTrueSize(short val){
		return val + val;
	}
	private float CalcTrueHeight(short val){
		return val * 10f;
	}
	private float CalcTrueDuration(short val){
		return val * 0.5f;
	}

	public void ModifyValue(short val){
		switch (val) {
		case 1:
			if (frequency < 9) {
				frequency++;
				trueFrequency = CalcTrueFreq (frequency);
			}
			freqText.GetComponent<TextMesh> ().text = frequency.ToString();
			break;
		case 2:
			if (frequency > 1) {
				frequency--;
				trueFrequency = CalcTrueFreq (frequency);
			}
			freqText.GetComponent<TextMesh> ().text = frequency.ToString();
			break;
		case 3:
			if (size < 9) {
				size++;
				trueSize = CalcTrueSize (size);
			}
			sizeText.GetComponent<TextMesh> ().text = size.ToString();

			break;
		case 4:
			if (size > 1) {
				size--;
				trueSize = CalcTrueSize (size);
			}
			sizeText.GetComponent<TextMesh> ().text = size.ToString();
			break;
		case 5:
			if (duration < 9) {
				duration++;
				trueDuration = CalcTrueDuration (duration);
			}
			durText.GetComponent<TextMesh> ().text = duration.ToString();
			break;
		case 6:
			if (duration > 1) {
				duration--;
				trueDuration = CalcTrueDuration (duration);
			}
			durText.GetComponent<TextMesh> ().text = duration.ToString();
			break;
		case 7:
			if (height < 9) {
				height++;
				trueHeight = CalcTrueHeight (height);
			}
			heightText.GetComponent<TextMesh> ().text = height.ToString ();
			break;
		case 8:
			if (height > 1) {
				height--;
				trueHeight = CalcTrueHeight (height);
			}
			heightText.GetComponent<TextMesh> ().text = height.ToString ();
			break;
		}
	}

	// Use this for initialization
	void Start () {
		scoreTextMesh = scoreText.GetComponent<TextMesh> ();
		scoreTextMesh.text = "";
		score = 0;
		freqText.GetComponent<TextMesh> ().text = frequency.ToString();
		sizeText.GetComponent<TextMesh> ().text = size.ToString();
		durText.GetComponent<TextMesh> ().text = duration.ToString();
		heightText.GetComponent<TextMesh> ().text = height.ToString();
		trueFrequency = CalcTrueFreq (frequency);
		trueSize = CalcTrueSize (size);
		trueDuration = CalcTrueDuration (duration);
		trueHeight = CalcTrueHeight (height);
		freqTimestamp = Time.time + trueFrequency;
	}

	void FixedUpdate () {
		if (freqTimestamp <= Time.time) {
			freqTimestamp = Time.time + trueFrequency;
			Instantiate (machineTarget).GetComponent<MachineTarget>().Spawn(this.gameObject);
		}
	}

	public void IncrementScore(){
		score += 1;
		if (score >= 3) {
			if (score < 1000) {
				scoreTextMesh.text = score.ToString ();
			} else {
				score = 1000;
				scoreTextMesh.text = "LOTS";
			}
		}
	}

	public void ResetScore(){
		score = 0;
		scoreTextMesh.text = "";
	}
}
