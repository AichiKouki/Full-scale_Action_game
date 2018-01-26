using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicAttackController : MonoBehaviour {
	[SerializeField]
	QueryChanController queryChanController;

	//必殺技関連
	[SerializeField]
	GameObject summoning_magicField;//必殺技の時に召喚したような演出のため
	[SerializeField]
	GameObject magic_ring;
	[SerializeField]
	GameObject ghost;
	[SerializeField]
	GameObject deathblow_Ghost;
	[SerializeField]
	SpecialMoveController specialMoveController;
	[SerializeField]
	UnityChanControlScriptWithRgidBody unityChanControlScriptWithRgidBody;//必殺技発動時は移動とかできないようにしたいから
	[SerializeField]
	Transform Deathblow_Ghost_parent_when_moving;
	private float scale_value=1;//15まで上がる
	private bool do_Deathblow=false;
	Vector3 ghost_scale;
	private float deathblow_time;
	public bool special_movie_finish = false;//SpecialMoveControllerから呼ばれる。必殺技発動時の演出が終わったらtrueになる。
	private bool once_process=false;//一度だけ処理したい時のため
	private bool xbox_controller_licensing=true;//xboxコントローラーを使用していいかのフラグ
	// Use this for initialization
	void Start () {
		ghost_scale = new Vector3 (1,1,1);//ゴーストのサイズを拡大する処理の時に、newを繰り返さないため
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (do_Deathblow == true) Deathblow ();//XboxコントローラーのYボタンを押された瞬間にフラグが立って、ここが処理される
	}

	public void StartDeathblow(){
		deathblow_Ghost.SetActive (true);
		do_Deathblow = true;//必殺技を開始したのでtrue
		queryChanController.xbox_controller_licensing=false;//xboxコントローラーを一時的に使えなくする
		specialMoveController.do_special_movie = true;//必殺技カメラ演出をスタートさせるフラグ
		unityChanControlScriptWithRgidBody.enabled = false;//Unityちゃんの移動スクリプトをオフにして移動できなくする。
		deathblow_Ghost.transform.parent = Deathblow_Ghost_parent_when_moving;//このままだと巨大ゴーストの移動中にQueryちゃんと同じ方向に動いてしまうから一時的に親を変更
		summoning_magicField.SetActive (true);//必殺技発動時のキャラの周りのエフェクト
		magic_ring.SetActive(true);
	}

	//必殺技
	void Deathblow(){
		deathblow_time+=Time.deltaTime;//時間によって処理を分けるため
		//お化けを大きくして、敵に突進するような必殺技
		if(scale_value<=30)scale_value += Time.deltaTime*3;
		ghost_scale.x=scale_value;
		ghost_scale.y=scale_value;
		ghost_scale.z=scale_value;
		deathblow_Ghost.transform.localScale = ghost_scale;

		//必殺技カメラ演出が終わったら処理
		if (special_movie_finish==true) {//巨大ゴーストを突進させる処理
			//一度だけ処理したい部分
			if (once_process == false) {
				once_process = true;//一度だけ処理のため
				unityChanControlScriptWithRgidBody.enabled = true;//カメラ演出が終わったらキャラを移動できるようにする
				queryChanController.xbox_controller_licensing = true;//xbox関係の操作もできるようにする。
				summoning_magicField.SetActive (false);//必殺技を発動した瞬間のエフェクトを非表示にする。
				magic_ring.SetActive(false);
				deathblow_time = 0;//必殺技発動から、終了までの時間を0に戻す
			}

			//Debug.Log ("突進");
			deathblow_Ghost.transform.Translate (0,0,10*Time.deltaTime);
			if (deathblow_time > 4) {
				Debug.Log ("終了");
				deathblow_time = 0;
				special_movie_finish = false;
				do_Deathblow = false;//必殺技を行ったのでfalseに戻す
				deathblow_Ghost.transform.localScale = new Vector3 (1,1,1);
				deathblow_Ghost.transform.parent = gameObject.transform;//巨大した時に親要素を変更したので親を元に戻す
				deathblow_Ghost.transform.localPosition = new Vector3 (0.2f,0,5);//親を戻してからローカル座標を元に戻す
				deathblow_Ghost.transform.localRotation = Quaternion.Euler(0,0,0);//親を戻してから向きを元に戻す
				scale_value = 1;//必殺技によって巨大化したゴーストのサイズを1に戻す
				deathblow_Ghost.SetActive (false);//必殺技が終わったら、巨大化する方のゴーストは非表示にする。
				once_process = false;
			}
		}
	}

}
