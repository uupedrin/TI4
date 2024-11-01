using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    #region Habilidades
    [Header("Habilidades")]
    [SerializeField] private bool wallJump;
    [SerializeField] private bool AgarrarAbl;
    [SerializeField] private bool PuloAbl;
    [SerializeField] private bool PuloDoploAbl;
    [SerializeField] private bool correAbl;
    [SerializeField] private bool rolamentoAbl;
    [SerializeField] private bool podeMover;
    [SerializeField] private bool gravidadeBool;

    #endregion
    #region Movimento Var
    [Header("Confi Andar")]
    [SerializeField] private float velocidade = 3;
    [SerializeField] private float velocidadeCorrida = 6;
    [SerializeField] private float rotVel;
    private float velocidadeAntiga;
    private Vector2 myInput;
    private CharacterController characterController;
    [SerializeField] private Transform myCamera;
    [SerializeField] private Animator animator;
    [SerializeField] private bool run;    
    [SerializeField] private bool sideScroller;
    #endregion
    #region Pulo var
    [Header("Config Pular")]
    [SerializeField] private float gravidade = 9.81f;
    [SerializeField] private float puloAltura = 2.0f;
    private Vector3 velocidadeMovimento;
    [SerializeField] private bool chao;
    [SerializeField] private bool pulo;
    [SerializeField] private bool caindo;
    [SerializeField] private bool podePuloDoplo;
    public bool queda = false;
    #endregion
    #region Verificacao
    [Header("Verificacão")]
    GameObject alternatingPlatforms;
    AlternatingPlatforms alt;
    #endregion
    private void Awake()
    {
        // Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        if (GameObject.Find("AlternatingPlatforms") != null)
        {
            alternatingPlatforms = GameObject.Find("AlternatingPlatforms");
            alt = alternatingPlatforms.GetComponent<AlternatingPlatforms>();
        }
        velocidadeAntiga = velocidade;
        characterController = GetComponent<CharacterController>();
    }


    public void MoverPersonagem(InputAction.CallbackContext value)
    {
        if(!sideScroller) myInput = value.ReadValue<Vector2>();
        else myInput = new Vector2(value.ReadValue<Vector2>().x, 0);
    }

    private void Update()
    {

        chao = NoChao();
        animator.SetBool("Chao", chao);

        if (gravidadeBool)
        {
            velocidadeMovimento.y -= gravidade * Time.deltaTime;
        }

        if (NoChao())
        {
            caindo = false;
            podePuloDoplo = true;
            animator.SetBool("Caindo", false);
        }
        else if (!NoChao())
        {
            caindo = true;
            pulo = false;
            animator.SetBool("Caindo", true);
        }

        RotacionarPersonagem();

        if (run)
            velocidade = velocidadeCorrida;
        else
            velocidade = velocidadeAntiga;

        if (podeMover)
            characterController.Move(transform.forward * myInput.magnitude * velocidade * Time.deltaTime);

        if (chao && velocidadeMovimento.y < 0)
        {
            velocidadeMovimento.y = -2f; // Pequeno valor negativo para garantir que o characterController está no chão
        }
       
        characterController.Move(velocidadeMovimento * Time.deltaTime);
        animator.SetBool("Walk", myInput != Vector2.zero);
    }


    private void RotacionarPersonagem()
    {
        Vector3 forward = myCamera.TransformDirection(Vector3.forward);
        Vector3 right = myCamera.TransformDirection(Vector3.right);
        Vector3 targetDirection = myInput.x * right + myInput.y * forward;

        if (myInput != Vector2.zero && targetDirection.magnitude > 0.1f && podeMover)
        {
            float rotVelTemp = rotVel;
            if(sideScroller) rotVelTemp *= 10;
            Quaternion freeRotation = Quaternion.LookRotation(targetDirection.normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(new Vector3(transform.eulerAngles.x, freeRotation.eulerAngles.y, transform.eulerAngles.z)), rotVelTemp * Time.deltaTime);
        }
    }

    public void Correr(InputAction.CallbackContext value)
    {
        if (correAbl)
        {
            run = value.started || value.performed;
            animator.SetBool("Run", value.started || value.performed);
        }
    }

    public void Pulo(InputAction.CallbackContext value)
    {
        if (value.started && NoChao())
        {
            
            animator.SetTrigger("Pulo");
            pulo = true;
            velocidadeMovimento.y = Mathf.Sqrt(puloAltura * 2 * gravidade);
            if(alternatingPlatforms != null) alt.Alternate();
        }
        else if (value.started && !NoChao() && podePuloDoplo && PuloDoploAbl)
        {
            
            animator.SetTrigger("Pulo");
            pulo = true;
            velocidadeMovimento = Vector3.zero;
            velocidadeMovimento.y = Mathf.Sqrt(puloAltura * 2 * gravidade);
            if(alternatingPlatforms != null) alt.Alternate();
            podePuloDoplo = false;
        }
        else if(value.canceled)
        velocidadeMovimento = Vector3.zero;

    }
    public bool NoChao() => characterController.isGrounded;

    void PodeMover()
    {
        podeMover = true;
    }
}

