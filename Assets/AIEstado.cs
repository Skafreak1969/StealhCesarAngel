using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEstado {

    protected Guard guard;
    protected Vector3 targetWaypoint;
    protected int targetWaypointIndex;


    public void Iniciar(Guard instancia)
    {
        guard = instancia;
    }

    virtual public void Iniciar(Guard instancia, Vector3 targetWaypoint1, int targetWaypointIndex1)
    {
        guard = instancia;
        targetWaypoint = targetWaypoint1;
        targetWaypointIndex = targetWaypointIndex1;

    }

    virtual protected void Ejecutar(bool enRango, Transform self, Transform target)
    {
       
    }

    virtual protected void Ejecutar(bool enRango, Transform self, Transform target, Vector3[] waypoints, float speed, float turnSpeed)
    {

    }

    public void Correr(bool enRango, Transform self, Transform target)
    {
        Ejecutar(enRango, self, target);
    }

    public void Correr(bool enRango, Transform self, Transform target, Vector3[] waypoints, float speed, float turnSpeed)
    {
        Ejecutar(enRango, self, target, waypoints, speed, turnSpeed);
    }
}
