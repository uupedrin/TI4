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
	[SerializeField] public Vector3 startT;
	[SerializeField] private int maxMesas = 5;
	private int mesasInstanciadas = 0;
	[SerializeField] private TextMeshProUGUI mesasText;
	[SerializeField] List<GameObject> mesasCriadas = new List<GameObject>(); 
	[SerializeField] private List<Transform> lugares = new List<Transform>();
	[SerializeField] Image removeImageButton;
	private bool full;

	AudioSource source;

	private Transform atual;

	void Awake()
	{
		startT = transform.position;
		_mesa = Instantiate(prefab, mesaSpawn.position, mesaSpawn.rotation, transform);
		mesasInstanciadas++;
		source = GetComponent<AudioSource>();
		source.enabled = false;
	}

	void Start(){
		AtualizarTextoMesas();
	}


	void Update()
	{
		if(mesasInstanciadas < maxMesas && _mesa == null && full)
		{
			full = false;
			_mesa = Instantiate(prefab, mesaSpawn.position, mesaSpawn.rotation, transform);
			mesasInstanciadas++;
		}

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
			removeImageButton.color = Color.gray;
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
					mesasCriadas.Add(_mesa);
					_mesa.transform.SetParent(null);
					_mesa.GetComponent<Rigidbody>().useGravity = true;
					_mesa = null;
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
					removeImageButton.color = Color.white;
					if (mesasInstanciadas < maxMesas)
					{
						_mesa = Instantiate(prefab, mesaSpawn.position, mesaSpawn.rotation, transform);
						mesasInstanciadas++;
						AtualizarTextoMesas();
					}
					else
					{
						full = true;
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
        if (Vector3.Distance(transform.position, startT) > 0.01f)
        {
            Debug.Log("O drone não está na posição inicial. Não é possível destruir a penúltima mesa.");

        }
		if(colocando)
		{
			return;
		}

        if (mesasCriadas.Count > 0)
        {
			GameObject penultimaMesa = mesasCriadas[mesasCriadas.Count - 1];
            Debug.Log("Removendo mesa: " + penultimaMesa.name);
            mesasCriadas.Remove(penultimaMesa);
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