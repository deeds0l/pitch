using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour {

	public static ObjectPool m_instance;
	
	[SerializeField]
	private VehicleEngine m_car;

	GameObject m_carContainer;

	[SerializeField]
	private int m_carPreloadCount;

	private Queue<VehicleEngine> m_carQueue;

	void Awake ()
	{
		m_instance = this;
		m_carContainer = new GameObject("m_carContainer");
	}
	
	void Start ()
	{
		m_carQueue = new Queue<VehicleEngine>();
		for (int it = 0; it < m_carPreloadCount; it++) 
		{
			VehicleEngine car = Instantiate(m_car, transform.position, transform.rotation) as VehicleEngine;
			car.initState();
			m_carQueue.Enqueue(car);
			car.transform.parent = m_carContainer.transform;
		}

	}

	public VehicleEngine GetCar ()
	{
		if (m_carQueue.Count <= 0) {
			VehicleEngine extraCar = Instantiate(m_car, transform.position, transform.rotation) as VehicleEngine;
			extraCar.initState();
			m_carQueue.Enqueue(extraCar);
			extraCar.transform.parent = m_carContainer.transform;
		}
		
		VehicleEngine car = m_carQueue.Dequeue ();

		return car;
	}
	
	public void ReturnNutrient (VehicleEngine p_car)
	{
		p_car.gameObject.transform.position = transform.position;
		p_car.gameObject.transform.rotation = transform.rotation;
		
		m_carQueue.Enqueue (p_car);
	}

}
