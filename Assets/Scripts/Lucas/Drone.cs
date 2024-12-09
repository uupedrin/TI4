using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

[RequireComponent(typeof(AudioSource))]
public class Drone : MonoBehaviour
{
	[SerializeField] GameObject _mesa, prefab;
	[SerializeField] float _speed;
	[SerializeField] Transform MeioSala, mesaSpawn;
	[SerializeField] private bool indo, voltando, meio, colocando;
	[SerializeField] private Transform transformAtual;
	[SerializeField] private float timeEspera;
	private float startTime;
	private Vector3 startPos;
	private int p;
	[SerializeField] private Vector3 startT;
	[SerializeField] private int maxMesas = 5;
	private int mesasInstanciadas = 0;
	[SerializeField] private TextMeshProUGUI mesasText;
	private List<GameObject> mesasCriadas = new List<GameObject>(); 
	[SerializeField] private List<Transform> lugares = new List<Transform>();
	
	AudioSource source;
	
	private Transform atual;

	void Awake()
	{
		startT = transform.position;
		_mesa = Instantiate(prefab, mesaSpawn.position, mesaSpawn.rotation, transform);
		mesasCriadas.Add(_mesa);
		mesasInstanciadas++;
		source = GetComponent<AudioSource>();
		source.enabled = false;
	}

	void Start(){
		AtualizarTextoMesas();

	}
	void Update()
	{

	}

	public void Colocar(Transform Pos)
	{
		if(colocando)
		{
			lugares.Add(Pos);
			atual = Pos;
		}
		else
		{
			transformAtual = Pos;
			startPos = transform.position;
			meio = true;
			colocando = true;
			StartCoroutine(MoveObject());
		}
	}

	IEnumerator MoveObject()
	{
		source.enabled = true;
		while (meio || indo || voltando)
		{
			if (meio)
			{
				transform.position = Vector3.MoveTowards(transform.position, MeioSala.position, _speed * Time.deltaTime);
				if (Vector3.Distance(transform.position, MeioSala.position) < 0.01f)
				{
					meio = false;
					indo = true;
					startPos = transform.position;
					yield return new WaitForSeconds(timeEspera);
				}
			}
			else if (indo)
			{
				transform.position = Vector3.MoveTowards(transform.position, transformAtual.position, _speed * Time.deltaTime);
				if (Vector3.Distance(transform.position, transformAtual.position) < 0.01f)
				{
					voltando = true;
					indo = false;
					startPos = transform.position;
					_mesa.transform.SetParent(null);
					_mesa.GetComponent<Rigidbody>().useGravity = true;
					yield return new WaitForSeconds(timeEspera);
				}
			}
			else if (voltando)
			{
				transform.position = Vector3.MoveTowards(transform.position, startT, _speed * Time.deltaTime);
				if (Vector3.Distance(transform.position, startT) < 0.01f)
				{
					voltando = false;
					startPos = transform.position;
					colocando = false;
					if (mesasInstanciadas < maxMesas)
					{
						_mesa = Instantiate(prefab, mesaSpawn.position, mesaSpawn.rotation, transform);
						mesasCriadas.Add(_mesa);
						mesasInstanciadas++;
						AtualizarTextoMesas();
					}
					else
					{
						Debug.Log("Número máximo de mesas instanciadas atingido.");
					}
				}
			}
			yield return null;
			
		}
		source.enabled = false;
		if(lugares.Count != 0 && !colocando)
			{
				Colocar(lugares[0]);
				lugares.RemoveAt(0);
				yield return null;
			}
		yield return null;
			
	}
	public void DestruirUltimaMesa()
	{
		if (mesasCriadas.Count > 1) 
		{
			GameObject penultimaMesa = mesasCriadas[mesasCriadas.Count - 2];
			mesasCriadas.RemoveAt(mesasCriadas.Count - 2); 
			Destroy(penultimaMesa); 
			mesasInstanciadas--; 
			AtualizarTextoMesas(); 
		}
		else
		{
			Debug.Log("Não há mesas suficientes para destruir a penúltima.");
		}
	}

	private void AtualizarTextoMesas()
	{
		mesasText.text = "Mesas: " + mesasInstanciadas + "/" + maxMesas;
	}
}