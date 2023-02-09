using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip playerHitSound, enemyHitSound, jumpSound, powerPickSound, healthPickSound, enemyDeathSound, hornPickSound, gameOverSound, beltPickSound, bootPickSound, spikedSound;
    static AudioSource audioSrc;

    private void Start()
    {
        playerHitSound = Resources.Load<AudioClip>("PlayerHurtSound");
        enemyHitSound = Resources.Load<AudioClip>("HitSound");
        jumpSound = Resources.Load<AudioClip>("ThorJumpSound");
        powerPickSound = Resources.Load<AudioClip>("DivinePowerSound");
        healthPickSound = Resources.Load<AudioClip>("HealthPickSound");
        enemyDeathSound = Resources.Load<AudioClip>("EnemyDeathSound");
        hornPickSound = Resources.Load<AudioClip>("HornPickSound");
        gameOverSound = Resources.Load<AudioClip>("GameOverSound");
        beltPickSound = Resources.Load<AudioClip>("BeltPickSound");
        bootPickSound = Resources.Load<AudioClip>("BootPickSound");
        spikedSound = Resources.Load<AudioClip>("SpikeHitSound");

        audioSrc = GetComponent<AudioSource>();

 

    }

    public static void PlaySound (string clip)
    {
        switch (clip)
        {
            case "PlayerHurtSound":
                audioSrc.PlayOneShot(playerHitSound);
                break;
            case "HitSound":
                audioSrc.PlayOneShot(enemyHitSound);
                break;
            case "ThorJumpSound":
                audioSrc.PlayOneShot(jumpSound);
                break;
            case "DivinePowerSound":
                audioSrc.PlayOneShot(powerPickSound);
                break;
            case "HealthPickSound":
                audioSrc.PlayOneShot(healthPickSound);
                break;
            case "EnemyDeathSound":
                audioSrc.PlayOneShot(enemyDeathSound);
                break;
            case "HornPickSound":
                audioSrc.PlayOneShot(hornPickSound);
                break;
            case "GameOverSound":
                audioSrc.PlayOneShot(gameOverSound);
                break;
            case "BeltPickSound":
                audioSrc.PlayOneShot(beltPickSound);
                break;
            case "BootPickSound":
                audioSrc.PlayOneShot(bootPickSound);
                break;
            case "SpikeHitSound":
                audioSrc.PlayOneShot(spikedSound);
                break;
        }
    }
}
