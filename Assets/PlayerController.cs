using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	float speed = 5;
	float Verticaltranslation;
	float Horizontaltranslation;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		Verticaltranslation = Input.GetAxis("Vertical") * speed;
		Horizontaltranslation = Input.GetAxis("Horizontal") * speed;
		Verticaltranslation *= Time.deltaTime;
		Horizontaltranslation *= Time.deltaTime;
		rigidbody.transform.Translate(Horizontaltranslation, 0 , Verticaltranslation);
	}
}
