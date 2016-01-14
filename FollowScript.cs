using UnityEngine;
using System.Collections;

public class FollowScript : MonoBehaviour {
	public Transform toFollow;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = toFollow.position;
	}
}
