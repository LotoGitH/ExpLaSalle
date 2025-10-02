using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

[RequireComponent(typeof(NavMeshAgent), typeof(Animator),typeof(CapsuleCollider))]
public class FollowPlayer : MonoBehaviour
{
    public float attackColdown = 5;
    private float stopDistance = 4;
    public float attackDamage = 10;
    public float initialLife = 100;
    public Image lifebar;
    public int pointsOnDefeat = 10;

    private GameObject _player;
    private Life _lifePlayer;
    // private Animator _animator;
    private bool _isChasing = false;
    private NavMeshAgent _navMeshAgent;
    private float _attackColdownTimeRef;
    private CapsuleCollider _collider;
    private float _currentLife;
    private ScoreManager _scoreManager;

    public float currentLifeEnc
    {
        get => _currentLife;
        set
        {
            _currentLife = value;
            float percentage = _currentLife / initialLife;
            lifebar.fillAmount = percentage;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindWithTag("Player");
        _lifePlayer = _player.GetComponent<Life>();
        _navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        // _animator = gameObject.GetComponent<Animator>();
        _collider = gameObject.GetComponent<CapsuleCollider>();
        _navMeshAgent.stoppingDistance = 1;
        _scoreManager = GameObject.FindObjectOfType<ScoreManager>();
        _attackColdownTimeRef = Time.time;
    }

    public void EnableOnSpawn()
    {
        currentLifeEnc = initialLife;
        StarFollowPlayer();
    }
    
    public void StarFollowPlayer()
    {
        _navMeshAgent.SetDestination(_player.transform.position);
        _navMeshAgent.isStopped = false;
        _isChasing = true;
        _collider.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isChasing)
        {
            // Debug.Log("PlayerDistance: "+Vector3.Distance(_player.transform.position, transform.position));
            if (Vector3.Distance(_player.transform.position, transform.position) <= stopDistance &&
                _attackColdownTimeRef < Time.time)
            {
                // Debug.Log("attack User");
                // _animator.SetTrigger("Attack");
                _attackColdownTimeRef = Time.time + attackColdown;
                _lifePlayer.TakeDamage(attackDamage);
            }
        }

        // _animator.SetFloat("Distance", Vector3.Distance(_player.transform.position, transform.position));
    }

    public void ContinueChasing()
    {
        _isChasing = true;
        _attackColdownTimeRef = Time.time + attackColdown;
        _navMeshAgent.SetDestination(_player.transform.position);
        _navMeshAgent.isStopped = false;
    }

    public void GetHit(float damageAmount)
    {
        if (currentLifeEnc - damageAmount <= 0)
        {
            currentLifeEnc = 0;
            // _animator.SetBool("Dead", true);
            _collider.enabled = false;
            _scoreManager.AddScore(pointsOnDefeat);
            // Esta linea se quita cuando tengamos animación de muerte
            // _navMeshAgent.isStopped = true;
            DeactiveEnemy();
        }
        else
        {
            currentLifeEnc = _currentLife - damageAmount;
        }

        // _animator.SetTrigger("Hit");
        _isChasing = false;
        // _navMeshAgent.isStopped = true;
    }

    public void DeactiveEnemy()
    {
        gameObject.SetActive(false);
    }

    // private void OnCollisionEnter(Collision other)
    // {
    //     if (other.gameObject.CompareTag("Bullet"))
    //     {
    //         DeactiveEnemy();
    //     }
    // }
}