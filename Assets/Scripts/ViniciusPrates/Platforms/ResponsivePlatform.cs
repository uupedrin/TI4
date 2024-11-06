using UnityEngine;

public class ResponsivePlatform : MonoBehaviour
{
    private HingeJoint hinge;
    private JointMotor motor;
    [SerializeField] private float responsiveForce;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        hinge = GetComponent<HingeJoint>();

        motor.targetVelocity = -30f;
    }

    public void OnCollisionStay(Collision hit)
    {
        Debug.Log("acertou");
        if (hit.transform.CompareTag("Player"))
        {
            CharacterController cc = hit.transform.GetComponent<CharacterController>();
            rb.AddForce(cc.velocity * responsiveForce);
            Debug.Log(cc.velocity);
        }
    }
}
