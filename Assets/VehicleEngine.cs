using UnityEngine;
using System.Collections;

public class VehicleEngine : MonoBehaviour {
	
	Rigidbody m_rigidbody;
	private VehicleState m_head;

	void Awake(){

		m_rigidbody = this.rigidbody;
	}
	// Use this for initialization
	void Start () {
	
	}

	void OnEnable () {
		initState ();
	}
	
	// Update is called once per frame
	void Update () {

		m_head.OnUpdate ();
	}

	public void changeState (VehicleState p_newState)
	{
		m_head.OnExit ();
		m_head = p_newState;
		m_head.OnEnter ();
	}
	
	public void initState ()
	{
		m_head = new IdleVehicleState (this);
		m_head.OnEnter ();
	}
	
	// Events
	public void onCollide ()
	{
		m_head.OnCollide ();
	}
	
}

public class VehicleState {
	
	protected VehicleEngine m_body;
	
	public VehicleState (VehicleEngine p_body)
	{
		m_body = p_body;
	}

	public virtual void OnCollide () {}
	// Generic state events
	public virtual void OnEnter () {}
	public virtual void OnExit () {}
	public virtual void OnUpdate () {}
	
}

public class IdleVehicleState : VehicleState {
	
	public IdleVehicleState (VehicleEngine p_body) : base (p_body)
	{
		m_body = p_body;
	}
	
	public override void OnEnter ()
	{
		base.OnEnter ();
	}
	
	public override void OnExit ()
	{
		base.OnExit ();
	}
	
	public override void OnUpdate ()
	{
		base.OnUpdate ();
	}
}

public class MovingVehicleState : VehicleState {

	Vector3 front;
	Vector3 back;
	RaycastHit hit;
	float speed;
	Vector3 Direction;

	public MovingVehicleState (VehicleEngine p_body) : base (p_body)
	{
		m_body = p_body;
	}

	void Awake(){

		front = m_body.transform.TransformDirection(Vector3.forward);
		back = m_body.transform.TransformDirection(Vector3.back);

	}

	void FixedUpdate(){

		checkDistance ();
	}
	
	public override void OnEnter ()
	{
		base.OnEnter ();
	}
	
	public override void OnExit ()
	{
		base.OnExit ();
	}
	
	public override void OnUpdate ()
	{
		base.OnUpdate ();
		moveCar (speed, Direction);
	}

	void moveCar(float speed, Vector3 Direction){
		
		m_body.rigidbody.transform.Translate (Direction * speed * Time.deltaTime);

	}

	void checkDistance(){

		Debug.DrawRay(m_body.transform.position,front * 10, Color.green);
		
		if(Physics.Raycast(m_body.transform.position,front,out hit, 10))
		{
			inCollision(hit);		
		}
		
		/*if(Physics.Raycast(m_body.transform.position,back,out hit, 9))
		{
			inCollision(hit);
		}*/
	}

	void inCollision(RaycastHit hit)
	{
		if (hit.collider.gameObject.tag == "leftEdge") {
			
			//Debug.Log(leftCollider.name); test left collider declaration	
			//transform.position.x = transform.position.x - 78.3f;
			
			//ADD TO POOL
		}
	}
}
