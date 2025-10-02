using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Para reiniciar el contador de vida
    private Life _life;
    /// <summary>
    //Para reiniciar la interfaz
    private GameObject _mainUI;
    /// </summary>
    //Para reiniciar el spawn de enemigos
    private SpawnManager _spawnManager;
    //Para reiniciar el arma
    private Shoot _gunController;
    //Para reiniciar el puntaje
    private ScoreManager _scoreManager;

    private AudioSource _mainActionSong;
    // Start is called before the first frame update
    void Start()
    {
        _life = GameObject.FindObjectOfType<Life>();
        _mainUI = GameObject.Find("MainCanvas");
        _spawnManager = GameObject.FindObjectOfType<SpawnManager>();
        _gunController = GameObject.FindObjectOfType<Shoot>();
        _scoreManager = GameObject.FindObjectOfType<ScoreManager>();
        _mainActionSong = GameObject.Find("ActionSong").GetComponent<AudioSource>();
    }

    public void RestartGame()
    {
        _mainUI.GetComponent<Animator>().SetTrigger("Dead");
        _life.RestartLife();
        _spawnManager.StartSpawning();
        _scoreManager.RestartLevelScore();
        _mainActionSong.Play();
    }

    public void EndGame()
    {
        _mainUI.GetComponent<Animator>().SetTrigger("Start");
        _spawnManager.ResetSpawner();
        _gunController.ResetGunPosition();
        _mainActionSong.Stop();
    }
}