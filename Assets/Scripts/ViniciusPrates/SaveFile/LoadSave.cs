using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadSave : MonoBehaviour
{
    [SerializeField] SaveFileSO saveFile;
    [SerializeField] Button button;

    public void Awake()
    {
        saveFile.LoadFromFile(Application.persistentDataPath + "/SaveFile.json");
        if(saveFile.isSaved)
        {
            button.image.color = Color.white;
        }
    }
}
