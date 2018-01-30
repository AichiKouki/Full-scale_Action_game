using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	//フェードアウト処理
	[SerializeField]
	Image fadeInImage;
	private float fadeInValue=255;
	private bool is_fadeIn = false;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (fadeInValue > 0) FadeIn ();
	}

	//フェードインしてゲーム開始
	void FadeIn(){
		fadeInValue-= 3f;
		if (fadeInValue > 0) {
			fadeInImage.color = new Color (0/255,0/255,0/255,fadeInValue/255);
		}
		//Debug.Log ("ふぇーどいんなう");
	}

}
