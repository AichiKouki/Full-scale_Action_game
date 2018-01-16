using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovieController : MonoBehaviour {
	[SerializeField]
	GameObject[] movie_camera;

	// Use this for initialization
	void Start () {
		StartCoroutine ("Camera_Switching");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator Camera_Switching(){
		for(int i=0;i<3;i++){
			yield return new WaitForSeconds (3);
			if (!(i == 2)) {
				movie_camera [i].SetActive (false);
				movie_camera [i + 1].SetActive (true);
			} else {
				movie_camera [i].SetActive (true);
			}
		}
	}
}
