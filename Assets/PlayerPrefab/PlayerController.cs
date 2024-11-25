using System;
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
	[SerializeField] public bool PuloDoploAbl;
	[SerializeField] public bool correAbl;
	[SerializeField] public bool rolamentoAbl;
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
	private int xPos =1 , yPos = 1;
	#endregion
	#region Pulo var
	[Header("Config Pular")]
	[SerializeField] private float gravidade = 9.81f;
	[SerializeField] private float puloAltura = 2.0f;
	[SerializeField] private float molaAltura = 8.0f;
	[SerializeField] private float coyoteTime;
	[SerializeField] private float coyoteTimeCounter;
	private Vector3 velocidadeMovimento;
	[SerializeField] private bool chao;
	[SerializeField] private bool coyote;
	[SerializeField] private bool pulo;
	[SerializeField] private bool caindo;
	[SerializeField] private bool cancel = true;
	[SerializeField] private bool podePuloDoplo;
	public bool queda = false;
	#endregion

	#region Dash var
	[Header("Config Dash")]
	public bool rolamento = false;
	[SerializeField] AnimationCurve rolamentoCurva;
	private float tempoRolamento;
	[SerializeField] float velocidadeRolamento;
	[SerializeField] float DashTempo;
	[SerializeField] float DashSpeed;
	[SerializeField] bool Dash = true;
	#endregion

	#region Verificacao
	[Header("Verificacão")]
	[SerializeField] float timerRolamento;
	#endregion

	#region Dano
	[Header("Dano")]
	[SerializeField] int life = 3;
	public int Life
	{
		get { return life; }
		set => life = value;
	}
	float invuneravelCD = 1;
	[SerializeField] bool podeTomarDano = true;
	public bool PodeTomarDano
	{
		get { return podeTomarDano; }
		set => podeTomarDano = false;
	}
	#endregion
	
	#region Events
	public event Action PlayerJustJumped;
	public event Action EmitSmoke;
	#endregion

	private void Awake()
	{
		// Cursor.lockState = CursorLockMode.Locked;
		//Cursor.visible = false;
		velocidadeAntiga = velocidade;
		characterController = GetComponent<CharacterController>();
	}

	private void Start()
	{
		CrossSceneReference.instance.playerController = this;
	}
	
	public IEnumerator Invuneravel()
	{
		podeTomarDano = false;
		yield return new WaitForSeconds(invuneravelCD);
		podeTomarDano = true;
	}

	public void MoverPersonagem(InputAction.CallbackContext value)
	{
		if(!sideScroller) myInput = value.ReadValue<Vector2>();
		else myInput = new Vector2(value.ReadValue<Vector2>().x, 0);
	}

	private void Update()
	{
		if(characterController.isGrounded)
		coyoteTimeCounter = coyoteTime;
		else
		coyoteTimeCounter -= Time.deltaTime;


		animator.SetBool("Run", run);
		chao = NoChao();
		animator.SetBool("Chao", chao);

		if (gravidadeBool)
		{
			velocidadeMovimento.y -= gravidade * Time.deltaTime;
		}

		if (NoChao())
		{
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

			if (myInput != Vector2.zero && targetDirection.magnitude > 0.1f)
			{
					
					float rotVelTemp = rotVel;
					//podeMover = false; // Bloqueia o movimento durante a rotação
				if(sideScroller) rotVelTemp *= 10;
					Quaternion freeRotation  = Quaternion.LookRotation(targetDirection.normalized);

					
					Quaternion a = Quaternion.Slerp(transform.rotation, Quaternion.Euler(new Vector3(transform.eulerAngles.x, freeRotation.eulerAngles.y, transform.eulerAngles.z)), rotVelTemp *10* Time.deltaTime);
					Quaternion b = Quaternion.Slerp(transform.rotation, Quaternion.Euler(new Vector3(transform.eulerAngles.x, freeRotation.eulerAngles.y, transform.eulerAngles.z)), rotVelTemp * Time.deltaTime);
					if(Math.Abs(Math.Abs(a.eulerAngles.y) - Math.Abs(b.eulerAngles.y)) > 45f )
					podeMover = false;
					else
					podeMover = true;
					transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(new Vector3(transform.eulerAngles.x, freeRotation.eulerAngles.y, transform.eulerAngles.z)), rotVelTemp * Time.deltaTime);
					
					
			}
			
		
	}

	public void Correr(InputAction.CallbackContext value)
	{
		if (correAbl)
		{
			//run = value.started || value.performed;
			
		}
	}
	
	public void Pulo(InputAction.CallbackContext value)
	{
		if (value.started && NoChao())
		{
			coyoteTimeCounter = -1;
			animator.SetTrigger("Pulo");
			pulo = true;
			velocidadeMovimento.y = Mathf.Sqrt(puloAltura * 2 * gravidade);
			PlayerJustJumped?.Invoke();
			StartCoroutine(WaitToSmoke());
			cancel = false;
		}
		else if (value.started && !NoChao() && podePuloDoplo && PuloDoploAbl)
		{
			
			animator.SetTrigger("Pulo");
			pulo = true;
			velocidadeMovimento = Vector3.zero;
			velocidadeMovimento.y = Mathf.Sqrt(puloAltura * gravidade);
			//PlayerJustJumped?.Invoke();
			EmitSmoke?.Invoke();
			podePuloDoplo = false;
		}
		else if(value.canceled && !cancel)
		{
			//velocidadeMovimento = Vector3.zero;
			if(velocidadeMovimento.y > 0)
			{
				coyoteTimeCounter = -1;
				velocidadeMovimento.y = Mathf.Sqrt((puloAltura/5) * 2 * gravidade);
				cancel = true;
			}
		}
	}
	IEnumerator WaitToSmoke()
	{
		yield return new WaitForSeconds(.2f);
		EmitSmoke?.Invoke();
	}
	public bool NoChao() => coyoteTimeCounter > 0;

	void PodeMover()
	{
		podeMover = true;
	}

	 public void RolamentoInput(InputAction.CallbackContext value)
	{
			if(!characterController.isGrounded && rolamentoAbl)
			StartCoroutine(Rolamento());
	}
	IEnumerator Rolamento()
	{

		if (Dash)
		{
			rolamento = true;
			timerRolamento = 0;
			Dash = false;
			animator.SetTrigger("Dash");
			EmitSmoke?.Invoke();

			while (timerRolamento < DashTempo)
			{
				
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
		}
	}

	public void Mola()
	{
		animator.SetTrigger("Pulo");
			pulo = true;

			coyoteTimeCounter = 0;
			velocidadeMovimento.y = Mathf.Sqrt(molaAltura * gravidade);
	}

	public void CHEAT()
	{
		GameManager.instance.cheat = !GameManager.instance.cheat;
	}

}


