using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadSave : MonoBehaviour
{
    [SerializeField] SaveFileSO saveFile;
    [SerializeField] Button button;
    [SerializeField] TrocarCena trocarCena;

    public void Awake()
    {
        saveFile.LoadFromFile(Application.persistentDataPath + "/SaveFile.json");
        if(saveFile.isSaved)
        {
            button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.white;
            button.enabled = saveFile.isSaved;
        }
    }
}
