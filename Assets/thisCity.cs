using UnityEngine;
using System.Collections;
using DG.Tweening;

public class thisCity : MonoBehaviour {


	// Use this for initialization
	void Start () {
		//transform.DOMove(new Vector3(2,3,4), 1);
		rigidbody.DOMove(new Vector3(2,3,4), 1);
		//transform.DOMoveY (5, 1).From ()
		//.SetEase (Ease.InOutQuad);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
