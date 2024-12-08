using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillsUI : MonoBehaviour
{
    [SerializeField] GameObject dashImage;
    [SerializeField] GameObject doubleJumpImage;

    public void Start()
    {
        GameManager.instance.skillsUI = this;
    }
    
    public void ActiveDoubleUI()
    {
        doubleJumpImage.SetActive(true);
    }

    public void ActiveDashUI()
    {
        dashImage.SetActive(true);
    }
}
