using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlatformTremble : MonoBehaviour
{
    [SerializeField] float _timeToCrash = 2.0f, _timeToRegen = 3.0f;
    [SerializeField] Quaternion _initialCrashRotation;
    [SerializeField] float _smoothTremble = 1.0f;
    float _crashingTimingStart, _timing;
    bool _isCrashing, isCrashed;
    [SerializeField] bool ajustarCollider = true;
    [SerializeField] Transform _platformBody;

    Action Crashed, Regenerated;

    public void Awake()
    {
        Crashed += OnCrash;
        Regenerated += OnRegen;
        GetComponent<Rigidbody>().useGravity = false;
        if(ajustarCollider)
             GetComponent<BoxCollider>().size = _platformBody.localScale;

    }

    public void ActiveRoutine()
    {
        if(!_isCrashing)
        {    
            _crashingTimingStart = Time.time;
            StartCoroutine(StartVibrationCrash());
            _platformBody.rotation = _initialCrashRotation;
        }
    }

    public IEnumerator RegenPlatform()
    {
        if(isCrashed)
        {
            yield return new WaitForSeconds(_timeToRegen);
            Regenerated();
        }
    }

    void OnCrash()
    {
        _platformBody.gameObject.SetActive(false);
        GetComponent<BoxCollider>().enabled = false;
        isCrashed = true;
        StartCoroutine(RegenPlatform());
    }

    void OnRegen()
    {
        GetComponent<BoxCollider>().enabled = true;
        _timing = 0;
        _platformBody.rotation = Quaternion.Euler(Vector3.zero);
        _platformBody.gameObject.SetActive(true);

    }

    public IEnumerator StartVibrationCrash()
    { 
        _isCrashing = true;
        Quaternion inverseRotation = Quaternion.Inverse(_initialCrashRotation);
        while(_isCrashing)
        {
            yield return new WaitForSeconds(0.2f);
            _platformBody.rotation = Quaternion.Slerp(_initialCrashRotation, inverseRotation,_smoothTremble * Time.deltaTime);
            yield return new WaitForSeconds(0.2f);
            _platformBody.rotation = Quaternion.Slerp(inverseRotation, _initialCrashRotation,_smoothTremble * Time.deltaTime);
            _timing = Time.time;
            if(_timing - _crashingTimingStart >= _timeToCrash)
            {
                _isCrashing = false;
            }
        }

        Crashed();
    }
}
