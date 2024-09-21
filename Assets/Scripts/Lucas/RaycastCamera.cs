using UnityEngine;

public class RaycastCamera : MonoBehaviour
{
    private float angulo, distancia;
    private Light light;

    [SerializeField] private Transform other;

    void Awake()
    {
        light = GetComponent<Light>();
        distancia = light.range - (light.range/8);
        angulo = Mathf.Cos((light.spotAngle/2) * Mathf.Deg2Rad);
        Debug.Log(angulo);

    }
    void Update()
    {
        Vector3 dir = other.position - transform.position;
        float dot = Vector3.Dot(dir.normalized, transform.forward);
        if (dot > angulo && dir.magnitude < distancia)
        {
            Debug.Log("Colidiu");
        }

    }
}