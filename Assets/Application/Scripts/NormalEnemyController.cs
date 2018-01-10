using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemyController : MonoBehaviour {

	//剣だけを判定するため
	GameObject otherChild;

	Animator animator;

	[SerializeField]
	GameObject hitPoint;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
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

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "magicAttackObj") {
			animator.SetTrigger ("Dead");
			hitPoint.gameObject.tag = "Untagged";
		}
	}
}
