using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrocarCena : MonoBehaviour
{
    public string sceneName;
    public Animator doorAnimator;
    public Animator cameraAnimator;
    public string doorAnimationTrigger = "Porta";
    public string cameraAnimationTrigger = "CameraApproach";
    public float animationDuration = 1.5f;
    
    public GameObject sprite1;
    public GameObject sprite2; 

    private bool isSceneChanging = false;

    void OnMouseDown()
    {
        if (!isSceneChanging)
        {
            isSceneChanging = true;

            
            if (sprite1 != null)
            {
                sprite1.SetActive(false);
            }
            if (sprite2 != null)
            {
                sprite2.SetActive(false);
            }

            StartCoroutine(PlayAnimationsAndChangeScene());
        }
    }

    IEnumerator PlayAnimationsAndChangeScene()
    {
        
        if (doorAnimator != null)
        {
            doorAnimator.SetTrigger(doorAnimationTrigger);
        }

        
        if (cameraAnimator != null)
        {
            cameraAnimator.SetTrigger(cameraAnimationTrigger);
        }

        
        yield return new WaitForSeconds(animationDuration);

        
        SceneManager.LoadScene(sceneName);
    }
}