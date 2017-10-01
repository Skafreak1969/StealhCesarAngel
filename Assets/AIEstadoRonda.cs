using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEstadoRonda : AIEstado {

    protected override void Ejecutar(bool enRango, Transform self, Transform target, Vector3[] waypoints, float speed, float turnSpeed)
    {
        //Debug.Log("hola");
        self.position = Vector3.MoveTowards(self.position, targetWaypoint, speed * Time.deltaTime);
        if (self.position == targetWaypoint)
        {
            targetWaypointIndex = (targetWaypointIndex + 1) % waypoints.Length;
            targetWaypoint = waypoints[targetWaypointIndex];
            Vector3 dirToLookTarget = (targetWaypoint - self.position).normalized;
            float targetAngle = 90 - Mathf.Atan2(dirToLookTarget.z, dirToLookTarget.x) * Mathf.Rad2Deg;

            while (Mathf.Abs(Mathf.DeltaAngle(self.eulerAngles.y, targetAngle)) > 0.05f)
            {
                float angle = Mathf.MoveTowardsAngle(self.eulerAngles.y, targetAngle, turnSpeed * Time.deltaTime);
                self.eulerAngles = Vector3.up * angle;
            }
        }
        if (enRango)
        {
            guard.CambiarEstado(true);
        }
    }
}
