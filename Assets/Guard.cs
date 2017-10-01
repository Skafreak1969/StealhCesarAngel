using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour
{

    [SerializeField] protected Light spotLight;
    [SerializeField] protected float viewDistance;
    [SerializeField] protected LayerMask viewMask;
    [SerializeField] float timeToSpotPlayer = 0.5f;

    protected float viewAngle;
    protected float playerVisibleTimer;

    protected Transform player;
    protected Color originalSpotlightColor;

    AIEstado estadoActual;
    AIEstadoHold estado1;
    AIEstadoPersecucion estado2;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * viewDistance);
    }

    virtual public void CambiarEstado(bool persecucion)
    {
        if (persecucion)
        {
            estadoActual = estado2;
            estadoActual.Iniciar(this);
        }
        else
        {
            estadoActual = estado1;
            estadoActual.Iniciar(this);
        }
    }

    void Awake()
    {
        estado1 = new AIEstadoHold();
        estado2 = new AIEstadoPersecucion();
        estadoActual = estado1;
        estadoActual.Iniciar(this);
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        viewAngle = spotLight.spotAngle;
        originalSpotlightColor = spotLight.color;
    }

    public bool CanSeePLayer()
    {
        if (Vector3.Distance(transform.position, player.position) < viewDistance)
        {
            Vector3 dirToPLayer = (player.position - transform.position).normalized;
            float angleBetweenGuardAndPlayer = Vector3.Angle(transform.forward, dirToPLayer);
            if (angleBetweenGuardAndPlayer < viewAngle / 2f)
            {
                if (!Physics.Linecast(transform.position, player.position, viewMask))
                {
                    return true;
                }
            }
        }
        return false;
    }

    virtual protected void CorrerEstado(bool enRango)
    {
        if (enRango)
        {
            estadoActual.Correr(true, transform, player);
        }
        else
        {
            estadoActual.Correr(false, transform, player);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (CanSeePLayer())
        {
            playerVisibleTimer += Time.deltaTime;
        }
        else
        {
            playerVisibleTimer -= Time.deltaTime;
        }
        playerVisibleTimer = Mathf.Clamp(playerVisibleTimer, 0, timeToSpotPlayer);
        spotLight.color = Color.Lerp(originalSpotlightColor, Color.red, playerVisibleTimer / timeToSpotPlayer);

        if (playerVisibleTimer >= timeToSpotPlayer)
        {
            CorrerEstado(true);
        }
        else
        {
            CorrerEstado(false);
        }
    }
}
