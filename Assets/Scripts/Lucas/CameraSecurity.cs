using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSecurity : MonoBehaviour
{
    [SerializeField] private float rotacaoMax;
    [SerializeField] private float Velocidade;
    [SerializeField] private float tempoEspera;
    private Quaternion vetorMax;
    private Quaternion vetorMin;
    private float rotacaoMin;

    void Start()
    {
        Velocidade *= 0.1f;
        rotacaoMin = rotacaoMax * -1;
        vetorMax = Quaternion.Euler(0, rotacaoMax, 0);
        vetorMin = Quaternion.Euler(0, rotacaoMin, 0);
        StartCoroutine(RotateSequentially());
    }

    IEnumerator RotateSequentially()
    {

        while (true)
        {
            Quaternion startRotation = transform.rotation;


            float t = 0;
            while (t < 1)
            {
                t += Time.deltaTime * Velocidade;

                // Interpolação esférica entre a rotação inicial e a rotação alvo
                transform.rotation = Quaternion.Slerp(startRotation, vetorMax, t);
                yield return null;
            }
            yield return new WaitForSeconds(tempoEspera);
            t = 0;
            startRotation = transform.rotation;
            while (t < 1)
            {
                t += Time.deltaTime * Velocidade;

                // Interpolação esférica entre a rotação inicial e a rotação alvo
                transform.rotation = Quaternion.Slerp(startRotation, vetorMin, t);

                yield return null; // Espera até o próximo frame
            }
            yield return new WaitForSeconds(tempoEspera);
        }
    }
}