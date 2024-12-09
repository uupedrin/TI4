using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class DialogoLore : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] float speed;
    [SerializeField] int index;
    char[] letters;

    private void Awake()
    {
        string dialog = text.text;
        text.text = "";
        letters = dialog.ToCharArray();
    }
    private void OnEnable()
    {        
        StartCoroutine(DialogRoutine());
    }

    public IEnumerator DialogRoutine()
    {
        while(text.text.ToCharArray().Count<char>() < letters.Count<char>())
        {
            text.text += letters[index];
            index++;
            yield return new WaitForSeconds(speed);
        }
        yield return null;
    }
}
