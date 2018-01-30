using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
//xboxのコントローラーで操作させための処理。macバージョンなので、wondowsとは異なる部分がある。
public class XboxController : MonoBehaviour {
	[SerializeField]
	QueryChanController queryChanController;//Input関連はQueryChanControllerで値をみる
	[SerializeField]
	MagicAttackController magicAttackController;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// 以下、メイン処理.リジッドボディと絡めるので、FixedUpdate内で処理を行う.
	void FixedUpdate ()
	{

	}//FixedUpdate

	public void Xbox_controller_process(){
		//xBoxのコントローラー処理
		if (Input.GetKeyDown(KeyCode.JoystickButton16) && queryChanController.attackPermission==true) {
			Debug.Log("Aボタン");
			queryChanController.endAttack = false;
			//attackPermission = false;
			//animator.SetTrigger ("Attack1");
			queryChanController.magicAttackObj = (GameObject)Instantiate (queryChanController.magicAttackObjPre[0],queryChanController.magicAttackObjPos.transform.position,Quaternion.identity);
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
			magicAttackController.StartDeathblow ();
			queryChanController.xbox_controller_licensing=false;//xboxコントローラーを一時的に使えなくする
			queryChanController.animator.SetTrigger ("Deathblow");
			queryChanController.aud.PlayOneShot (queryChanController.se[0]);
		}

		if (Input.GetKeyDown(KeyCode.JoystickButton15)) {
			Debug.Log("Xboxボタンが押された");
			if(queryChanController.openPanel==false) queryChanController.panelManager.OnEnable ();//3Dメニューの表示
			else queryChanController.panelManager.CloseCurrent();
			queryChanController.openPanel = !queryChanController.openPanel;
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
				queryChanController.unityChanControlScriptWithRgidBody.forwardSpeed = 10f;
				if (queryChanController.isRun == true) queryChanController.animator.SetTrigger ("Run");
				queryChanController.isRun = false;
			}
		}

		if (queryChanController.isRun==false && RightStickVertical == 0f) {
			queryChanController.isRun = true;
			queryChanController.unityChanControlScriptWithRgidBody.forwardSpeed = 7f;
			//isRun = true;
			queryChanController.animator.SetTrigger ("Idle");
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

		//PCと同じクリックの機能の実装(AかBかXかYボタンいずれかを押したら、クリックと同じ機能を持った)
		if(Input.GetKeyDown(KeyCode.JoystickButton16) || Input.GetKeyDown(KeyCode.JoystickButton17) || Input.GetKeyDown(KeyCode.JoystickButton18) || Input.GetKeyDown(KeyCode.JoystickButton19)){
			//現在選択してるボタンを調べて、そのあとはtargetの部分のボタンの変数を切り替える
			//if()

			ExecuteEvents.Execute
			(
				target      : queryChanController.fourTypeButton[0].gameObject,//resumeボタンを対象に処理
				eventData   : new PointerEventData( EventSystem.current ),
				functor     : ExecuteEvents.pointerClickHandler
			);
		}

	}
}
