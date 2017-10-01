using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundGuard : Guard {

    [SerializeField] float waitTime = 0.3f;
    [SerializeField] float speed = 5f;
    [SerializeField] float turnSpeed = 90;

    [SerializeField] Transform pathHolder;

    AIEstado estadoActual;
    AIEstadoRonda estado1;
    AIEstadoPersecucion estado2;

    Vector3[] waypoints;
    int targetWaypointIndex;
    Vector3 targetWaypoint;

    void OnDrawGizmos()
    {
        Vector3 startPosition = pathHolder.GetChild(0).position;
        Vector3 previousPosition = startPosition;
        foreach(Transform waypoint in pathHolder)
        {
            Gizmos.DrawSphere(waypoint.position,0.3f);
            Gizmos.DrawLine(previousPosition, waypoint.position);
            previousPosition = waypoint.position;
        }
        Gizmos.DrawLine(previousPosition, startPosition);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * viewDistance);
    }

    void Awake()
    {
        estado1 = new AIEstadoRonda();
        estado2 = new AIEstadoPersecucion();
        estadoActual = estado1;
        waypoints = new Vector3[pathHolder.childCount];
        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = pathHolder.GetChild(i).position;
            waypoints[i] = new Vector3(waypoints[i].x, transform.position.y, waypoints[i].z);
        }
        transform.position = waypoints[0];
        targetWaypointIndex = 1;
        targetWaypoint = waypoints[targetWaypointIndex];
        transform.LookAt(targetWaypoint);
        estadoActual.Iniciar(this, targetWaypoint,targetWaypointIndex);
    }

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        viewAngle = spotLight.spotAngle;
        originalSpotlightColor = spotLight.color;
    }

    override protected void CorrerEstado(bool enRango)
    {
        if (estadoActual == estado1) {
            if (enRango)
            {
                estadoActual.Correr(true, transform, player, waypoints, speed, turnSpeed);
            }
            else
            {
                estadoActual.Correr(false, transform, player, waypoints, speed, turnSpeed);
            }
        }
        else
        {
            if (enRango)
            {
                estadoActual.Correr(true, transform, player);
            }
            else
            {
                estadoActual.Correr(false, transform, player);
            }
        }

    }

    override public void CambiarEstado(bool persecucion)
    {
        if (persecucion)
        {
            
            estadoActual = estado2;
            estadoActual.Iniciar(this);
        }
        else
        {
            estadoActual = estado1;
            estadoActual.Iniciar(this,targetWaypoint,targetWaypointIndex);
            transform.LookAt(targetWaypoint);
        }
    }

    void FixedUpdate()
    {
        
    }

    /*

    IEnumerator FollowPath(Vector3[] waypoints)
    {
        transform.position = waypoints[0];
        int targetWaypointIndex = 1;
        Vector3 targetWaypoint = waypoints[targetWaypointIndex];
        transform.LookAt(targetWaypoint);

        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetWaypoint, speed * Time.deltaTime);
            if (transform.position == targetWaypoint)
            {
                targetWaypointIndex = (targetWaypointIndex + 1) % waypoints.Length;
                targetWaypoint = waypoints[targetWaypointIndex];
                yield return new WaitForSeconds(waitTime);
                yield return StartCoroutine(TurnToFace(targetWaypoint));
            }
            yield return null;
        }

    }

    IEnumerator TurnToFace(Vector3 lookTarget)
    {
        Vector3 dirToLookTarget = (lookTarget - transform.position).normalized;
        float targetAngle = 90 - Mathf.Atan2(dirToLookTarget.z, dirToLookTarget.x) * Mathf.Rad2Deg;

        while (Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.y, targetAngle))>0.05f)
        {
            float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetAngle, turnSpeed * Time.deltaTime);
            transform.eulerAngles = Vector3.up * angle;
            yield return null;
        }
    }
    */

}
