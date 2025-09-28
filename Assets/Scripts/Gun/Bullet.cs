using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider),typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float _timeToDisable = 0.2f;
    private bool _onlyOneHit = true;
    public float gunDamage = 50f;
    private Coroutine _autoDisableRoutine;

    public void BulletEnable()
    {
        _onlyOneHit = true;
        gameObject.GetComponent<SphereCollider>().enabled = true;
        gameObject.SetActive(true);
        Rigidbody bulletRigidbody = gameObject.GetComponent<Rigidbody>();
        bulletRigidbody.linearVelocity = Vector3.zero;
        bulletRigidbody.angularVelocity = Vector3.zero;
        if (_autoDisableRoutine != null) StopCoroutine(_autoDisableRoutine);
        _autoDisableRoutine = StartCoroutine(AutoDisableByTime());
    }

    private IEnumerator AutoDisableByTime()
    {
        yield return new WaitForSeconds(_timeToDisable);
        gameObject.SetActive(false);
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.gameObject.CompareTag("Enemy"))
    //     {
    //         other.gameObject.GetComponent<FollowPlayer>().GetHit();
    //         Debug.Log("Hit Enemy");
    //         gameObject.SetActive(false);
    //     }
    // }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy") && _onlyOneHit)
        {
            other.gameObject.GetComponent<FollowPlayer>().GetHit(gunDamage);
            Debug.Log("Hit Enemy: " + other.gameObject.name);
            gameObject.GetComponent<SphereCollider>().enabled = false;
            _onlyOneHit = false;
        }

        gameObject.SetActive(false);
    }
}