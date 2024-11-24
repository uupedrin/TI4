using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraAtaque : MonoBehaviour
{
    [SerializeField] private float tempo;
    private bool dentro;
    void OnTriggerEnter(Collider other)
    {
        if(GameManager.instance.cheat)
        return;
        if(!dentro)
        {
            dentro = true;
            StartCoroutine(Ataque());
        }
    }
     void OnTriggerExit(Collider other)
    {
       dentro = false;
    }

    IEnumerator Ataque()
    {
        yield return new WaitForSeconds(tempo);
        if(dentro)
        SceneManager.LoadScene("GameOver");
        
    }

    
}
