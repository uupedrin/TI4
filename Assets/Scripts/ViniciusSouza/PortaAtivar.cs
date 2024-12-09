using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortaAtivar : MonoBehaviour
{
    [SerializeField] Collider otherCollider;
    [SerializeField] string cena;
     void Update()
    {
        otherCollider.enabled = GameManager.instance.vitoria1 && GameManager.instance.vitoria2;
    }
    public void Ativar(){
        if(otherCollider != null){
            OnTriggerEnter(otherCollider);
            Debug.Log("Script foi ativado");
        }
    }
    void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Player")){
            GetComponent<SceneTrigger>().enabled = true;

        }
    }
}
