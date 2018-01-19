using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovieZombieController : MonoBehaviour {

	//ゾンビたちがバラバラにアニメーションしてるようにする
	Animator animator;
	private float ran;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		StartCoroutine ("Make_animation_start_hours_at_random");
	}
	
	// Update is called once per frame
	void Update () {
		SimpleWalk ();
	}

	IEnumerator Make_animation_start_hours_at_random(){
		Debug.Log ("アニメーションバラバラ");
		animator.enabled = false;
		ran = Random.Range (0.1f,1);
		yield return new WaitForSeconds (ran);
		animator.enabled = true;
	}

	void SimpleWalk(){
		transform.Translate (0,0,1*Time.deltaTime*0.5f);
	}

}
