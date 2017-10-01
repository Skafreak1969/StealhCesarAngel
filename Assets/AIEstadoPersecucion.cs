using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEstadoPersecucion : AIEstado {
    float tiempoPersecucion=5f;
    override protected void Ejecutar(bool enRango,Transform self,Transform target)
    {
        //Debug.Log("Hola");
        //Vector3 dir = new Vector3(self.position.x - target.position.x, 0, self.position.z - target.position.z);
        //self.Translate(-dir * 5 * Time.deltaTime);
        self.position=Vector3.MoveTowards(self.position, target.position, 5f*Time.deltaTime);
        tiempoPersecucion -= Time.deltaTime;
        self.LookAt(target);
        if (tiempoPersecucion<=0)
        {
            tiempoPersecucion = 5f;
            guard.CambiarEstado(false);
        }
    }
}
