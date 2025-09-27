using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterSFX : MonoBehaviour
{
    public AudioSource playerSFX;
    public AudioSource gunSFX;
    public AudioSource MonsterSFX;
    
    //AudioClips
    public AudioClip audioShoot;

    public AudioClip getDamage;

    private Dictionary<string, AudioClip> _playerClips = new Dictionary<string, AudioClip>(); 
    private Dictionary<string, AudioClip> _gunClips = new Dictionary<string, AudioClip>(); 
    private Dictionary<string, AudioClip> _monsterClips = new Dictionary<string, AudioClip>(); 
    // Start is called before the first frame update
    void Start()
    {
        //Player
        _playerClips.Add("damage",getDamage);
        //Gun
        _gunClips.Add("fire",audioShoot);
    }

    public void PlaySFX(string name)
    {
        if(playerSFX.isPlaying) return;
        switch (name)
        {
            case "damage":
                playerSFX.PlayOneShot(_playerClips["damage"]);
                break;
        }
    } 
    public void PlayGunSFX(string name)
    {
        switch (name)
        {
            case "shoot":
                gunSFX.Stop();
                gunSFX.clip = _gunClips["fire"];
                gunSFX.Play();
                break;
        }
    } 
    
    public void PlaySFXMonster(string name)
    {
        switch (name)
        {
            case "shoot":
                playerSFX.PlayOneShot(_playerClips["fire"]);
                break;
            case "damage":
                playerSFX.PlayOneShot(_playerClips["damage"]);
                break;
        }
    }
}
