using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class AIRespawn : MonoBehaviour
{
    
    private Transform currentRespawnPoint;
    //[HideInInspector]
    public int currentRespawnPointInt;
    public List<WheelCollider> myWcs;
    private bool isStartingRespawn=false;     
    private AIDriver aiDriverScript;
    private List<Transform> waypoints;
    //private Vector3 lastPosition;
    public float timeTillRespawn = 5;
    public float lastTimeToReachNextWP;

    //Event 2
    public delegate void RespawnHandler(AIEventArgs e);
    public static RespawnHandler onRespawnWaypoint; 

    void Awake()
    {        
        
        
    }
	
    void Start () {
        //wir machen dies in der Start-Routine, weil wir im AIDriver-Skript erst in der Awake-Fkt die Waypoints zuweisen
        //Um sicherzugehen, dass die Waypoints auch gefunden und der List zugewiesen wurde, weisen wir dies deshalb erst hier zu!
        
        aiDriverScript = gameObject.GetComponent("AIDriver") as AIDriver;
        waypoints = aiDriverScript.waypoints;
	}

    public void StartRespawn()
    {
        if (!isStartingRespawn)
        {
            isStartingRespawn = true;           
            Respawn();
        }
    }

	void Update () {       
        if (!IsCorrectMoving())
        {
            StartRespawn();
        }
	}

                
    void Respawn()
    { 
         
        int currentWaypoint = aiDriverScript.currentWaypoint;
        if (currentWaypoint == 0)
        {
            currentWaypoint  = waypoints.Count -1;
        }
        else
        {
            currentWaypoint -= 1;
        }
        currentRespawnPoint = waypoints[currentWaypoint];
        transform.position = currentRespawnPoint.position;
        transform.rotation = currentRespawnPoint.rotation;

        aiDriverScript.aiSteerAngle = 0;
        aiDriverScript.currentAngle = 0;        
        
        isStartingRespawn = false;
        lastTimeToReachNextWP = 0;

        //fire event BEGIN
        if (onRespawnWaypoint != null)
        {
            AIEventArgs e = new AIEventArgs();
            e.name = gameObject.name;
            e.currentWaypointIndex = currentWaypoint;
            e.currentWaypointName = waypoints[currentWaypoint].name;
            e.position = gameObject.transform.position;
            e.rotation = gameObject.transform.rotation;
            e.tag = gameObject.tag;
            onRespawnWaypoint(e);
        }
        //fire event END
    }
  

    bool IsCorrectMoving()
    {
        bool moving = true;
        lastTimeToReachNextWP += Time.deltaTime;
       
        if (lastTimeToReachNextWP >= timeTillRespawn)
        {
           
            moving = false;          

        }
        //lastPosition = transform.position;
        return moving;
    }

    
   
}
