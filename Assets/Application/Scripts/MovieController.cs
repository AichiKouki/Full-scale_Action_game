using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MovieController : MonoBehaviour {
	[SerializeField]
	GameObject[] movie_camera;

	//フェードアウト処理
	[SerializeField]
	Image fadeOutImage;
	private float fadeOutValue;
	private bool is_fadeOut = false;

	// Use this for initialization
	void Start () {
		StartCoroutine ("Camera_Switching");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(is_fadeOut==true)FadeOut ();
	}

	//二回カットして、カメラを切り替える
	IEnumerator Camera_Switching(){
		for(int i=0;i<3;i++){
			yield return new WaitForSeconds (4);
			if (!(i == 2)) {
				movie_camera [i].SetActive (false);
				movie_camera [i + 1].SetActive (true);
			} else {
				movie_camera [i].SetActive (true);
				is_fadeOut = true;
			}
		}
	}

	void FadeOut(){
		fadeOutValue+= 3f;
		if (fadeOutValue < 255) {
			fadeOutImage.color = new Color (0/255,0/255,0/255,fadeOutValue/255);
		} else {
			Debug.Log ("フェードアウト完了");
			SceneManager.LoadScene ("Stage1");
		}
	}
}
