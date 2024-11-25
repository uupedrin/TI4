using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraAtaque : MonoBehaviour
{
    [SerializeField] private float tempo;
    [SerializeField] private GameObject spawn;
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
        {
            Debug.Log(":3");
            if(CrossSceneReference.instance.playerController.health > 0)
            {
                CrossSceneReference.instance.playerController.GetComponent<CharacterController>().enabled = false;
                CrossSceneReference.instance.playerController.transform.position = spawn.transform.position;
                CrossSceneReference.instance.playerController.transform.rotation = spawn.transform.rotation;
                CrossSceneReference.instance.playerController.GetComponent<CharacterController>().enabled = true;
                CrossSceneReference.instance.playerController.health--;
                dentro = false;
            }
            else SceneManager.LoadScene("GameOver");
        }       
    }

    
}
