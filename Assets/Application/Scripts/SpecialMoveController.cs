using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//必殺技の演出のためのカメラをコントコールしたりする
public class SpecialMoveController : MonoBehaviour {
	//スカイボックス変えたりカメラを切り替えたり

	[SerializeField]
	QueryChanController queryChanController;
	[SerializeField]
	GameObject camera;
	public bool do_special_movie=false;
	private float special_movie_time;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (do_special_movie == true) SpecialMovie ();
	}


	void SpecialMovie(){
		special_movie_time += Time.deltaTime;
		camera.SetActive (true);
		if (special_movie_time < 5) {
			//カメラの操作
		} else {
			do_special_movie = false;
			camera.SetActive (false);
			queryChanController.special_movie_finish = true;
		}
	}
}
