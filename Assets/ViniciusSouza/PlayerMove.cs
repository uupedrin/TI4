using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    Rigidbody rb;
    public Transform point;
    public Transform point2;
    public float jumpForce = 5f;
    public float speed = 8f;
    public float maxJumpHoldTime = 1.2f;
    public LayerMask mask;
    public LayerMask vineMask;
    public Vector3 dir = Vector3.zero;
    private bool canDoubleJump = false;
    private bool isOnVine = false;
    private float jumpHoldTime = 0f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        dir = new Vector3(h * speed, rb.velocity.y, v * speed);

        if (CheckVine())
        {
            isOnVine = true;
        }
        else if (isOnVine)
        {
            isOnVine = false;
        }

        if (isOnVine)
        {
            HandleVineClimb();
        }
        else
        {
            HandleJump();
        }

        rb.velocity = new Vector3(dir.x, rb.velocity.y, dir.z);      
    }

    void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpHoldTime = Time.time;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            float holdDuration = Time.time - jumpHoldTime;
            if (holdDuration >= maxJumpHoldTime)
            {
                rb.AddForce(Vector3.up * jumpForce * 2.4f, ForceMode.Impulse) ;
                jumpHoldTime = Time.time;
                canDoubleJump = true;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (CheckGround())
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                canDoubleJump = true;
            }
            else if (canDoubleJump)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                canDoubleJump = false;
            }
            jumpHoldTime = 0f;
        }
    }

    void HandleVineClimb()
    {
        if (CheckVine())
        {
            if (Input.GetKey(KeyCode.Q))
            {
                rb.velocity = new Vector3(0, speed, 0);
            }
            else if (Input.GetKey(KeyCode.E))
            {
                rb.velocity = new Vector3(0, -speed, 0);
            }
            else
            {
                rb.velocity = Vector3.zero;
            }
        }
    }

    bool CheckGround()
    {
        bool onGround = Physics.CheckSphere(point.position, 0.45f, mask);
        if (onGround)
        {
            canDoubleJump = false;
        }
        return onGround;
    }

    bool CheckVine()
    {
        return Physics.CheckSphere(point2.position, 0.45f, vineMask);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Derrota"))
        {
            SceneManager.LoadScene("ROque");
        }
        if (other.gameObject.CompareTag("Vitoria"))
        {
            SceneManager.LoadScene("Roque");
        }

        PlatformTremble platform;
        if(other.transform.TryGetComponent<PlatformTremble>(out platform))
            platform.ActiveRoutine();
    }

    void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & vineMask) != 0)
        {
            isOnVine = true;
            rb.useGravity = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (((1 << other.gameObject.layer) & vineMask) != 0)
        {
            isOnVine = false;
            rb.useGravity = true;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(point.position, 0.45f);
        Gizmos.DrawWireSphere(point2.position, 0.45f);
    }
}





