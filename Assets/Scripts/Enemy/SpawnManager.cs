using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public BulletsPooling enemyPool;

    // public Transform spawnPosition;

    private Transform _player;
    private bool _isSpawningActive = false;
    private GameObject[] _spawnPoints;

    //Variables de referencia de tiempo para crear nuevos enemigos
    private float _spawnRate = 2f;
    private float _spawnTimeRef;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindWithTag("Player").transform;
        _spawnPoints = GameObject.FindGameObjectsWithTag("Respawn");
        StartSpawning();
    }

    public void ResetSpawner()
    {
        enemyPool.ResetAllBullets();
        _spawnRate = 5f;
        _isSpawningActive = false;
    }

    public void StartSpawning()
    {
        _isSpawningActive = true;
        _spawnTimeRef = Time.time + _spawnRate;
    }

    private void SpawnEnemy()
    {
        GameObject e = enemyPool.GetBullet();
        if (e)
        {
            Debug.Log("Spawn",e.gameObject);
            e.transform.position = _spawnPoints[Random.Range(0, _spawnPoints.Length - 1)].transform.position;
            e.transform.LookAt(_player);
            e.gameObject.SetActive(true);
            e.gameObject.GetComponent<FollowPlayer>().EnableOnSpawn();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_isSpawningActive && Time.time > _spawnTimeRef)
        {
           
            _spawnTimeRef = Time.time + _spawnRate;
            SpawnEnemy();
        }
    }
}