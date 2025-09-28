using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    // public Transform shootPosition;
    // public float shootForce;
    // private MasterSFX _masterSfx;
    private Vector3 _initialPostion;
    private Quaternion _initialRotation;
    private Rigidbody _rigidbody;

    // public BulletsPooling bulletsPooling;
    // public float gunThrowDamage = 150f;

    // Start is called before the first frame update
    void Start()
    {
        // _masterSfx = GameObject.FindObjectOfType<MasterSFX>();
        _initialPostion = transform.position;
        _initialRotation = transform.rotation;
        _rigidbody = GetComponent<Rigidbody>();
        // _bulletsPooling = GameObject.FindObjectOfType<BulletsPooling>();
    }


    public void ResetGunPosition()
    {
        _rigidbody.useGravity = false;
        _rigidbody.linearVelocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        transform.position = _initialPostion;
        transform.rotation = _initialRotation;
    }

    // public void FireGun()
    // {
    //     _masterSfx.PlayGunSFX("shoot");
    //     GameObject b = bulletsPooling.GetBullet();
    //     if (b)
    //     {
    //         b.transform.position = shootPosition.position;
    //         b.transform.rotation = shootPosition.rotation;
    //         b.GetComponent<Bullet>().BulletEnable();
    //         Rigidbody bulletRigidbody = b.GetComponent<Rigidbody>();
    //         bulletRigidbody.AddForce(shootPosition.right * shootForce, ForceMode.Impulse);
    //     }
    // }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            // other.gameObject.GetComponent<FollowPlayer>().GetHit(gunThrowDamage);
        }

        ResetGunPosition();
    }
}