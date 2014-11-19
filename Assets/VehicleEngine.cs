using UnityEngine;
using System.Collections;

public class VehicleEngine : MonoBehaviour {
	
	Rigidbody m_rigidbody;
	float speed;
	private VehicleState m_head;

	void Awake(){

		m_rigidbody = this.rigidbody;
		speed = 5;
	}
	// Use this for initialization
	void Start () {
	
	}

	void OnEnable () {
		m_vehicle = this;
		initState ();
	}
	
	// Update is called once per frame
	void Update () {

		m_head.OnUpdate ();
	}

	void moveForward(float speed, Vector3 Direction){

		m_rigidbody.transform.Translate (Direction * speed * Time.deltaTime);
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
	
	// Generic state events
	public void OnEnter () {}
	public void OnExit () {}
	public void OnUpdate () {}
	
}

public class IdleVehicleState : VehicleState {
	
	public IdleVehicleState (VehicleState p_body) : base (p_body)
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
	
	public MovingVehicleState (VehicleState p_body) : base (p_body)
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

	void moveForward(float speed, Vector3 Direction){
		
		m_rigidbody.transform.Translate (Direction * speed * Time.deltaTime);
	}
}
