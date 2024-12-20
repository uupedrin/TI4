using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesligarLasers : MonoBehaviour, ISubject
{

	private List<IObserver> observers = new List<IObserver>();

	public void RegistrarObserver(IObserver observer)
	{
		if (!observers.Contains(observer))
		{
			observers.Add(observer);
			Debug.Log("Registrou");
		}
	}

	public void NotificarObserver()
	{
		foreach (var observer in observers)
			observer.Notificar();
	}
	
	private void OnTriggerEnter(Collider other)
	{
		NotificarObserver();
	}
}
