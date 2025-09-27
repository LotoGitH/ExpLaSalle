using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Life : MonoBehaviour
{
    public Image lifebar;


    public float initialLife;

    private MasterSFX _masterSfx;
    private GameManager _gameManager;
    private float _inGameLife;
    public float _inGameLifeEnc
    {
        get => _inGameLife;
        set
        {
            _inGameLife = value;
            float percentage = _inGameLife / initialLife;
            lifebar.fillAmount = percentage;
        }
    }


// Start is called before the first frame update
    void Start()
    {
        _masterSfx = GameObject.FindObjectOfType<MasterSFX>();
        _gameManager = GameObject.FindObjectOfType<GameManager>();
        RestartLife();
    }

    public void RestartLife()
    {
        _inGameLifeEnc = initialLife;
    }

    public void TakeDamage(float d)
    {
        _inGameLifeEnc = _inGameLifeEnc - d <= 0 ? 0 : _inGameLifeEnc - d;
        _masterSfx.PlaySFX("damage");
        if (_inGameLifeEnc == 0)
        {
         _gameManager.EndGame();   
        }
    }

    public void healPack(float h)
    {
        _inGameLifeEnc = _inGameLifeEnc + h <= initialLife ? initialLife : _inGameLifeEnc - h;
    }
}