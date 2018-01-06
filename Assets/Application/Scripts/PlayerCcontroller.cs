using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCcontroller : MonoBehaviour {

	[SerializeField]
	QueryLocomotionController queryLocomotionController;

	// Use this for initialization
	void Start () {
		
	}
	
	// 以下、メイン処理.リジッドボディと絡めるので、FixedUpdate内で処理を行う.
	void FixedUpdate ()
	{
		//xBoxのコントローラー処理
		if (Input.GetKeyDown(KeyCode.JoystickButton16)) {
			Debug.Log("Aボタン");
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

		if (RightStickVertical > 0.7f) {
			queryLocomotionController.dash = true;
			Debug.Log ("走れ");
		}

		//右スティックボタンの左右
		float LeftStickHorizontal = Input.GetAxis("LeftStickHorizontal");
		if (LeftStickHorizontal < 0.0f){
			Debug.Log ("右スティックの右");
		}else if(LeftStickHorizontal > 0.0f){
			Debug.Log ("右スティックの左");
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
}
