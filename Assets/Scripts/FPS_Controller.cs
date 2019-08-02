using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FPS_Controller : MonoBehaviour {

	public GameObject webDisclaimer;
	public GameObject hitMarker;
	public GameObject bulletHole;

	public KeyCode bind_forward;//1
	public KeyCode bind_backward;//2
	public KeyCode bind_right;//3
	public KeyCode bind_left;//4
	public KeyCode bind_jump;//5
	public KeyCode bind_primary;//6
	public KeyCode bind_secondary;//7
	public KeyCode bind_sprint;//8
	public KeyCode bind_interact;//9
	public KeyCode bind_pause;//10
	public KeyCode bind_crouch;//11

	public bool isPaused;

	public InputField forwardBind;
	public InputField backwardBind;
	public InputField rightBind;
	public InputField leftBind;
	public InputField jumpBind;
	public InputField primaryBind;
	public InputField secondaryBind;
	public InputField sprintBind;
	public InputField interactBind;
	public InputField pauseBind;
	public InputField crouchBind;

	public Toggle hitmarkersToggle;
	public Toggle invertToggle;
	public InputField reticleSizeInputField;
	public InputField horRecoilInputField;
	public InputField verRecoilInputField;
	public InputField fireRateInputField;
	public InputField curResWidthInputField;
	public InputField curResHeightInputField;
	public InputField customResWidthInputField;
	public InputField customResHeightInputField;
	public Dropdown resolutionsDropdown;
	public GameObject customResParent;
	public GameObject measureWindowParent;
	public GameObject cursorSensitivityWindowParent;

	public GameObject mainMenuParent;
	public GameObject keyBindsParent;
	public Text changeMenuButton;
	public GameObject pauseMenu;
	public GameObject reticle;
	public GameObject moreInfo;
	public GameObject pressAnyKeyObject;
	public Slider fovSlider;
	public Slider reticleSizeSlider;
	public Slider horRecoilSlider;
	public Slider verRecoilSlider;
	public Slider fireRateSlider;
	public InputField fovVerticalInputField;
	public InputField fovHorizontalInputField;
	public InputField sugHor;
	public InputField sugVer;
	public Camera _camera;
	public Rigidbody rb;
	public float xSensitivity;
	public float ySensitivity;
	public float vFov;
	public float hFov;
	public float walkSpeed;
	public float sprintSpeed;
	public float diagonalSpeed;
	public float jumpHeight;
	public float jumpCooldown;
	public float airControl;
	public bool canFreeSprint;
	public float carryDistance;
	public float carryStrength;
	public float throwStrength;
	public float carryDropDistance;
	public MouseSensitivity mouseSensitivity;

	private Canvas retCanvas;
	private Collider col;
	private Rigidbody carryObject_rb;
	private float jump_timeStamp;
	private float throw_timeStamp;
	private float fovUpdate_timestamp;
	private float fire_timeStamp;
	private float xRotate;
	private float crouchMult;
	private CapsuleCollider Capsule;
	private bool isGrounded;
	private bool isSprinting;
	private bool isDisablingPAK;
	private bool isInteracting;
	private bool isFullScreen;
	private bool isChangingResolution;
	private bool isUpdatingFov;
	private bool isThrowing;
	private bool isClicking;
	private bool invert;
	private int rebindKey;
	private int curResWidth;
	private int curResHeight;

	public void CloseDisclaimer(){
		webDisclaimer.SetActive (false);
	}

	public void WebLink(){
		Application.OpenURL ("http://elgenzay.net/fpst");
	}

	void OnApplicationQuit(){
		PlayerPrefs.SetFloat ("xSensitivity", xSensitivity);
		PlayerPrefs.SetFloat ("ySensitivity", ySensitivity);
		PlayerPrefs.SetFloat ("vFov", vFov);
		PlayerPrefs.SetFloat ("reticleSize", reticleSizeSlider.value);
		PlayerPrefs.SetFloat ("horRecoil", horRecoilSlider.value);
		PlayerPrefs.SetFloat ("verRecoil", verRecoilSlider.value);
		PlayerPrefs.SetFloat ("fireRate", fireRateSlider.value);
		if (hitmarkersToggle.isOn) {
			PlayerPrefs.SetInt ("hitMarkersToggled", 1);
		} else {
			PlayerPrefs.SetInt ("hitMarkersToggled", 0);
		}
		if (invertToggle.isOn) {
			PlayerPrefs.SetInt ("invertToggled", 1);
		} else {
			PlayerPrefs.SetInt ("invertToggled", 0);
		}
		PlayerPrefs.SetInt ("bindForward", (int)bind_forward);
		PlayerPrefs.SetInt ("bindBackward", (int)bind_backward);
		PlayerPrefs.SetInt ("bindLeft", (int)bind_left);
		PlayerPrefs.SetInt ("bindRight", (int)bind_right);
		PlayerPrefs.SetInt ("bindCrouch", (int)bind_crouch);
		PlayerPrefs.SetInt ("bindJump", (int)bind_jump);
		PlayerPrefs.SetInt ("bindSprint", (int)bind_sprint);
		PlayerPrefs.SetInt ("bindPrimary", (int)bind_primary);
		PlayerPrefs.SetInt ("bindInteract", (int)bind_interact);
		PlayerPrefs.SetInt ("bindPause", (int)bind_pause);
		PlayerPrefs.SetInt ("bindSecondary", (int)bind_secondary);

		//PlayerPrefs.DeleteAll ();
		PlayerPrefs.Save ();
	}

	void Start () {
		col = this.GetComponent<Collider> ();
		retCanvas = reticle.GetComponent<Canvas> ();
		if (!PlayerPrefs.HasKey ("xSensitivity")) {
			PlayerPrefs.SetFloat ("xSensitivity", 2f);
		}
		if (!PlayerPrefs.HasKey ("ySensitivity")) {
			PlayerPrefs.SetFloat ("ySensitivity", 2f);
		}
		if (PlayerPrefs.GetFloat ("xSensitivity") != PlayerPrefs.GetFloat ("ySensitivity")) {
			this.GetComponent<MouseSensitivity> ().xyToggle.isOn = true;
		} else {
			this.GetComponent<MouseSensitivity> ().xyToggle.isOn = false;
		}
		if (!PlayerPrefs.HasKey ("vFov")) {
			PlayerPrefs.SetFloat ("vFov", 60f);
		}
		if (!PlayerPrefs.HasKey ("reticleSize")) {
			PlayerPrefs.SetFloat ("reticleSize", 3f);
		}
		if (!PlayerPrefs.HasKey ("horRecoil")) {
			PlayerPrefs.SetFloat ("horRecoil", 0f);
		}
		if (!PlayerPrefs.HasKey ("verRecoil")) {
			PlayerPrefs.SetFloat ("verRecoil", 0f);
		}
		if (!PlayerPrefs.HasKey ("fireRate")) {
			PlayerPrefs.SetFloat ("fireRate", 0f);
		}
		if (!PlayerPrefs.HasKey ("hitMarkersToggled")){
			PlayerPrefs.SetInt ("hitMarkersToggled", 0);
		}
		if (!PlayerPrefs.HasKey ("invertToggled")){
			PlayerPrefs.SetInt ("invertToggled", 0);
		}
		if (!PlayerPrefs.HasKey ("bindPrimary")) {
			PlayerPrefs.SetInt ("bindPrimary", (int)KeyCode.Mouse0);
		}
		if (!PlayerPrefs.HasKey ("bindForward")) {
			PlayerPrefs.SetInt ("bindForward", (int)KeyCode.W);
		}
		if (!PlayerPrefs.HasKey ("bindBackward")) {
			PlayerPrefs.SetInt ("bindBackward", (int)KeyCode.S);
		}
		if (!PlayerPrefs.HasKey ("bindLeft")) {
			PlayerPrefs.SetInt ("bindLeft", (int)KeyCode.A);
		}
		if (!PlayerPrefs.HasKey ("bindRight")) {
			PlayerPrefs.SetInt ("bindRight", (int)KeyCode.D);
		}
		if (!PlayerPrefs.HasKey ("bindCrouch")) {
			PlayerPrefs.SetInt ("bindCrouch", (int)KeyCode.LeftControl);
		}
		if (!PlayerPrefs.HasKey ("bindJump")) {
			PlayerPrefs.SetInt ("bindJump", (int)KeyCode.Space);
		}
		if (!PlayerPrefs.HasKey ("bindSprint")) {
			PlayerPrefs.SetInt ("bindSprint", (int)KeyCode.LeftShift);
		}
		if (!PlayerPrefs.HasKey ("bindInteract")) {
			PlayerPrefs.SetInt ("bindInteract", (int)KeyCode.E);
		}
		if (!PlayerPrefs.HasKey ("bindPause")) {
			PlayerPrefs.SetInt ("bindPause", (int)KeyCode.P);
		}
		if (!PlayerPrefs.HasKey ("bindSecondary")) {
			PlayerPrefs.SetInt ("bindSecondary", (int)KeyCode.Mouse1);
		}

		PlayerPrefs.Save ();
		xSensitivity = PlayerPrefs.GetFloat ("xSensitivity");
		ySensitivity = PlayerPrefs.GetFloat ("ySensitivity");
		fovVerticalInputField.text = vFov.ToString ();
		reticleSizeSlider.value = PlayerPrefs.GetFloat ("reticleSize");
		horRecoilSlider.value = PlayerPrefs.GetFloat ("horRecoil");
		verRecoilSlider.value = PlayerPrefs.GetFloat ("verRecoil");
		fireRateSlider.value = PlayerPrefs.GetFloat ("fireRate");
		if (PlayerPrefs.GetInt ("hitMarkersToggled") == 1) {
			hitmarkersToggle.isOn = true;
		} else {
			hitmarkersToggle.isOn = false;
		}
		if (PlayerPrefs.GetInt ("invertToggled") == 1) {
			invertToggle.isOn = true;
			invert = true;
		} else {
			invertToggle.isOn = false;
			invert = false;
		}
		vFov = PlayerPrefs.GetFloat ("vFov");

		bind_forward = (KeyCode)PlayerPrefs.GetInt("bindForward");
		bind_backward = (KeyCode)PlayerPrefs.GetInt("bindBackward");
		bind_left = (KeyCode)PlayerPrefs.GetInt("bindLeft");
		bind_right = (KeyCode)PlayerPrefs.GetInt("bindRight");
		bind_crouch = (KeyCode)PlayerPrefs.GetInt("bindCrouch");
		bind_jump = (KeyCode)PlayerPrefs.GetInt("bindJump");
		bind_sprint = (KeyCode)PlayerPrefs.GetInt("bindSprint");
		bind_primary = (KeyCode)PlayerPrefs.GetInt("bindPrimary");
		bind_interact = (KeyCode)PlayerPrefs.GetInt("bindInteract");
		bind_pause = (KeyCode)PlayerPrefs.GetInt("bindPause");
		bind_secondary = (KeyCode)PlayerPrefs.GetInt("bindSecondary");

		RefreshBindsMenu();

		Application.targetFrameRate = 60;
		QualitySettings.vSyncCount = 0;
		Cursor.lockState = CursorLockMode.Locked;
		isGrounded = false;
		isInteracting = false;
		isUpdatingFov = false;
		isThrowing = false;
		isClicking = false;
		crouchMult = 1f;
		jump_timeStamp = Time.time;
		throw_timeStamp = Time.time;
		fire_timeStamp = Time.time;
		fovUpdate_timestamp = Time.realtimeSinceStartup + 0.1f;
		Capsule = GetComponent<CapsuleCollider>();
		UpdateFov (vFov);
		this.gameObject.GetComponent<MouseSensitivity> ().xUpdateSensitivity (xSensitivity);
		this.gameObject.GetComponent<MouseSensitivity> ().yUpdateSensitivity (ySensitivity);
		isDisablingPAK = false;

		pause ();
	}

	public void Quit(){
		Application.Quit ();
	}

	public void MeasureButtonPressed(){
		measureWindowParent.SetActive (true);
	}

	public void FullScreenToggle (bool newval){
		Screen.fullScreen = newval;
		isFullScreen = newval;
	}

	public void HitPointsToggle(bool newval){
		hitmarkersToggle.isOn = newval;//redundant?
	}

	public void invertToggled(bool newval){
		invert = newval;
	}

	public void UpdateResolution(int width, int height){
		isChangingResolution = true;
		curResWidth = width;
		curResHeight = height;
		curResWidthInputField.text = width.ToString ();
		curResHeightInputField.text = height.ToString ();
		//WEBGL
		Screen.SetResolution (width, height, isFullScreen);
		//comment for web, uncomment for standalone
		fovUpdate_timestamp = Time.realtimeSinceStartup + 0.1f;
		isUpdatingFov = true;
		sugHor.text = ((20f * hFov) / Screen.width).ToString();
		sugVer.text = ((20f * vFov) / Screen.height).ToString();

		//mouseSensitivity.xUpdateSensitivity (xSensitivity);
		//mouseSensitivity.yUpdateSensitivity (ySensitivity);
	}

	public void ApplyCustomResolution(){
		if (int.Parse (customResWidthInputField.text) >= 100 && int.Parse (customResHeightInputField.text) >= 100) {
			UpdateResolution (int.Parse (customResWidthInputField.text),int.Parse (customResHeightInputField.text));
		}
	}

	public void ResDropdownChanged(int newval){
		switch (newval) {
		case 0:
			//WEBGL
			customResParent.SetActive (true);
			//comment for webgl
			break;
		case 1:
			UpdateResolution (800,400);
			break;
		case 2:
			UpdateResolution (1024, 768);
			break;
		case 3:
			UpdateResolution (1200, 900);
			break;
		case 4:
			UpdateResolution (1280, 720);
			break;
		case 5:
			UpdateResolution (1280, 1024);
			break;
		case 6:
			UpdateResolution (1440, 900);
			break;
		case 7:
			UpdateResolution (1600, 900);
			break;
		case 8:
			UpdateResolution (1680, 1050);
			break;
		case 9:
			UpdateResolution (1600, 1200);
			break;
		case 10:
			UpdateResolution (1920, 1080);
			break;
		case 11:
			UpdateResolution (1920, 1200);
			break;
		case 12:
			UpdateResolution (2560, 1440);
			break;
		case 13:
			UpdateResolution (2560, 1600);
			break;
		}
		if (newval != 0) {
			customResParent.SetActive (false);
		}
	}

	public void UpdateFov(float newval){
		if (isPaused) {
			_camera.fieldOfView = newval;
		}
		vFov = newval;
		if (fovSlider.value != newval) {
			fovSlider.value = newval;
		}
		if (!fovVerticalInputField.isFocused && float.Parse (fovVerticalInputField.text) != newval) {
			fovVerticalInputField.text = newval.ToString ();
		}
		if (!fovHorizontalInputField.isFocused && float.Parse (fovHorizontalInputField.text) != Mathf.Atan((Mathf.Tan((vFov * Mathf.Deg2Rad) *.5f)) * _camera.aspect) * 2f * Mathf.Deg2Rad) {
			hFov = Mathf.Atan ((Mathf.Tan ((vFov * Mathf.Deg2Rad) * .5f)) * _camera.aspect) * 2f * Mathf.Rad2Deg;
			fovHorizontalInputField.text = hFov.ToString();
		}
		sugHor.text = ((20f * hFov) / Screen.width).ToString();
		sugVer.text = ((20f * vFov) / Screen.height).ToString();

		mouseSensitivity.xUpdateSensitivity (xSensitivity);
		mouseSensitivity.yUpdateSensitivity (ySensitivity);
	}

	public void fovSliderChanged(float newval){
		UpdateFov (newval);
	}

	public void fovVerticalInputFieldChanged(string newval){
		if (newval != "" && newval != "-" && newval != "." && fovVerticalInputField.isFocused) {
			if (float.Parse (newval) > 0f) {
				UpdateFov (float.Parse (newval));
			}
		}
	}

	public void fovHorizontalInputFieldChanged(string newval){
		if (newval != "" && newval != "-" && newval != "." && fovHorizontalInputField.isFocused) {
			if (float.Parse (newval) > 0f) {
				hFov = float.Parse (newval);
				UpdateFov ((2f * Mathf.Atan((Mathf.Tan(hFov/(2f * Mathf.Rad2Deg)))/_camera.aspect))/Mathf.Deg2Rad);
			}
		}
	}

	public void OtherMenu(){
		if (mainMenuParent.activeSelf) {
			mainMenuParent.SetActive (false);
			keyBindsParent.SetActive (true);
			changeMenuButton.text = "Back";
		} else {
			mainMenuParent.SetActive (true);
			keyBindsParent.SetActive (false);
			changeMenuButton.text = "Settings";
		}
	}

	public void ReticleSizeSliderChanged(float newval){
		reticleSizeInputField.text = (newval - 1f).ToString();
	}

	public void HorizontalRecoilSliderChanged(float newval){
		horRecoilInputField.text = newval.ToString();
	}

	public void VerticalRecoilSliderChanged(float newval){
		verRecoilInputField.text = newval.ToString ();
	}

	public void FireRateSliderChanged(float newval){
		fireRateInputField.text = newval.ToString ();
	}

	public void CursorSensitivityInfo(){
		if (cursorSensitivityWindowParent.activeSelf) {
			cursorSensitivityWindowParent.SetActive (false);
		} else {
			cursorSensitivityWindowParent.SetActive (true);
		}
	}

	public void Rebind(int key){
		rebindKey = key;
		pressAnyKeyObject.SetActive (true);
	}

	private void ResetBinds(){
		bind_forward = KeyCode.W;
		bind_backward = KeyCode.S;
		bind_right = KeyCode.D;
		bind_left = KeyCode.A;
		bind_jump = KeyCode.Space;
		bind_primary = KeyCode.Mouse0;
		bind_secondary = KeyCode.Mouse1;
		bind_sprint = KeyCode.LeftShift;
		bind_interact = KeyCode.E;
		bind_pause = KeyCode.P;
		bind_crouch = KeyCode.LeftControl;
		reticleSizeSlider.value = 3f;
		horRecoilSlider.value = 0f;
		verRecoilSlider.value = 0f;
		fireRateSlider.value = 0f;
		hitmarkersToggle.isOn = false;

		RefreshBindsMenu ();
	}

	private void RefreshBindsMenu(){
		forwardBind.text = bind_forward.ToString();
		backwardBind.text = bind_backward.ToString();
		leftBind.text = bind_left.ToString();
		rightBind.text = bind_right.ToString();
		jumpBind.text = bind_jump.ToString();
		primaryBind.text = bind_primary.ToString();
		secondaryBind.text = bind_secondary.ToString();
		sprintBind.text = bind_sprint.ToString();
		interactBind.text = bind_interact.ToString();
		pauseBind.text = bind_pause.ToString();
		crouchBind.text = bind_crouch.ToString();
	}

	public void pause(){
		if (isPaused) {
			if (measureWindowParent.activeSelf){
				measureWindowParent.SetActive (false);
				Cursor.lockState = CursorLockMode.None;
			}else if (!isDisablingPAK) {
				Cursor.lockState = CursorLockMode.Locked;
				cursorSensitivityWindowParent.SetActive (false);
				mainMenuParent.SetActive (true);
				keyBindsParent.SetActive (false);
				changeMenuButton.text = "Settings";
				pauseMenu.SetActive (false);
				reticle.SetActive (true);
				isPaused = false;
				Time.timeScale = 1f;
			}
		} else {
			Cursor.lockState = CursorLockMode.None;
			Time.timeScale = 0f;
			reticle.SetActive (false);
			pauseMenu.SetActive (true);
			isPaused = true;
		}
	}

	void OnGUI(){
		if (pressAnyKeyObject.activeSelf && !isDisablingPAK){
			bool mouseHeld = false;
			Event e = Event.current;
			if (Input.GetKey(KeyCode.LeftShift)){
				e.keyCode = KeyCode.LeftShift;
			} else if (Input.GetKey(KeyCode.RightShift)){
				e.keyCode = KeyCode.RightShift;
			} else if (Input.GetKeyDown(KeyCode.Mouse0)){
				if (rebindKey != 10) {
					mouseHeld = true;
					e.keyCode = KeyCode.Mouse0;
				}
			} else if (Input.GetKeyDown(KeyCode.Mouse1)){
				mouseHeld = true;
				e.keyCode = KeyCode.Mouse1;
			} else if (Input.GetKeyDown(KeyCode.Mouse2)){
				mouseHeld = true;
				e.keyCode = KeyCode.Mouse2;
			} else if (Input.GetKeyDown(KeyCode.Mouse3)){
				mouseHeld = true;
				e.keyCode = KeyCode.Mouse3;
			} else if (Input.GetKeyDown(KeyCode.Mouse4)){
				mouseHeld = true;
				e.keyCode = KeyCode.Mouse4;
			} else if (Input.GetKeyDown(KeyCode.Mouse5)){
				mouseHeld = true;
				e.keyCode = KeyCode.Mouse5;
			} else if (Input.GetKeyDown(KeyCode.Mouse6)){
				mouseHeld = true;
				e.keyCode = KeyCode.Mouse6;
			}
			if (!Input.GetKey(KeyCode.Escape) && ( e.isKey || e.keyCode == KeyCode.LeftShift || e.keyCode == KeyCode.RightShift || mouseHeld)) {
				switch (rebindKey) {
				case 1:
					bind_forward = e.keyCode;
					forwardBind.text = e.keyCode.ToString();
					break;
				case 2:
					bind_backward = e.keyCode;
					backwardBind.text = e.keyCode.ToString();
					break;
				case 3:
					bind_right = e.keyCode;
					rightBind.text = e.keyCode.ToString();
					break;
				case 4:
					bind_left = e.keyCode;
					leftBind.text = e.keyCode.ToString();
					break;
				case 5:
					bind_jump = e.keyCode;
					jumpBind.text = e.keyCode.ToString();
					break;
				case 6:
					bind_primary = e.keyCode;
					primaryBind.text = e.keyCode.ToString();
					break;
				case 7:
					bind_secondary = e.keyCode;
					secondaryBind.text = e.keyCode.ToString();
					break;
				case 8:
					bind_sprint = e.keyCode;
					sprintBind.text = e.keyCode.ToString();
					break;
				case 9:
					bind_interact = e.keyCode;
					interactBind.text = e.keyCode.ToString();
					break;
				case 10:
					bind_pause = e.keyCode;
					pauseBind.text = e.keyCode.ToString();
					break;
				case 11:
					bind_crouch = e.keyCode;
					crouchBind.text = e.keyCode.ToString ();
					break;
				}
				isDisablingPAK = true;
			}
			else if (Input.GetKey(KeyCode.Escape)){
				isDisablingPAK = true;
			}
		}
	}

	private void PlayerMove(){
		Vector3 desiredMove = transform.position;
		if (Input.GetKey (bind_crouch)) {
			crouchMult = 0.5f;
			if (_camera.transform.localPosition.y > 0.5f) {
				_camera.transform.localPosition = new Vector3 (_camera.transform.localPosition.x,_camera.transform.localPosition.y - 0.05f,_camera.transform.localPosition.z);
			}
		} else {
			crouchMult = 1f;
			if (_camera.transform.localPosition.y < 0.75f) {
				_camera.transform.localPosition = new Vector3 (_camera.transform.localPosition.x,_camera.transform.localPosition.y + 0.05f,_camera.transform.localPosition.z);
			}
		}

		if (Input.GetKey(bind_forward)) {
			if (Input.GetKey(bind_left) ^ Input.GetKey(bind_right)) {
				if (Input.GetKey(bind_sprint) && !Input.GetKey (bind_crouch)) {
					isSprinting = true;
					desiredMove = desiredMove + (transform.forward * sprintSpeed * diagonalSpeed * crouchMult);
				} else {
					desiredMove = desiredMove + (transform.forward * walkSpeed * diagonalSpeed * crouchMult);
				}
			} else {
				if (Input.GetKey(bind_sprint) && !Input.GetKey (bind_crouch)) {
					isSprinting = true;
					desiredMove = desiredMove + (transform.forward * sprintSpeed * crouchMult);
				} else {
					desiredMove = desiredMove + (transform.forward * walkSpeed * crouchMult);
				}
			}
		}
		if (Input.GetKey(bind_left)) {
			if (Input.GetKey(bind_forward) ^ Input.GetKey(bind_backward)) {
				if (Input.GetKey(bind_sprint) && canFreeSprint && !Input.GetKey (bind_crouch)) {
					isSprinting = true;
					desiredMove = desiredMove + (-transform.right * (sprintSpeed * 0.8f * diagonalSpeed * crouchMult));
				} else {
					desiredMove = desiredMove + (-transform.right * (walkSpeed * 0.8f * diagonalSpeed * crouchMult));
				}
			} else {
				if (Input.GetKey(bind_sprint) && canFreeSprint && !Input.GetKey (bind_crouch)) {
					isSprinting = true;
					desiredMove = desiredMove + (-transform.right * sprintSpeed * 0.8f * crouchMult);
				} else {
					desiredMove = desiredMove + (-transform.right * walkSpeed * 0.8f * crouchMult);
				}
			}
		}
		if (Input.GetKey(bind_backward)) {
			if (Input.GetKey(bind_left) ^ Input.GetKey(bind_right)) {
				if (Input.GetKey(bind_sprint) && canFreeSprint && !Input.GetKey (bind_crouch)) {
					isSprinting = true;
					desiredMove = desiredMove + (-transform.forward * sprintSpeed * 0.8f * diagonalSpeed * crouchMult);
				} else {
					desiredMove = desiredMove + (-transform.forward * walkSpeed * 0.8f * diagonalSpeed * crouchMult);
				}
			} else {
				if (Input.GetKey(bind_sprint) && canFreeSprint && !Input.GetKey (bind_crouch)) {
					isSprinting = true;
					desiredMove = desiredMove + (-transform.forward * sprintSpeed * 0.8f * crouchMult);
				} else {
					desiredMove = desiredMove + (-transform.forward * walkSpeed * 0.8f * crouchMult);
				}
			}
		}
		if (Input.GetKey(bind_right)) {
			if (Input.GetKey(bind_forward) ^ Input.GetKey(bind_backward)) {
				if (Input.GetKey(bind_sprint) && canFreeSprint && !Input.GetKey (bind_crouch)) {
					isSprinting = true;
					desiredMove = desiredMove + (transform.right * sprintSpeed * 0.8f * diagonalSpeed * crouchMult);
				} else {
					desiredMove = desiredMove + (transform.right * walkSpeed * 0.8f * diagonalSpeed * crouchMult);
				}
			} else {
				if (Input.GetKey(bind_sprint) && canFreeSprint && !Input.GetKey (bind_crouch)) {
					isSprinting = true;
					desiredMove = desiredMove + (transform.right * sprintSpeed * 0.8f * crouchMult);
				} else {
					desiredMove = desiredMove + (transform.right * walkSpeed * 0.8f * crouchMult);
				}
			}
		}

		RaycastHit hit;
		if (Physics.SphereCast (transform.position, Capsule.radius, Vector3.down, out hit,
			((Capsule.height / 2f) - Capsule.radius) + 0.01f)) {
			if (!isGrounded) { // landing
				isGrounded = true;
				rb.velocity = Vector3.zero;
			}
		} else if (isGrounded) { // jumping
			isGrounded = false;
			if (isSprinting) {
				rb.AddForce ((desiredMove - transform.position) * 200f * sprintSpeed, ForceMode.Impulse);
			} else {
				rb.AddForce ((desiredMove - transform.position) * 200f * walkSpeed, ForceMode.Impulse);
			}
		} else { // airborne
			if (Mathf.Abs (rb.velocity.x) < airControl && Mathf.Abs (rb.velocity.z) < airControl) {
				if (isSprinting && Mathf.Abs (rb.velocity.x) < airControl && Mathf.Abs (rb.velocity.z) < airControl) {
					rb.AddForce ((desiredMove - transform.position) * 1000f * sprintSpeed);
				} else if (Mathf.Abs (rb.velocity.x) < airControl * 0.75 && Mathf.Abs (rb.velocity.z) < airControl * 0.75f){
					rb.AddForce ((desiredMove - transform.position) * 1000f * walkSpeed);
				}
			}
		}
		if (Input.GetKey(bind_jump)) {
			if (isGrounded && jump_timeStamp <= Time.time) {
				jump_timeStamp = Time.time + jumpCooldown;
				rb.AddForce (Vector3.up * jumpHeight, ForceMode.Impulse);
			}
		}

		if (isGrounded) {
			rb.MovePosition (desiredMove);
			if (col.material.dynamicFriction == 0.1f) {
				col.material.dynamicFriction = 0.8f;
				col.material.staticFriction = 0.8f;
			}
		} else {
			if (col.material.dynamicFriction == 0.8f) {
				col.material.dynamicFriction = 0.1f;
				col.material.staticFriction = 0.1f;
			}
		}
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape) || Input.GetKeyDown (bind_pause)) {
			pause ();
		}
		if (isDisablingPAK){
			pressAnyKeyObject.SetActive (false);
			isDisablingPAK = false;
		}
		if (!isChangingResolution && (curResWidth != Screen.width || curResHeight != Screen.height)) {
			resolutionsDropdown.value = 0;
			UpdateResolution (Screen.width, Screen.height);
		}
		isChangingResolution = false;
		if (isUpdatingFov && fovUpdate_timestamp <= Time.realtimeSinceStartup) {
			UpdateFov (vFov);
			isUpdatingFov = false;
		}

		if (isPaused) {
			if (forwardBind.isFocused){
				forwardBind.gameObject.SetActive(false);
				Rebind (1);
				forwardBind.gameObject.SetActive(true);
			} else if (backwardBind.isFocused){
				backwardBind.gameObject.SetActive(false);
				Rebind (2);
				backwardBind.gameObject.SetActive(true);
			} else if (rightBind.isFocused){
				rightBind.gameObject.SetActive(false);
				Rebind (3);
				rightBind.gameObject.SetActive(true);
			} else if (leftBind.isFocused){
				leftBind.gameObject.SetActive(false);
				Rebind (4);
				leftBind.gameObject.SetActive(true);
			} else if (jumpBind.isFocused){
				jumpBind.gameObject.SetActive(false);
				Rebind (5);
				jumpBind.gameObject.SetActive(true);
			} else if (primaryBind.isFocused){
				primaryBind.gameObject.SetActive(false);
				Rebind (6);
				primaryBind.gameObject.SetActive(true);
			} else if (secondaryBind.isFocused){
				secondaryBind.gameObject.SetActive(false);
				Rebind (7);
				secondaryBind.gameObject.SetActive(true);
			} else if (sprintBind.isFocused){
				sprintBind.gameObject.SetActive(false);
				Rebind (8);
				sprintBind.gameObject.SetActive(true);
			} else if (interactBind.isFocused){
				interactBind.gameObject.SetActive(false);
				Rebind (9);
				interactBind.gameObject.SetActive(true);
			} else if (pauseBind.isFocused){
				pauseBind.gameObject.SetActive(false);
				Rebind (10);
				pauseBind.gameObject.SetActive(true);
			} else if (crouchBind.isFocused){
				crouchBind.gameObject.SetActive(false);
				Rebind (11);
				crouchBind.gameObject.SetActive(true);
			}
		}
		else{
			///* STANDALONE
			float mouse_x = Input.GetAxis ("Mouse X") * xSensitivity;
			float mouse_y;
			if (invert) {
				mouse_y = -Input.GetAxis ("Mouse Y") * ySensitivity;
			} else {
				mouse_y = Input.GetAxis ("Mouse Y") * ySensitivity;
			}
			//*/
			/* WEBGL
			float mouse_x = (Input.GetAxis ("Mouse X")/2) * xSensitivity;
			float mouse_y;
			if (invert) {
				mouse_y = (-Input.GetAxis ("Mouse Y") / 2) * ySensitivity;
			} else {
				mouse_y = (Input.GetAxis ("Mouse Y") / 2) * ySensitivity;
			}
			*/
			xRotate = transform.localEulerAngles.x - mouse_y;
			if ((_camera.transform.localEulerAngles.x != 90f && mouse_y <= 0f) || (_camera.transform.localEulerAngles.x != 270f && mouse_y >= 0f)) {
				if ((_camera.transform.localEulerAngles.x - mouse_y) > 90f && (_camera.transform.localEulerAngles.x - mouse_y) < 180f) {
					_camera.transform.localEulerAngles = new Vector3 (90f, _camera.transform.localEulerAngles.y, _camera.transform.localEulerAngles.z);
				} else if ((_camera.transform.localEulerAngles.x - mouse_y) < 270f && (_camera.transform.localEulerAngles.x - mouse_y) >= 180f) {
					_camera.transform.localEulerAngles = new Vector3 (270f, _camera.transform.localEulerAngles.y, _camera.transform.localEulerAngles.z);
				} else {
					_camera.transform.Rotate (xRotate, 0f, 0f);
				}
			}
			this.transform.Rotate (0f, mouse_x, 0f);

			if (Input.GetKeyDown(bind_interact)){
				isInteracting = true;
			}
			if (Input.GetKeyDown(bind_primary)){
				isClicking = true;
			}
		}
	}

	void FixedUpdate(){
		if (Input.GetKey(bind_primary) && !isThrowing){
			//fire
			bool willFire = false;
			if (Input.GetKey (bind_primary) && fire_timeStamp < Time.time && fireRateSlider.value != 0f) {
				willFire = true;
			} else if (fireRateSlider.value == 0f){
				if (isClicking){
					willFire = true;
				}
			}
			isClicking = false;
			if (willFire) {
				RaycastHit hit;
				if (Physics.Raycast (_camera.transform.position, _camera.transform.forward, out hit, 300f, ~(1 << 9))) {
					GameObject instantiatedHitMarker = Instantiate (hitMarker);
					instantiatedHitMarker.transform.position = hit.point;
					instantiatedHitMarker.transform.rotation = Quaternion.LookRotation (-_camera.transform.forward);
					if (hitmarkersToggle.isOn){
						Instantiate (bulletHole).transform.position = hit.point;
					}
					if (hit.collider.gameObject.GetComponent<Hittable> () != null) {
						hit.collider.gameObject.GetComponent<Hittable> ().Hit (this.transform.position);
					}
					if (hit.collider.GetComponent<Rigidbody> () != null) {
						hit.collider.GetComponent<Rigidbody> ().AddForce (_camera.transform.forward * 100f);
					}
				}
				fire_timeStamp = Time.time + (0.2f - (fireRateSlider.value * fireRateSlider.value * 0.01f));


				//_camera.transform.Rotate (-(verRecoilSlider.value * verRecoilSlider.value * 0.05f), 0f, 0f);
				float newY = verRecoilSlider.value * verRecoilSlider.value * 0.075f;
				if ((_camera.transform.localEulerAngles.x != 90f && newY <= 0f) || (_camera.transform.localEulerAngles.x != 270f && newY >= 0f)) {
					if ((_camera.transform.localEulerAngles.x - newY) > 90f && (_camera.transform.localEulerAngles.x - newY) < 180f) {
						_camera.transform.localEulerAngles = new Vector3 (90f, _camera.transform.localEulerAngles.y, _camera.transform.localEulerAngles.z);
					} else if ((_camera.transform.localEulerAngles.x - newY) < 270f && (_camera.transform.localEulerAngles.x - newY) >= 180f) {
						_camera.transform.localEulerAngles = new Vector3 (270f, _camera.transform.localEulerAngles.y, _camera.transform.localEulerAngles.z);
					} else {
						_camera.transform.Rotate (-newY, 0f, 0f);
					}
				}

				this.transform.Rotate (0f, Random.Range(-(horRecoilSlider.value * horRecoilSlider.value * 0.075f),horRecoilSlider.value * horRecoilSlider.value * 0.075f), 0f);

			}
		}

		if (retCanvas.scaleFactor != reticleSizeSlider.value) {
			retCanvas.scaleFactor = reticleSizeSlider.value;
		}
		isThrowing = false;
		if (isSprinting) {
			if (_camera.fieldOfView < vFov + 5) {
				_camera.fieldOfView += 1f;
			}
			if (_camera.fieldOfView > vFov + 5) {
				_camera.fieldOfView = vFov + 5;
			}
		} else if (_camera.fieldOfView > vFov) {
			_camera.fieldOfView -= 1f;
			if (_camera.fieldOfView < vFov) {
				_camera.fieldOfView = vFov;
			}
		}
		isSprinting = false;
		PlayerMove ();
		//throw
		if (Input.GetKey (bind_primary) && carryObject_rb != null) {
			throw_timeStamp = Time.time + 0.5f;
			carryObject_rb.velocity = _camera.transform.forward * carryStrength * throwStrength;
			carryObject_rb.angularDrag = carryObject_rb.GetComponent<Carriable> ().initialAngularDrag;
			carryObject_rb = null;
			isThrowing = true;
		}
		if (isInteracting){
			isInteracting = false;
			//drop by intent
			if (carryObject_rb != null) {
				throw_timeStamp = Time.time + 0.5f;
				carryObject_rb.angularDrag = carryObject_rb.GetComponent<Carriable> ().initialAngularDrag;
				carryObject_rb = null;
			}
			//pick up
			if (carryObject_rb == null) {
				RaycastHit rayhit;
				if (throw_timeStamp <= Time.time && Physics.Raycast (_camera.transform.position, _camera.transform.forward, out rayhit, carryDistance + carryDropDistance, ~(1 << 8))) {
					if (rayhit.collider.gameObject.GetComponent<Carriable> () != null) {
						carryObject_rb = rayhit.rigidbody;
						carryObject_rb.angularDrag = 5f;
					} else if (rayhit.collider.gameObject.GetComponent<Interactable>() != null) {
						//interact
						rayhit.collider.gameObject.GetComponent<Interactable>().Interact();
					}
				}
			}
		}
		//carrying
		if (carryObject_rb != null) {
			carryObject_rb.velocity = ((((_camera.transform.forward * carryDistance) + _camera.transform.position) - carryObject_rb.position) * carryStrength);
		}
		//drop by carrydistance
		if (carryObject_rb != null && (((_camera.transform.forward * carryDistance) + _camera.transform.position) - carryObject_rb.position).sqrMagnitude > carryDropDistance * carryDropDistance) {
			carryObject_rb.angularDrag = carryObject_rb.GetComponent<Carriable> ().initialAngularDrag;
			carryObject_rb = null;
		}

	}
}
