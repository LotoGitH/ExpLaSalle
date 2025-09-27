using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _timeToDisable = 2;
    private bool _onlyOneHit = true;
    public float gunDamage = 50f;

    public void BulletEnable()
    {
        _onlyOneHit = true;
        gameObject.GetComponent<SphereCollider>().enabled = true;
        gameObject.SetActive(true);
        Rigidbody bulletRigidbody = gameObject.GetComponent<Rigidbody>();
        bulletRigidbody.linearVelocity = Vector3.zero;
        bulletRigidbody.angularVelocity = Vector3.zero;
    }

    IEnumerator AutoDisableByTime(float t)
    {
        yield return new WaitForSeconds(t);
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