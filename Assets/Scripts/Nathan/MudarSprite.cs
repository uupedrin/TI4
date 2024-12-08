using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MudarSprite : MonoBehaviour
{
    public Sprite newSprite;
    public Image targetImage;

    private Button button;

    void Start()
    {
        button = GetComponent<Button>();

        if (button != null)
        {
            button.onClick.AddListener(ChangeSprite);
        }
    }

    void ChangeSprite()
    {
        if (newSprite != null && targetImage != null)
        {
            targetImage.sprite = newSprite;
        }
    }
}
