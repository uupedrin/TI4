using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObserver
{
    public void Notificar();
}

public interface ISubject
{
    void RegistrarObserver(IObserver observer);
    void NotificarObserver();
}
