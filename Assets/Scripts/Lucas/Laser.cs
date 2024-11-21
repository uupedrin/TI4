using UnityEngine;
using UnityEngine.Events;

public class Laser : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] Transform laserInicio,laserFinal;
    [SerializeField] private bool seMove;
    [SerializeField] private BoxCollider colider;

    private void Awake()
    {
        colider.enabled = seMove;
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, laserInicio.position);
        lineRenderer.SetPosition(1, laserFinal.position);
    }

    private void Update()
    {
        if(transform.hasChanged)
        {
            lineRenderer.SetPosition(0, laserInicio.position);
            lineRenderer.SetPosition(1, laserFinal.position);
        }
    }
}