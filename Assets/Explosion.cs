using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

	SimpleAnimator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent<SimpleAnimator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!animator.playing) Destroy (this.gameObject);
	}
}
