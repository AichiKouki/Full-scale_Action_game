using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicAttackObjController : MonoBehaviour {

	GameObject enemy;
	[SerializeField]
	float speed=3;

	// Use this for initialization
	void Start () {
		enemy = GameObject.FindWithTag ("enemy");
		StartCoroutine ("AutoDestroyer");
	}
	
	// Update is called once per frame
	void Update () {
		Chase_enemy ();
	}

	//敵を追従する処理
	void Chase_enemy(){
		//ターゲットの方に向く処理
		transform.rotation=Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (enemy.transform.position - transform.position), 0.3f);//ターゲットの方に少しずつ向きが変わる

		transform.position += transform.forward *Time.deltaTime* speed;//前へ移動
	}

	void OnTriggerEnter(Collider other){
		Debug.Log ("ぶつかった");
		Destroy (gameObject);
	}

	IEnumerator AutoDestroyer(){
		yield return new WaitForSeconds (5);
		Destroy (gameObject);
	}
}
