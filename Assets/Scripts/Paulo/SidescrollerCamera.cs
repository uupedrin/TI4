using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SidescrollerCamera : MonoBehaviour
{
    Camera cam;
    [SerializeField] GameObject player;
    [SerializeField] float horizontalThreshold;
    [SerializeField] float verticalThreshold;
    [SerializeField] float speed;
    
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void FixedUpdate()
    {
        //Reposition();
        Vector3 playerPos = cam.WorldToViewportPoint(player.transform.position);
        if(playerPos.x <= horizontalThreshold || playerPos.x >= 1 - horizontalThreshold)
        {
            transform.position = Vector3.Slerp(transform.position, new Vector3(playerPos.x * 10, 0, 0), speed);
        }
        Debug.Log(playerPos);
    }

    void Reposition()
    {
        Vector3 playerPos = cam.WorldToViewportPoint(player.transform.position);
        if(playerPos.x <= horizontalThreshold)
        {
            transform.position = Vector3.Slerp(transform.position, playerPos, speed);
        }
        else if(playerPos.x >= 1 - horizontalThreshold)
        {
            transform.position += new Vector3(playerPos.x, 0, 0);
        }
        if(playerPos.y <= verticalThreshold)
        {
            transform.position -= new Vector3(0, playerPos.y, 0);
        }
        else if(playerPos.y >= 1 - verticalThreshold)
        {
            transform.position += new Vector3(0, playerPos.y, 0);
        }
    }
}
