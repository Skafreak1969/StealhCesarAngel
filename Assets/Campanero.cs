using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Campanero : Guard {
    [SerializeField] protected LayerMask viewMaskSphere;
    [SerializeField]Guard[] aiCercanos;
    /*
    AIEstadoHold estado1;
    AIEstadoHold estado2;
    AIEstado estadoActual;
    */

    // Use this for initialization
    void Awake () {
        Collider[] coleccionEnemigos = Physics.OverlapSphere(transform.position, 20, viewMaskSphere);
        aiCercanos = new Guard[coleccionEnemigos.Length];
        int i = 0;
        foreach (Collider a in coleccionEnemigos)
        {
            if (a.gameObject.GetComponent<RoundGuard>()!=null) {
                aiCercanos[i] = a.gameObject.GetComponent<RoundGuard>();
            }
            if(a.gameObject.GetComponent<Guard>() != null)
            {
                aiCercanos[i] = a.gameObject.GetComponent<Guard>();
            }
            i++;
        }
        /*
        estado1 = new AIEstadoHold();
        estado2 = new AIEstadoHold();
        estadoActual = estado1;
        */
	}
	
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        //Gizmos.DrawSphere(transform.position, 20);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * viewDistance);
    }

    protected override void CorrerEstado(bool enRango)
    {
        if (enRango)
        {
            Collider[] coleccionEnemigos = Physics.OverlapSphere(transform.position, 20, viewMaskSphere);
            aiCercanos = new Guard[coleccionEnemigos.Length];
            int i = 0;
            foreach (Collider a in coleccionEnemigos)
            {
                if (a.gameObject.GetComponent<RoundGuard>() != null)
                {
                    aiCercanos[i] = a.gameObject.GetComponent<RoundGuard>();
                }
                if (a.gameObject.GetComponent<Guard>() != null)
                {
                    aiCercanos[i] = a.gameObject.GetComponent<Guard>();
                }
                i++;
            }

            foreach (Guard a in aiCercanos)
            {
                //Debug.Log("aja");
                a.CambiarEstado(true);
            }
        }
    }
}
