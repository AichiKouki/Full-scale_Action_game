using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemyController : MonoBehaviour {

	//剣だけを判定するため
	GameObject otherChild;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision other){//衝突した位置にエフェクトを生成する処理でCollisionじゃないとできないから
		otherChild = other.gameObject.transform.Find ("Character1_Reference").gameObject;
		if (otherChild.gameObject.tag == "sword") {
			Debug.Log ("剣に当たった");
		}
	}
}
