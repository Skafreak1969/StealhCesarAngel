using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SwitchController : MonoBehaviour {
   
    SwitchControllerSingleton instance = SwitchControllerSingleton.Instance;
    [SerializeField]GameObject puerta;
    [SerializeField]GameObject[] arregloIconosSwitch;
    int indiceIconos;
    //bool exito;
    // Use this for initialization
    void Start () {
        foreach (Transform child in transform)
        {
            instance.PedirStackOrden().Push(child.gameObject);
        }
        indiceIconos = 0;
    }

    public void SwitchPrendido(GameObject nuevo)
    {
        instance.PedirStackEncendido().Push(nuevo);
        arregloIconosSwitch[indiceIconos].SetActive(true);
        indiceIconos++;
        //SwitchEncendido.Push(new GameObject());
    }

    public void DesactivarSwitches()
    {
        SceneManager.LoadScene("Prototipo");
    }

    

    // Update is called once per frame
    void Update()
    {
        if (instance.PedirStackEncendido().Count == instance.PedirStackOrden().Count)
        {
            if (!instance.RevisarOrden(instance.PedirStackEncendido(), instance.PedirStackOrden()))
            {
                DesactivarSwitches();
            }
            else
            {
                instance.PedirStackEncendido().Clear();
                puerta.SetActive(false);
                Debug.Log("wohoooooo");
            }
        }
    }
}
