using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MudarSprite : MonoBehaviour
{
    public Sprite newSprite;

    private Button button;
    private Image buttonImage;

    void Start()
    {
        button = GetComponent<Button>();
        buttonImage = GetComponent<Image>();

        if (button != null)
        {
            button.onClick.AddListener(ChangeSprite);
        }
    }

    void ChangeSprite()
    {
        if (newSprite != null && buttonImage != null)
        {
            buttonImage.sprite = newSprite;
        }
    }
}