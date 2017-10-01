using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEstadoHold : AIEstado {
    Vector3 pos;
    int a = 0;
    override protected void Ejecutar(bool enRango,Transform self, Transform target)
    {
        if (a>=1) {
            self.position = Vector3.MoveTowards(self.position, pos, 10f * Time.deltaTime);
            self.LookAt(pos);
        }
        if (enRango)
        {
            pos = self.position;
            a++;
            guard.CambiarEstado(true);
        }
    }
}
