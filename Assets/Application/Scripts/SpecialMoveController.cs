using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//必殺技の演出のためのカメラをコントコールしたりする
public class SpecialMoveController : MonoBehaviour {
	//スカイボックス変えたりカメラを切り替えたり

	[SerializeField]
	MagicAttackController magicAttackController;
	[SerializeField]
	GameObject[] camera;
	public bool do_special_movie=false;
	private float special_movie_time;
	[SerializeField]
	Material[] skybox;
	[SerializeField]
	GameObject directional_Light;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (do_special_movie == true) SpecialMovie ();
	}


	void SpecialMovie(){
		special_movie_time += Time.deltaTime;
		camera[0].SetActive (true);
		RenderSettings.skybox = skybox[1];// Skyboxを変更する
		directional_Light.SetActive (false);//ディレクショナルライトを非表示にしてキャラ以外をほとんど表示しない感じにする
		if (special_movie_time > 0 && special_movie_time < 2) {
			//カメラの操作
		} else if (special_movie_time >= 2 && special_movie_time < 4) {
			camera [0].SetActive (false);
			camera [1].SetActive (true);
		}else {
			do_special_movie = false;
			special_movie_time = 0;
			camera[0].SetActive (false);
			camera[1].SetActive (false);
			magicAttackController.special_movie_finish = true;
			RenderSettings.skybox = skybox[0];
			directional_Light.SetActive (true);//ディレクショナルライトを非表示にしてキャラ以外をほとんど表示しない感じにする
		}
	}
}
