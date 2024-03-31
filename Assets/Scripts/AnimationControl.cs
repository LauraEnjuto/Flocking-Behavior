using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControl : MonoBehaviour
{
    private SteeringBehaviour _steeringBehaviour;
    private Animator _anim;

    void Awake()
    {
        _steeringBehaviour = GetComponent<SteeringBehaviour>();
        _anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(_steeringBehaviour)
            _anim.speed = Mathf.Max(1f, _steeringBehaviour.GetCurrentSpeed());
    }
}
