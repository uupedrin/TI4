using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SaveFile", menuName = "Save Game File", order = 1)]
public class SaveFileSO : ScriptableObject
{
    public SceneField currentScene;
    public Transform transform;
    public int score;
    public bool Dash;
    public bool PuloDoploAbl;
    public bool isSaved = false;

    public void SaveToFile(string filePath)
    {
        string json = JsonUtility.ToJson(this, true);
        System.IO.File.WriteAllText(filePath, json);
        isSaved = true;
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
