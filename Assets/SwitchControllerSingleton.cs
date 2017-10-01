using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchControllerSingleton : MonoBehaviour {
    Stack<GameObject> OrdenSwitches = new Stack<GameObject>();
    Stack<GameObject> SwitchEncendido = new Stack<GameObject>();

    public Stack<GameObject> PedirStackOrden()
    {
        return OrdenSwitches;
    }
    public Stack<GameObject> PedirStackEncendido()
    {
        return SwitchEncendido;
    }

    void Awake()
    {
        //SwitchEncendido.Push(new GameObject());
        
        
        //Debug.Log(OrdenSwitches.Count);

    }

    public bool RevisarOrden(Stack<GameObject> SwitchEncendido, Stack<GameObject> OrdenSwitches)
    {
        Stack<GameObject> aux = new Stack<GameObject>();
        Stack<GameObject> aux2 = new Stack<GameObject>();
        bool iguales = true;

        for(int i= 0; i <= SwitchEncendido.Count+1; i++)
        {
            //Debug.Log(i + " " + SwitchEncendido.Count);
            if (SwitchEncendido.Count!=0&& OrdenSwitches.Count != 0) {
                Debug.Log(OrdenSwitches.Peek().gameObject.GetComponent<Switch>().GetIdentificador() + " " + SwitchEncendido.Peek().gameObject.GetComponent<Switch>().GetIdentificador());
                Debug.Log(SwitchEncendido.Count);
                if (OrdenSwitches.Peek().gameObject.GetComponent<Switch>().GetIdentificador() != SwitchEncendido.Peek().gameObject.GetComponent<Switch>().GetIdentificador())
                {
                    iguales = false;
                    break;
                }   
                aux.Push(OrdenSwitches.Pop());
                aux2.Push(SwitchEncendido.Pop());
            }
            
        }
        if (aux.Count!=0) {
            for (int i = 0; i < aux.Count; i++)
            {
                OrdenSwitches.Push(aux.Pop());
                SwitchEncendido.Push(aux2.Pop());
            }
        }
        Debug.Log(iguales);
        if (!iguales)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private static SwitchControllerSingleton instance;
    public static SwitchControllerSingleton Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new SwitchControllerSingleton();
            }
            return instance;
        }
    }
}
