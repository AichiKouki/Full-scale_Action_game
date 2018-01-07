using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCcontroller : MonoBehaviour {

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

	[SerializeField]
	GameObject sword;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		aud = GetComponent<AudioSource> ();
	}
	
	// 以下、メイン処理.リジッドボディと絡めるので、FixedUpdate内で処理を行う.
	void FixedUpdate ()
	{
		//xBoxのコントローラー処理
		if (Input.GetKeyDown(KeyCode.JoystickButton16) && attackPermission==true) {
			Debug.Log("Aボタン");
			endAttack = false;
			attackPermission = false;
			if (attackType == 1) {
				animator.SetTrigger ("Attack1");
				aud.PlayOneShot (se [1]);
				attackType++;
			} else if (attackType == 2) {
				animator.SetTrigger ("Attack2");
				aud.PlayOneShot (se [1]);
				attackType++;
			} else if (attackType == 3) {
				animator.SetTrigger ("Attack3");
				aud.PlayOneShot (se [0]);
				attackType = 1;
			}
		}

		if (Input.GetKeyDown(KeyCode.JoystickButton17)) {
			Debug.Log("Bボタンが押された");
		}

		if (Input.GetKeyDown(KeyCode.JoystickButton18)) {
			Debug.Log("Xボタンが押された");
		}

		if (Input.GetKeyDown(KeyCode.JoystickButton19)) {
			Debug.Log("Yボタンが押された");
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
			Debug.Log ("左スティックの右");
		}else if(RightStickHorizontal > 0.0f){
			Debug.Log ("左スティックの左");
		}

		//左スティックボタンの上下
		float RightStickVertical = Input.GetAxis("RightStickVertical");
		if (RightStickVertical < 0.0f){
			Debug.Log ("左スティックの下");
		}else if(RightStickVertical > 0.0f){
			Debug.Log ("左スティックの上");
		}
		//右スティックボタンの左右
		float LeftStickHorizontal = Input.GetAxis("LeftStickHorizontal");
		if (LeftStickHorizontal < 0.0f){
			Debug.Log ("右スティックの右");
		}else if(LeftStickHorizontal > 0.0f){
            Debug.Log("右スティックの左");
		}

		//右スティックボタンの上下
		float LeftStickVertical = Input.GetAxis("LeftStickVertical");
		if (LeftStickVertical < 0.0f){
			Debug.Log ("右スティックの下");
		}else if(LeftStickVertical > 0.0f){
			Debug.Log ("右スティックの上");
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
	}//FixedUpdate

	//アニメーションイベント
	void StartAttackHit(){
		startAttackHit = true;
		sword.gameObject.tag = "sword";
	}

	void EndAttackHit(){
		endAttack = true;
	}

	void EndAttack(){
		animator.SetTrigger ("Idle");
		startAttackHit = false;
		endAttackHit = false;
		endAttack = true;
		attackPermission = true;

		sword.gameObject.tag = "Untagged";
	}
}
