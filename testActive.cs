using UnityEngine;
using System.Collections;

public class testActive : MonoBehaviour {

	// Use this for initialization
	void Start () {
		var otherObject = GameObject.Find ("sofa_2");
		otherObject.SetActive (false);
		otherObject.SetActive (true);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
