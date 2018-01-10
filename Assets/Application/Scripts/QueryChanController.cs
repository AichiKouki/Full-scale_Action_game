﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueryChanController : MonoBehaviour {
	//アニメーション関連
	Animator animator;
	public bool startAttackHit=false;
	public bool endAttackHit = false;
	public bool endAttack = false;
	private int attackType = 1;
	private bool attackPermission=true;//連続でキーを押して攻撃しようとしてアニメーションがマシンガンみたいに切り替わらないように攻撃許可フラグの作成

	AudioSource aud;
	[SerializeField]
	AudioClip[] se;

	//走る処理関連
	private bool isRun=true;

	//魔法陣
	[SerializeField]
	GameObject magicField;
	[SerializeField]
	GameObject summoning_magicField;//必殺技の時に召喚したような演出のため

	//通常攻撃
	[SerializeField]
	GameObject[] magicAttackObjPre;
	[SerializeField]
	GameObject magicAttackObjPos;
	GameObject magicAttackObj;

	//必殺技関連
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
		animator = GetComponent<Animator> ();
		aud = GetComponent<AudioSource> ();
		ghost_scale = new Vector3 (1,1,1);//ゴーストのサイズを拡大する処理の時に、newを繰り返さないため
	}

	// 以下、メイン処理.リジッドボディと絡めるので、FixedUpdate内で処理を行う.
	void FixedUpdate ()
	{
		if(xbox_controller_licensing==true) Xbox_controller_process ();

		if (do_Deathblow == true) Deathblow ();

	}//FixedUpdate

	void Xbox_controller_process(){
		//xBoxのコントローラー処理
		if (Input.GetKeyDown(KeyCode.JoystickButton16) && attackPermission==true) {
			Debug.Log("Aボタン");
			endAttack = false;
			attackPermission = false;
			animator.SetTrigger ("Attack1");
			magicAttackObj = (GameObject)Instantiate (magicAttackObjPre[0],magicAttackObjPos.transform.position,Quaternion.identity);
		}

		if (Input.GetKeyDown(KeyCode.JoystickButton17)) {
			Debug.Log("Bボタンが押された");
			StartCoroutine ("Display_MagicField");
		}

		if (Input.GetKeyDown(KeyCode.JoystickButton18)) {
			Debug.Log("Xボタンが押された");
		}

		if (Input.GetKeyDown(KeyCode.JoystickButton19)) {
			Debug.Log("Yボタンが押された");
			deathblow_Ghost.SetActive (true);
			do_Deathblow = true;//必殺技を開始したのでtrue
			xbox_controller_licensing=false;//xboxコントローラーを一時的に使えなくする
			specialMoveController.do_special_movie = true;//必殺技カメラ演出をスタートさせるフラグ
			unityChanControlScriptWithRgidBody.enabled = false;//Unityちゃんの移動スクリプトをオフにして移動できなくする。
			deathblow_Ghost.transform.parent = Deathblow_Ghost_parent_when_moving;//このままだと巨大ゴーストの移動中にQueryちゃんと同じ方向に動いてしまうから一時的に親を変更
			animator.SetTrigger ("Deathblow");
			aud.PlayOneShot (se[0]);
			summoning_magicField.SetActive (true);//召喚魔法陣を表示
		}

		if (Input.GetKeyDown(KeyCode.JoystickButton15)) {
			Debug.Log("Xboxボタンが押された");
		}

		if (Input.GetKeyDown(KeyCode.JoystickButton13)) {
			Debug.Log("LBボタンが押された");
		}

		if (Input.GetKeyDown(KeyCode.JoystickButton14)) {
			Debug.Log("RBボタンが押された");
		}

		if (Input.GetKey(KeyCode.JoystickButton5)) {
			Debug.Log("十字キーの上");
		}
		//十字キーの下
		if (Input.GetKey(KeyCode.JoystickButton6)) {
			Debug.Log("十字キーの下");
		}
		//十字キーの左
		if (Input.GetKey(KeyCode.JoystickButton7)) {
			Debug.Log("十字キーの左");
		}
		//十字キーの右
		if (Input.GetKey(KeyCode.JoystickButton8)) {
			Debug.Log("十字キーの右");
		}
		//スタートボタン
		if (Input.GetKey(KeyCode.JoystickButton9)) {
			Debug.Log("スタートボタン");
		}

		if (Input.GetKey(KeyCode.JoystickButton10)) {
			Debug.Log("バックボタン");
		}

		//左のスティックボタンの左右
		float RightStickHorizontal = Input.GetAxis("RightStickHorizontal");
		if (RightStickHorizontal < 0.0f){
			//Debug.Log ("左スティックの右");
		}else if(RightStickHorizontal > 0.0f){
			//Debug.Log ("左スティックの左");
		}

		//左スティックボタンの上下
		float RightStickVertical = Input.GetAxis("RightStickVertical");
		if (RightStickVertical < 0.0f){
			//Debug.Log ("左スティックの下");
		}else if(RightStickVertical > 0.0f){
			//Debug.Log ("左スティックの上");

			//走る処理
			if (RightStickVertical > 0.7f) {
				unityChanControlScriptWithRgidBody.forwardSpeed = 10f;
				if (isRun == true) animator.SetTrigger ("Run");
				isRun = false;
			}
		}

		if (isRun==false && RightStickVertical == 0f) {
			isRun = true;
			unityChanControlScriptWithRgidBody.forwardSpeed = 7f;
			//isRun = true;
			animator.SetTrigger ("Idle");
			//Debug.Log ("走るのやめた");
		}

		//右スティックボタンの左右
		float LeftStickHorizontal = Input.GetAxis("LeftStickHorizontal");
		if (LeftStickHorizontal < 0.0f){
			//Debug.Log ("右スティックの右");
		}else if(LeftStickHorizontal > 0.0f){
			//Debug.Log("右スティックの左");
		}

		//右スティックボタンの上下
		float LeftStickVertical = Input.GetAxis("LeftStickVertical");
		if (LeftStickVertical < 0.0f){
			//Debug.Log ("右スティックの下");
			if(LeftStickVertical<-0.7f && transform.position.y>2)transform.Translate (0,-6f*Time.deltaTime,0);
		}else if(LeftStickVertical > 0.0f){
			//Debug.Log ("右スティックの上");
			if(LeftStickVertical>0.7f)transform.Translate (0,6f*Time.deltaTime,0);
		}

		//トリガーに関しては、強さを調整できる。弱く押せば-0.1とかになるけど、完全に押したら-1になる
		//左トリガー
		float LeftTrigger = Input.GetAxis("LeftTrigger");//何もしなければ1。押したら、-1となる
		if (LeftTrigger == -1) {
			Debug.Log ("LTボタンが押された");
		}

		//右トリガー
		float RightTrigger = Input.GetAxis("RightTrigger");//何もしなければ1。押したら、-1となる
		if (RightTrigger == -1) {
			Debug.Log ("RTボタンが押された");
		}

	}

	//魔法陣表示処理
	IEnumerator Display_MagicField(){
		magicField.SetActive (true);
		yield return new WaitForSeconds (3);
		magicField.SetActive (false);
	}

	//必殺技
	void Deathblow(){
		//お化けを大きくして、敵に突進するような必殺技
		if(scale_value<=30)scale_value += Time.deltaTime*3;
		ghost_scale.x=scale_value;
		ghost_scale.y=scale_value;
		ghost_scale.z=scale_value;
		deathblow_Ghost.transform.localScale = ghost_scale;
		if (special_movie_finish==true) {//巨大ゴーストを突進させる処理
			//Debug.Log ("突進");
			if (once_process == false) {//処理が一度だけでいい部分
				once_process = true;//一度だけ処理のため
				unityChanControlScriptWithRgidBody.enabled = true;
				xbox_controller_licensing = true;
				summoning_magicField.SetActive (false);
			}
			deathblow_Ghost.transform.Translate (0,0,10*Time.deltaTime);
			deathblow_time+=Time.deltaTime;//時間によって処理を分けるため
			if (deathblow_time > 4) {
				Debug.Log ("終了");
				deathblow_time = 0;
				special_movie_finish = false;
				do_Deathblow = false;//必殺技を行ったのでfalseに戻す
				deathblow_Ghost.transform.localScale = new Vector3 (1,1,1);
				deathblow_Ghost.transform.parent = gameObject.transform;//巨大した時に親要素を変更したので親を元に戻す
				deathblow_Ghost.transform.localPosition = new Vector3 (0.2f,0,5);//親を戻してからローカル座標を元に戻す
				deathblow_Ghost.transform.localRotation = Quaternion.Euler(0,0,0);//親を戻してから向きを元に戻す
				scale_value = 1;
				deathblow_Ghost.SetActive (false);
				once_process = false;
			}
		}
	}

	//アニメーションイベント
	void StartAttackHit(){
		startAttackHit = true;
	}

	void EndAttackHit(){
		endAttack = true;
	}

	void EndAttack(){
		if (isRun == false)
			animator.SetTrigger ("Run");
		else
			animator.SetTrigger ("Idle");
		startAttackHit = false;
		endAttackHit = false;
		endAttack = true;
		attackPermission = true;
	}
	//アニメーションイベントはここまで

}
