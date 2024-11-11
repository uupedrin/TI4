using UnityEngine;

public class ResponsivePlatform : MonoBehaviour
{
    private HingeJoint hinge;
    [SerializeField] private float responsiveForce;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        hinge = GetComponent<HingeJoint>();
    }

    public void Move(CharacterController cc)
    {
        Vector3 dir = cc.velocity;
        dir.y = 0;
        rb.AddForce(cc.velocity * responsiveForce);
        Debug.Log(cc.velocity);
    }
}
