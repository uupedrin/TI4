using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SaveFile", menuName = "Save Game File", order = 1)]
public class SaveFileSO : ScriptableObject
{
    public string sceneName;
    public string currentSceneName;
    public Vector3 postition;
    public int score;
    public bool Dash;
    public bool PuloDoploAbl;

    public void SaveToFile(string filePath)
    {
        string json = JsonUtility.ToJson(this, true);
        System.IO.File.WriteAllText(filePath, json);
    }

    public void LoadFromFile(string filePath)
    {
        if (System.IO.File.Exists(filePath))
        {
            string json = System.IO.File.ReadAllText(filePath);
            JsonUtility.FromJsonOverwrite(json, this);
        }
    }

}
