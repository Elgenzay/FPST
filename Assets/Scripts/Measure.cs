using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Measure : MonoBehaviour {

	public GameObject player;
	public GameObject chooseXyParent;
	public GameObject step1Parent;
	public GameObject step2Parent;
	public GameObject upImage;
	public GameObject downImage;
	public GameObject leftImage;
	public GameObject rightImage;
	public GameObject cursor;
	public GameObject finishText;
	public GameObject fovInfo;
	public GameObject degreeInfo;
	public Toggle leftToggle;
	public Toggle rightToggle;
	public Toggle upToggle;
	public Toggle downToggle;
	public Button measureDegreesButton;
	public InputField xMult;
	public InputField yMult;
	public InputField currentdotsInputField;
	public InputField totaldotsInputField;
	public Text instructions;

	private FPS_Controller fpsC;
	private short direction;
	private float totaldots;
	private float currentdots;

	void Start(){
		//fpsC = player.GetComponent<FPS_Controller> ();
		//not sure if moving this down breaks things unexpectedly but if things break unexpectedly try uncommenting this
	}

	void OnEnable(){
		fpsC = player.GetComponent<FPS_Controller> ();
		fovInfo.SetActive (false);
		degreeInfo.SetActive (false);
		chooseXyParent.SetActive (true);
		step1Parent.SetActive (false);
		step2Parent.SetActive (false);
		upImage.SetActive (false);
		downImage.SetActive (false);
		leftImage.SetActive (false);
		rightImage.SetActive (false);
		finishText.SetActive (false);
		direction = 2;
		leftToggle.isOn = false;
		rightToggle.isOn = true;
		upToggle.isOn = false;
		downToggle.isOn = false;
		measureDegreesButton.interactable = true;
		xMult.text = (fpsC.xSensitivity).ToString();
		yMult.text = (fpsC.ySensitivity).ToString();
	}

	public void BackButtonPressed(){
		this.gameObject.SetActive (false);
	}

	public void FovInfoButtonPressed(){
		if (fovInfo.activeSelf) {
			fovInfo.SetActive (false);
		} else {
			fovInfo.SetActive (true);
		}
	}

	public void DegreeInfoButtonPressed(){
		if (degreeInfo.activeSelf) {
			degreeInfo.SetActive (false);
		} else {
			degreeInfo.SetActive (true);
		}
	}

	public void LeftToggled(bool newval){
		if (newval) {
			direction = 1;
			rightToggle.isOn = false;
			upToggle.isOn = false;
			downToggle.isOn = false;
			measureDegreesButton.interactable = true;
		} else if (!rightToggle.isOn && !upToggle.isOn && !downToggle.isOn) {
			leftToggle.isOn = true;
		}
	}

	public void RightToggled(bool newval){
		if (newval) {
			direction = 2;
			leftToggle.isOn = false;
			upToggle.isOn = false;
			downToggle.isOn = false;
			measureDegreesButton.interactable = true;
		}else if (!leftToggle.isOn && !upToggle.isOn && !downToggle.isOn) {
			rightToggle.isOn = true;
		}
	}

	public void UpToggled(bool newval){
		if (newval) {
			direction = 3;
			leftToggle.isOn = false;
			rightToggle.isOn = false;
			downToggle.isOn = false;
			measureDegreesButton.interactable = false;
		} else if (!rightToggle.isOn && !leftToggle.isOn && !downToggle.isOn) {
			upToggle.isOn = true;
		}
	}

	public void DownToggled(bool newval){
		if (newval) {
			direction = 4;
			leftToggle.isOn = false;
			rightToggle.isOn = false;
			upToggle.isOn = false;
			measureDegreesButton.interactable = false;
		} else if (!rightToggle.isOn && !leftToggle.isOn && !upToggle.isOn) {
			downToggle.isOn = true;
		}
	}

	public void FovButton(){
		if (direction == 1 || direction == 2){
			totaldots = Mathf.Round(fpsC.hFov / (0.05f * fpsC.xSensitivity));
		} else if (direction == 3 || direction == 4){
			totaldots = Mathf.Round(fpsC.vFov / (0.05f * fpsC.ySensitivity));
		}
		chooseXyParent.SetActive (false);
		ActivateStep1 ();
	}

	public void DegreeButton(){
		if (direction == 1 || direction == 2){
			totaldots = Mathf.Round(360f / (0.05f * fpsC.xSensitivity));
		} else if (direction == 3 || direction == 4){
			totaldots = Mathf.Round(360f / (0.05f * fpsC.ySensitivity));
		}
		chooseXyParent.SetActive (false);
		ActivateStep1 ();
	}

	private void ActivateStep1(){
		step1Parent.SetActive (true);
		if (direction == 1) {
			leftImage.SetActive (true);
		} else if (direction == 2) {
			rightImage.SetActive (true);
		} else if (direction == 3) {
			upImage.SetActive (true);
		} else if (direction == 4) {
			downImage.SetActive (true);
		}
		Cursor.lockState = CursorLockMode.Locked;
	}

	void Update () {
		if (step2Parent.activeSelf) {
			///* STANDALONE
			if (direction == 2) {
				currentdots += Input.GetAxis ("Mouse X") * 20f;
			} else if (direction == 1) {
				currentdots += (Input.GetAxis ("Mouse X") * 20f) * -1f;
			} else if (direction == 3) {
				currentdots += Input.GetAxis ("Mouse Y") * 20f;
			} else if (direction == 4) {
				currentdots += (Input.GetAxis ("Mouse Y") * 20f) * -1f;
			}
			//*/
			/* WEBGL
			if (direction == 2) {
				currentdots += (Input.GetAxis ("Mouse X")/2) * 20f;
			} else if (direction == 1) {
				currentdots += ((Input.GetAxis ("Mouse X")/2) * 20f) * -1f;
			} else if (direction == 3) {
				currentdots += (Input.GetAxis ("Mouse Y")/2) * 20f;
			} else if (direction == 4) {
				currentdots += ((Input.GetAxis ("Mouse Y") * 20f)/2) * -1f;
			}
			*/
			cursor.transform.localPosition = new Vector3((450f * currentdots / totaldots),0f,0f);
			if (currentdots < -0.5f || currentdots > 0.5f) {
				currentdotsInputField.text = (Mathf.Round(currentdots)).ToString ();
			} else {
				currentdotsInputField.text = "0";
			}
			if (currentdots / totaldots < 1.005f && currentdots / totaldots > 0.995f) {
				if (!finishText.activeSelf) {
					finishText.SetActive (true);
				}
			} else if (finishText.activeSelf) {
				finishText.SetActive (false);
			}
			if (cursor.transform.localPosition.x < -40f || cursor.transform.localPosition.x > 515f) {
				step2Parent.SetActive (false);
				step1Parent.SetActive (true);
			}
		} else if (step1Parent.activeSelf && Input.GetKeyDown (KeyCode.Mouse0)) {
			step1Parent.SetActive (false);
			step2Parent.SetActive (true);
			currentdots = 0f;
			totaldotsInputField.text = totaldots.ToString ();
			if (direction == 1) {
				instructions.text = "Move your mouse left until the reticle reaches the center of the target. Try not to move the mouse up or down.";
			} else if (direction == 2) {
				instructions.text = "Move your mouse right until the reticle reaches the center of the target. Try not to move the mouse up or down.";
			} else if (direction == 3) {
				instructions.text = "Move your mouse up until the reticle reaches the center of the target. Try not to move the mouse left or right.";
			} else if (direction == 4) {
				instructions.text = "Move your mouse down until the reticle reaches the center of the target. Try not to move the mouse left or right.";
			}
		}
	}
}
