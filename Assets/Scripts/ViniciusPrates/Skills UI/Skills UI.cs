using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillsUI : MonoBehaviour
{
    [SerializeField] Image dashImage;
    [SerializeField] Image doubleJumpImage;

    public void Start()
    {
        GameManager.instance.skillsUI = this;
    }
    
    public void ActiveDoubleUI()
    {
        doubleJumpImage.color = Color.white;
    }

    public void ActiveDashUI()
    {
        dashImage.color = Color.white;
    }
}
