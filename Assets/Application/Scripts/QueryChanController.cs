using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QueryChanController : MonoBehaviour {
	//アニメーション関連
	public Animator animator;
	public bool startAttackHit=false;
	public bool endAttackHit = false;
	public bool endAttack = false;
	private int attackType = 1;
	public bool attackPermission=true;//連続でキーを押して攻撃しようとしてアニメーションがマシンガンみたいに切り替わらないように攻撃許可フラグの作成

	public AudioSource aud;
	public AudioClip[] se;

	public UnityChanControlScriptWithRgidBody unityChanControlScriptWithRgidBody;//必殺技発動時は移動とかできないようにしたいから


	//走る処理関連
	public bool isRun=true;

	//魔法陣
	[SerializeField]
	GameObject magicField;
	[SerializeField]
	GameObject summoning_magicField;//必殺技の時に召喚したような演出のため

	//通常攻撃
	public GameObject[] magicAttackObjPre;

	public GameObject magicAttackObjPos;
	public GameObject magicAttackObj;

	//必殺技関連
	[SerializeField]
	MagicAttackController magicAttackController;
	public bool xbox_controller_licensing=true;//xboxコントローラーを使用していいかのフラグ

	//3Dメニューの処理
	public PanelManager panelManager;//3Dメニューのスクリプトを利用して、Xboxコントローラーから使用する。
	public Button[] fourTypeButton;//Resumeボタン、NewGameボタン、Settingsボタン、Quitボタンを入れる

	public bool openPanel=false;//Xboxコントローラーだけで、3Dメニューの表示と非表示を行うので、開くたびに、フラグを変更する。

	//Xboxのコントローラーの操作をまとめたクラス
	[SerializeField]
	XboxController xboxController;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		aud = GetComponent<AudioSource> ();

		//振動対応かどうかに調べる処理
		if (SystemInfo.supportsVibration) print("振動対応");
		else print("振動非対応");

		panelManager.CloseCurrent ();//最初は3Dメニューを閉じる
	}

	// 以下、メイン処理.リジッドボディと絡めるので、FixedUpdate内で処理を行う.
	void FixedUpdate ()
	{
		//if(xbox_controller_licensing==true) Xbox_controller_process ();

	}//FixedUpdate

	void Update(){
		xboxController.Xbox_controller_process ();//Input関連は、FixedUpdateだと処理されないことがあるとアドバイスをいただいてこれに変更
	}

	void Xbox_controller_process(){
		/*
		//xBoxのコントローラー処理
		if (Input.GetKeyDown(KeyCode.JoystickButton16) && attackPermission==true) {
			Debug.Log("Aボタン");
			endAttack = false;
			//attackPermission = false;
			//animator.SetTrigger ("Attack1");
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
			magicAttackController.StartDeathblow ();
			xbox_controller_licensing=false;//xboxコントローラーを一時的に使えなくする
			animator.SetTrigger ("Deathblow");
			aud.PlayOneShot (se[0]);
		}

		if (Input.GetKeyDown(KeyCode.JoystickButton15)) {
			Debug.Log("Xboxボタンが押された");
			if(openPanel==false)panelManager.OnEnable ();//3Dメニューの表示
			else panelManager.CloseCurrent();
			openPanel = !openPanel;
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

		//PCと同じクリックの機能の実装(AかBかXかYボタンいずれかを押したら、クリックと同じ機能を持った)
		if(Input.GetKeyDown(KeyCode.JoystickButton16) || Input.GetKeyDown(KeyCode.JoystickButton17) || Input.GetKeyDown(KeyCode.JoystickButton18) || Input.GetKeyDown(KeyCode.JoystickButton19)){
			//現在選択してるボタンを調べて、そのあとはtargetの部分のボタンの変数を切り替える
			//if()

			ExecuteEvents.Execute
			(
				target      : fourTypeButton[0].gameObject,//resumeボタンを対象に処理
				eventData   : new PointerEventData( EventSystem.current ),
				functor     : ExecuteEvents.pointerClickHandler
			);
		}
		*/
	}

	//魔法陣表示処理
	IEnumerator Display_MagicField(){
		magicField.SetActive (true);
		yield return new WaitForSeconds (3);
		magicField.SetActive (false);
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
