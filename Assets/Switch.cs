using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour {
    bool activado;
    public bool once;
    [SerializeField] SwitchController controlador;
    [SerializeField] int identificador;
	// Use this for initialization
	void Awake () {
        once = true;
        activado = false;
        gameObject.GetComponent<Renderer>().material.color = Color.red;
	}

    public int GetIdentificador()
    {
        return identificador;
    }

    public void ActivarSwitch()
    {
        activado = true;
        gameObject.GetComponent<Renderer>().material.color = Color.green;
        controlador.SwitchPrendido(gameObject);
    }

    public void DesactivarSwitch()
    {
        activado = false;
        gameObject.GetComponent<Renderer>().material.color = Color.red;
    }

    void OnTriggerEnter(Collider other)
    { 
        if (other.CompareTag("Player"))
        {
            if (once) {
                ActivarSwitch();
                once = false;
            }
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
