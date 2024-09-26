using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody))]
//[RequireComponent(typeof(HingeJoint))]
//[RequireComponent(typeof(BoxCollider))]
public class ResponsivePlatform : MonoBehaviour
{
    [SerializeField] Transform platformBody;
    [SerializeField] float responsiveForce;

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.transform.GetComponent<PlayerMovement>().GetInputReader().MovementEvent += ResponsiveMove;
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            collision.transform.GetComponent<PlayerMovement>().GetInputReader().MovementEvent -= ResponsiveMove;
        }
    }

    public void ResponsiveMove(Vector2 dir)
    {
        Vector3 direct = new Vector3(dir.x, dir.y, 0);
        GetComponent<Rigidbody>().AddForce(direct * Time.deltaTime * responsiveForce);
    }
}
