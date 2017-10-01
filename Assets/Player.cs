using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
    [SerializeField] float movSpeed = 7;
    [SerializeField] float smoothMoveTime=0.1f;
    [SerializeField] float turnSpeed = 8;

    float angle;
    float smoothInputMagnitude;
    float smoothMoveVelocity;
    Vector3 velocity;

    Rigidbody rigidBody;

    bool escondido;
    bool castigo;

    float tiempoCastigo = 2f;
    float tiempoCastigoMax;

    [SerializeField]GameObject Caja;
    [SerializeField] Image barraCastigo;
    GameObject cajaAux;
	
    // Use this for initialization
	void Start () {
        castigo = false;
        escondido = false;
		rigidBody=GetComponent<Rigidbody>();
        tiempoCastigoMax = tiempoCastigo;
	}

    // Update is called once per frame
    void Update() {
        if (castigo)
        {
            tiempoCastigo -= Time.deltaTime;
            barraCastigo.fillAmount += (Time.deltaTime/tiempoCastigoMax) ;
        }
        if (tiempoCastigo <= 0)
        {
            tiempoCastigo = tiempoCastigoMax;
            castigo = false;
            barraCastigo.fillAmount = 1;
        }
        Vector3 inputDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        float inputMagnitude = inputDirection.magnitude;
        smoothInputMagnitude = Mathf.SmoothDamp(smoothInputMagnitude, inputMagnitude, ref smoothMoveVelocity, smoothMoveTime);

        float targetAngle = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg;
        angle = Mathf.LerpAngle(angle, targetAngle, Time.deltaTime * turnSpeed * inputMagnitude);

        velocity = transform.forward * movSpeed * smoothInputMagnitude;
        if (!castigo) { 
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (!escondido) {
                    movSpeed = 0;
                    escondido = true;
                    cajaAux = Instantiate(Caja, gameObject.transform.position, Quaternion.identity);
                    barraCastigo.fillAmount = 0;
                }
                else {
                    movSpeed = 7;
                    escondido = false;
                    castigo = true;
                    Destroy(cajaAux.gameObject);
                }
            }
        }

	}

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Guard"))
        {
            if (escondido)
            {
                castigo = true;
                movSpeed = 7;
                escondido = false;
                Destroy(cajaAux.gameObject);
            }
        }
    }

    void FixedUpdate()
    {
        rigidBody.MoveRotation(Quaternion.Euler(Vector3.up * angle));
        rigidBody.MovePosition(rigidBody.position + velocity * Time.deltaTime);
    }
}
