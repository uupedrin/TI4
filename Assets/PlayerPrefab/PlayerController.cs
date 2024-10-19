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

    #endregion
    #region Movimento Var
    [Header("Confi Andar")]
    [SerializeField] private float velocidade = 3;
    [SerializeField] private float velocidadeCorrida = 6;
    private float velocidadeAntiga;
    private Vector2 myInput;
    private CharacterController characterController;
    [SerializeField] private Transform myCamera;
    [SerializeField] private Animator animator;
    [SerializeField] private bool run;
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
    #region Rolamento var
    [Header("Config Rolamento")]
    public bool rolamento = false;
    [SerializeField] float colliderRolamento;
    [SerializeField] float alturaRolamento;
    private float colliderPrincipal;
    private float alturaPrincipal;
    [SerializeField] AnimationCurve rolamentoCurva;
    private float tempoRolamento;
    [SerializeField] float velocidadeRolamento;
    [SerializeField] float tempoCollider;
    #endregion
    #region Agarrar var
    [Header("Config Agarrar")]
    [SerializeField] bool agarrando;
    [SerializeField] bool podeMover = true;
    [SerializeField] bool gravidadeBool = true;
    [SerializeField] float A;
    [SerializeField] float B;
    float gravidadeAntiga;
    #endregion
    #region Escada
    [Header("Escada")]
    [SerializeField] float velocidadeEscada;
    public bool escada;
    #endregion
    #region Dash
    [Header("Dash")]
    [SerializeField] float DashTempo;
    [SerializeField] float DashSpeed;
    [SerializeField] bool Dash;
    #endregion
    #region WallJump
    [Header("WallJump")]
    [SerializeField] float gravidadeParede;
    [SerializeField] LayerMask raycastLayerMask;
    [SerializeField] float tamanhoRaycast;
    [SerializeField] bool parede;
    private string nomeParedeAnterior;
    #endregion
    #region Verificacao
    [Header("Verificacão")]
    [SerializeField] float timerRolamento;
    [SerializeField] float velY;
    [SerializeField] bool puloMaximo;
    #endregion
    private void Awake()
    {
       // Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        velocidadeAntiga = velocidade;
        characterController = GetComponent<CharacterController>();
        Keyframe rolamento_ultimoFrame = rolamentoCurva[rolamentoCurva.length - 1];
        tempoRolamento = rolamento_ultimoFrame.time - 0.15f;
        gravidadeAntiga = gravidade;
        colliderPrincipal = characterController.height;
        alturaPrincipal = characterController.center.y;

    }


    public void MoverPersonagem(InputAction.CallbackContext value)
    {
        myInput = value.ReadValue<Vector2>();
    }

    private void Update()
    {


        velY = (int)velocidadeMovimento.y;
        if (velY >= 1f)
        {
            
            puloMaximo = true;
        }
        if (wallJump)
            ParedePulo();
        if (AgarrarAbl)
            Agarrar();
        if (escada)
        {
            velocidadeMovimento.y = myInput.y * velocidadeEscada;
            animator.speed = Mathf.Abs(myInput.y);
        }

        chao = NoChao();
        animator.SetBool("Chao", chao);
        animator.SetBool("Parede", parede);


        if (NoChao())
        {
            
            nomeParedeAnterior = null;
            puloMaximo = false;
            Dash = true;
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

        if (!rolamento && podeMover)
            characterController.Move(transform.forward * myInput.magnitude * velocidade * Time.deltaTime);

        if (chao && velocidadeMovimento.y < 0)
        {
            velocidadeMovimento.y = -2f; // Pequeno valor negativo para garantir que o characterController está no chão
        }
        if (gravidadeBool)
        {
            velocidadeMovimento.y -= gravidade * Time.deltaTime;
        }
        characterController.Move(velocidadeMovimento * Time.deltaTime);
        animator.SetBool("Walk", myInput != Vector2.zero);
    }


    private void RotacionarPersonagem()
    {
        Vector3 forward = myCamera.TransformDirection(Vector3.forward);
        Vector3 right = myCamera.TransformDirection(Vector3.right);
        Vector3 targetDirection = myInput.x * right + myInput.y * forward;

        if (myInput != Vector2.zero && targetDirection.magnitude > 0.1f && !rolamento && podeMover)
        {
            Quaternion freeRotation = Quaternion.LookRotation(targetDirection.normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(new Vector3(transform.eulerAngles.x, freeRotation.eulerAngles.y, transform.eulerAngles.z)), 10 * Time.deltaTime);
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
        if (rolamento)
            return;
        if (value.started && agarrando)
        {
            parede = false;
            Dash = true;
            Invoke("PodeMover", 0.35f);
            gravidade = gravidadeAntiga;
            agarrando = false;
            animator.SetTrigger("Pulo");
            pulo = true;
            velocidadeMovimento.y = Mathf.Sqrt((puloAltura * 2) * 2 * gravidade);
        }
        else if (value.started && escada)
        {
            SairEscada();
            Dash = true;
            velocidadeMovimento.y = Mathf.Sqrt((puloAltura * 2) * 2 * gravidade);
        }
        else if (value.started && !NoChao() && parede)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, tamanhoRaycast, raycastLayerMask))
                nomeParedeAnterior = hit.collider.name;
            Dash = true;
            animator.SetTrigger("Pulo");
            pulo = true;
            podeMover = true;
            parede = false;
            velocidadeMovimento = Vector3.zero;
            transform.forward = transform.forward * -1;
            gravidade = gravidadeAntiga;
            velocidadeMovimento.y = Mathf.Sqrt(puloAltura * 2 * gravidade);

        }
        else if (value.started && NoChao())
        {
            Dash = true;
            animator.SetTrigger("Pulo");
            pulo = true;
            velocidadeMovimento.y = Mathf.Sqrt(puloAltura * 2 * gravidade);
        }
        else if (value.started && !NoChao() && podePuloDoplo && PuloDoploAbl)
        {
            Dash = true;
            animator.SetTrigger("Pulo");
            pulo = true;
            velocidadeMovimento = Vector3.zero;
            velocidadeMovimento.y = Mathf.Sqrt(puloAltura * 2 * gravidade);
            podePuloDoplo = false;
        }

    }
    public void RolamentoInput(InputAction.CallbackContext value)
    {
        if (!rolamento && rolamentoAbl)
            StartCoroutine(Rolamento());
    }
    IEnumerator Rolamento()
    {
        if (!agarrando && NoChao())
        {

            animator.SetTrigger("Rolamento");
            rolamento = true;
            timerRolamento = 0;


            while (timerRolamento < tempoRolamento)
            {
                if (timerRolamento >= tempoCollider)
                {
                    characterController.height = colliderRolamento;
                    characterController.center = new Vector3(0, alturaRolamento, 0);
                }

                float speed = rolamentoCurva.Evaluate(timerRolamento);
                Vector3 dir = (transform.forward * velocidadeRolamento) + Vector3.up * velocidadeMovimento.y;
                characterController.Move(dir * Time.deltaTime);
                timerRolamento += Time.deltaTime;
                yield return null;
            }
            rolamento = false;
            characterController.height = colliderPrincipal;
            characterController.center = new Vector3(0, alturaPrincipal, 0);
        }
        if (!agarrando && !NoChao() && Dash)
        {
            rolamento = true;
            timerRolamento = 0;
            Dash = false;


            while (timerRolamento < DashTempo)
            {
                if (timerRolamento >= tempoCollider)
                {
                    characterController.height = colliderRolamento;
                    characterController.center = new Vector3(0, alturaRolamento, 0);
                }

                float speed = rolamentoCurva.Evaluate(timerRolamento);
                gravidadeBool = false;
                Vector3 dir = (transform.forward * DashSpeed);
                velocidadeMovimento = Vector3.zero;
                characterController.Move(dir * Time.deltaTime);
                timerRolamento += Time.deltaTime;
                yield return null;
            }
            gravidadeBool = true;
            rolamento = false;
            characterController.height = colliderPrincipal;
            characterController.center = new Vector3(0, alturaPrincipal, 0);
        }
    }

    public bool NoChao() => characterController.isGrounded;

    void PodeMover()
    {
        podeMover = true;
    }
    public void SairEscada()
    {
        gravidade = gravidadeAntiga;
        gravidadeBool = true;
        escada = false;
        podeMover = true;
        animator.SetBool("Escada", false);
        animator.speed = 1;
    }

    void Agarrar()
    {
        if (velocidadeMovimento.y < 0 && !agarrando)
        {
            RaycastHit downHit;
            Vector3 lineDownStart = (transform.position + Vector3.up * A) + (transform.forward * 0.5f);
            Vector3 lineDownEnd = (transform.position + Vector3.up * B) + (transform.forward * 0.5f);
            Physics.Linecast(lineDownStart, lineDownEnd, out downHit, LayerMask.GetMask("Plataformas"));
            Debug.DrawLine(lineDownStart, lineDownEnd);

            if (downHit.collider != null && caindo)
            {
                RaycastHit fwdHit;
                Vector3 lineFwdStart = new Vector3(transform.position.x, downHit.point.y, transform.position.z);
                Vector3 lineFwdEnd = new Vector3(transform.position.x, downHit.point.y, transform.position.z) + transform.forward;
                Physics.Linecast(lineFwdStart, lineFwdEnd, out fwdHit, LayerMask.GetMask("Plataformas"));
                Debug.DrawLine(lineFwdStart, lineFwdEnd);

                if (fwdHit.collider != null)
                {
                    parede = true;
                    animator.SetTrigger("Agarrar");
                    gravidade = 0;
                    podeMover = false;
                    agarrando = true;
                    velocidadeMovimento.y = 0;
                    Vector3 hangPos = new Vector3(fwdHit.point.x, downHit.point.y, fwdHit.point.z);
                    Vector3 offset = transform.forward * -0.1f + transform.up * -0.1f;
                    hangPos += offset;
                    transform.position = hangPos * -1;
                    transform.forward = fwdHit.normal * -1;
                }
            }
        }
    }
    public void Escada()
    {
        escada = true;
        animator.SetBool("Escada", true);
        podeMover = false;
        gravidadeBool = false;

    }
    void ParedePulo()
    {
        Debug.DrawRay(transform.position, transform.forward * tamanhoRaycast, Color.red);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, tamanhoRaycast, raycastLayerMask))
        {
            if (!NoChao() && puloMaximo && hit.collider.name != nomeParedeAnterior)
            {
                if (velocidadeMovimento.y > 0)
                    velocidadeMovimento.y = 0;
                podePuloDoplo = true;
                animator.SetTrigger("Agarrar");
                transform.forward = hit.normal * -1;
                gravidade = gravidadeParede + Time.deltaTime;
                Debug.DrawRay(transform.position, transform.forward * tamanhoRaycast, Color.green);
                podeMover = false;
                parede = true;
            }
            else
            {
                podeMover = true;
                parede = false;
                gravidade = gravidadeAntiga;
            }


        }
    }
    public void PuloCaixa()
    {
        Dash = true;
        animator.SetTrigger("Pulo");
        pulo = true;
        velocidadeMovimento.y = Mathf.Sqrt(puloAltura * 2 * gravidade);
    }
}

