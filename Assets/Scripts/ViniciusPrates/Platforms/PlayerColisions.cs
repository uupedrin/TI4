using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerColisions : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.transform.CompareTag("InimigoInteracao"))
        {
            Debug.Log("Colidiu - Procure o script Player Collisions para mais informações");
        }
        if(hit.transform.CompareTag("Obstaculo"))
        {
            //GetDamage do player
            if(player.PodeTomarDano)
            {
                player.Life -= 1;
                StartCoroutine(player.Invuneravel());
                if(player.Life <= 0)
                {
                    //SceneManager.LoadScene("Lose");
                }
            }
        }
        if(hit.transform.CompareTag("Espinhos"))
        {
            Debug.Log("Morte");
        }
        if(hit.transform.CompareTag("Mola"))
        {
            if(hit.transform.position.y < transform.position.y)
                player.Mola();
        }

        if(hit.transform.TryGetComponent<PlatformTremble>(out PlatformTremble platform))
            platform.ActiveRoutine();
        
        if(hit.transform.TryGetComponent<ResponsivePlatform>(out ResponsivePlatform responsivePlatform))
            responsivePlatform.Move(GetComponent<CharacterController>());
        
        if(hit.transform.TryGetComponent<Collectable>(out Collectable collectable))
            collectable.Collect();
    }
}
