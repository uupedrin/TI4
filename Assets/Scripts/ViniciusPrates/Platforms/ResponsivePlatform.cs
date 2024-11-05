using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(HingeJoint))]
[RequireComponent(typeof(BoxCollider))]
public class ResponsivePlatform : MonoBehaviour
{
    public HingeJoint hingeJoint;
    public Transform player;
    public float baseSpeed = 10f;
    public float maxSpeed = 25f;
    public float speedIncreaseRate = 3.5f;

    private JointMotor motor;

    void Start()
    {

        motor = hingeJoint.motor;
        motor.targetVelocity = baseSpeed;
        hingeJoint.motor = motor;
        hingeJoint.useMotor = true;
    }

    void Update()
    {
        float playerDistance = Vector3.Distance(player.position, transform.position);

        float speedIncrease = Mathf.Clamp((1 / playerDistance) * speedIncreaseRate, 0, maxSpeed - baseSpeed);

        motor.targetVelocity = baseSpeed + speedIncrease;
        hingeJoint.motor = motor;
    }

    private void OnCollisionStay(Collision collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody playerRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            float playerMovement = playerRigidbody.velocity.magnitude;

            motor.targetVelocity = Mathf.Clamp(motor.targetVelocity + playerMovement * Time.deltaTime, baseSpeed, maxSpeed);
            hingeJoint.motor = motor;
        }
    }
}
