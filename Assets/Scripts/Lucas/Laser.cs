using UnityEngine;
using UnityEngine.Events;

public class Laser : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private LayerMask ignoreMask;
    [SerializeField] private UnityEvent OnHitTarget;
    [SerializeField] Transform laserInicio,laserFinal;
    [SerializeField] float a,b,c,y;
    float distance;

    private RaycastHit rayHit;
    private Ray ray;
    private Transform currentChild;
    private Vector3 currentChildVec3;

    private void Awake()
    {
        lineRenderer.positionCount = 2;
    }

    private void Update()
    {
        Vector3 direction =  laserFinal.localPosition - laserInicio.localPosition;
        distance = Vector3.Distance(laserInicio.position,laserFinal.position );
        ray = new Ray(laserInicio.position, direction.normalized);
        
        if (Physics.Raycast(ray, out rayHit,distance,~ignoreMask))
        {
            if (currentChild == null || currentChild != rayHit.transform)
            {
                // Se já havia um filho, desativa o parentesco
                if (currentChild != null)
                {
                    currentChild.SetParent(null);
                }

                // Faz o objeto atingido pelo Raycast se tornar filho do lançador do Raycast
                currentChild = rayHit.transform;
                currentChild.SetParent(transform);
                currentChildVec3 = currentChild.position;
                if (currentChild != null)
                {
                    currentChild.SetParent(null);
                    currentChild = null;
                } 
            }

            lineRenderer.SetPosition(0, laserInicio.position);
            //lineRenderer.SetPosition(1, transform.position + transform.forward * laserDistance);
            //ELE PARAR NO PLAYER   
            y = ((b * c)/a);

            lineRenderer.SetPosition(1, rayHit.point);

            //USAR UM METODO DO PLAYER
           // if (rayHit.collider.TryGetComponent(out Target target))
           // {
           //     target.Hit();
            //    OnHitTarget?.Invoke();
            //}
        }
        else
        {  
            lineRenderer.SetPosition(0, laserInicio.position);
            lineRenderer.SetPosition(1, laserFinal.position);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(ray.origin, ray.direction*distance);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(rayHit.point,0.23f);
    }
}