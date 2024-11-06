using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortaAtivar : MonoBehaviour
{
    [SerializeField] Collider otherCollider;
    public void Ativar(){
        if(otherCollider != null){
            OnTriggerEnter(otherCollider);
            Debug.Log("Script foi ativado");
        }
    }
    void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Player")){
            SceneManager.LoadScene("Fase1V2");
        }
    }
}
