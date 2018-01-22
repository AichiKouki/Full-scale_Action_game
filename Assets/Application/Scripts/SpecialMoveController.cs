using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//必殺技の演出のためのカメラをコントコールしたりする
public class SpecialMoveController : MonoBehaviour {
	//スカイボックス変えたりカメラを切り替えたり

	[SerializeField]
	MagicAttackController magicAttackController;//カメラの切り替えが終わったあとに、必殺技を実際に提出する処理のため
	[SerializeField]
	GameObject[] camera;//複数のカメラを切り替えるので、そのカメラが入る
	public bool do_special_movie=false;
	private float special_movie_time;//このカメラ演出の時間を測るための変数
	[SerializeField]
	Material[] skybox;
	[SerializeField]
	GameObject directional_Light;//カメラ演出中ではより世界を暗くしたいので、Directional Lightを非表示にしたりするため。
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (do_special_movie == true) SpecialMovie ();//MagicAttackControllerから呼ばれる。必殺技演出が終わった後にこのカメラ切り替え処理が開始される。
	}


	void SpecialMovie(){
		special_movie_time += Time.deltaTime;
		camera[0].SetActive (true);//カメラ切り替え処理が開始された瞬間に、カメラ演出用のカメラを表示する。
		RenderSettings.skybox = skybox[1];//カメラ演出を行なっている間は、暗めのSkyboxに変更する
		directional_Light.SetActive (false);//ディレクショナルライトを非表示にしてキャラ以外をほとんど表示しない感じにする
		if (special_movie_time > 0 && special_movie_time < 2) {
			//カメラの操作
		} else if (special_movie_time >= 2 && special_movie_time < 4) {//2〜4秒の間には、別のカメラに切り替える
			camera [0].SetActive (false);
			camera [1].SetActive (true);
		}else {//4秒数以降には、またカメラを切り替えて、MagicAttackControllerにそれを伝える
			do_special_movie = false;//カメラ演出開始のフラグを下ろす
			special_movie_time = 0;//複数回このカメラ演出処理をするので、演出が終わるたびに、時間の値を0に戻す
			camera[0].SetActive (false);
			camera[1].SetActive (false);
			magicAttackController.special_movie_finish = true;//このカメラ演出が終わったことを知らせる。これで、実際に必殺技を撃つ処理が開始される
			RenderSettings.skybox = skybox[0];//カメラ演出が終わったら、元のskyboxに戻す
			directional_Light.SetActive (true);//ディレクショナルライトを非表示にしてキャラ以外をほとんど表示しない感じにする
		}
	}
}
