using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SombraEdit : MonoBehaviour
{
    [SerializeField] float tamanho;
    void OnValidate()
    {
        this.gameObject.transform.localScale = new Vector3 (tamanho, this.gameObject.transform.localScale.y, tamanho);
    }
}
