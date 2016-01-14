using UnityEngine;
using System.Collections;

public class FirstPersonController : MonoBehaviour {

	public float movementSpeed = 5.0F;
	public float mouseSensitivity = 5.0F;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//rotation
		float rotX = Input.GetAxis ("Mouse X") * mouseSensitivity;

		transform.Rotate(0,rotX,0);
		//movement
		float moveHorizontal = Input.GetAxis ("Horizontal") * movementSpeed;
		float moveVertical = Input.GetAxis ("Vertical") * movementSpeed;

		Vector3 speed = new Vector3 (moveHorizontal,0,moveVertical);

		speed = transform.rotation * speed;
		CharacterController cc = GetComponent<CharacterController>();
		cc.SimpleMove (speed);
	}
}
