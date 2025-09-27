using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsPooling : MonoBehaviour
{
    private GameObject[] _bullets;
    public string poolingName;

    private int _bulletIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        _bullets = GameObject.FindGameObjectsWithTag(poolingName);
        ResetAllBullets();
    }

    public void ResetAllBullets()
    {
        foreach (GameObject b in _bullets)
        {
            b.SetActive(false);
            _bulletIndex = 0;
        }
    }

    public GameObject GetBullet()
    {
        GameObject selected = null;
        if (CheckAvailability())
        {
            while (selected == null)
            {
                int random = Random.Range(0, _bullets.Length - 1);
                if (!_bullets[random].activeInHierarchy)
                {
                    selected = _bullets[random];
                }
            }
        }

        return selected;
    }

    private bool CheckAvailability()
    {
        bool available = false;

        foreach (GameObject b in _bullets)
        {
            if (!b.activeInHierarchy)
            {
                available = true;
            }
        }

        return available;
    }
}