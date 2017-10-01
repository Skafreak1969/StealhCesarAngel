using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TriggerFinal : MonoBehaviour {
    bool final;
    [SerializeField]GameObject imagenFinal;

	// Use this for initialization
	void Start () {
        final = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (final)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Time.timeScale = 1;
                SceneManager.LoadScene("Prototipo");
            }
        }
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            Time.timeScale = 0;
            final = true;
            imagenFinal.SetActive(true);

        }
    }
}
