using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static bool playerIsAlive = true;
    public static bool playerInUpgradeMenu = false;
    public bool cheatsActivated = false;

    public List<GameObject> waves = new List<GameObject>();
    public static int currentWave = 0;

    public static GameManager myGameManager;
    public static Transform playerTrans;
    public static Transform playerSpawn;

    public static GameObject upgrades;
    public static Image bottomBarFill;
    public static Animator waveTextAnim;

    private static Pool pool_LoudAudioSource;
    public static Pool pool_EnemySpear;
    public static Pool pool_EnemyMissile;
    public static Pool pool_Explosion;

    public static ParticleSystem particles_Blood;
    public static ParticleSystem particles_BloodAboveWater;
    public static ParticleSystem particles_Water;
    void Awake()
    {
        myGameManager = GetComponent<GameManager>();
        playerTrans = GameObject.Find("Player").transform;
        playerSpawn = GameObject.Find("PlayerSpawn").transform;

        upgrades = GameObject.Find("Canvas/Upgrades");
        upgrades.SetActive(false);
        bottomBarFill = GameObject.Find("BottomBarFill").GetComponent<Image>();
        waveTextAnim = GameObject.Find("WaveText").GetComponent<Animator>();

        pool_LoudAudioSource = transform.Find("Pool_LoudAudioSource").GetComponent<Pool>();
        pool_EnemySpear = transform.Find("Pool_EnemySpear").GetComponent<Pool>();
        pool_EnemyMissile = transform.Find("Pool_EnemyMissile").GetComponent<Pool>();
        pool_Explosion = transform.Find("Pool_Explosion").GetComponent<Pool>();

        particles_Blood = transform.Find("Particles_Blood").GetComponent<ParticleSystem>();
        particles_BloodAboveWater = transform.Find("Particles_BloodAboveWater").GetComponent<ParticleSystem>();
        particles_Water = transform.Find("Particles_Water").GetComponent<ParticleSystem>();

        SetNewWave(0);
        StartWave();
    }

    public void StartWave() {
        Time.timeScale = 1f;
        waves[currentWave].SetActive(true);
        playerInUpgradeMenu = false;
    }
    public void SetNewWave(int newWaveIndex) {
        for (int i = 0; i < waves.Count; i++) {
            waves[i].SetActive(false);
        }
        currentWave = newWaveIndex;
        waveTextAnim.GetComponent<TextMeshProUGUI>().text = $"Wave {newWaveIndex+1} of 10";
        waveTextAnim.SetTrigger("ShowText");
        bottomBarFill.fillAmount = 0;
        if(newWaveIndex != 0) {
            upgrades.SetActive(true);
            playerInUpgradeMenu = true;
            Cursor.visible = true;
        }
    }

    void Update()
    {
        if (playerIsAlive == false) {
            if(Input.GetButtonDown("Restart")) {
                SetNewWave(0);
                GameManager.playerTrans.GetComponent<PlayerHealth>().currentHealth = 3;
                GameManager.playerTrans.GetComponent<Rigidbody>().position = GameManager.playerSpawn.position;
                GameManager.playerTrans.position = GameManager.playerSpawn.position;
                playerIsAlive = true;
            }
        }
        if (cheatsActivated) {
            if(Input.GetKey(KeyCode.LeftShift)) {
                if(Input.GetKeyDown(KeyCode.Alpha1)) {
                    SetNewWave(1);
                }
                if (Input.GetKeyDown(KeyCode.Alpha2)) {
                    SetNewWave(2);
                }
                if (Input.GetKeyDown(KeyCode.Alpha3)) {
                    SetNewWave(3);
                }
                if (Input.GetKeyDown(KeyCode.Alpha4)) {
                    SetNewWave(4);
                }
                if (Input.GetKeyDown(KeyCode.Alpha5)) {
                    SetNewWave(5);
                }
                if (Input.GetKeyDown(KeyCode.Alpha6)) {
                    SetNewWave(6);
                }
                if (Input.GetKeyDown(KeyCode.Alpha7)) {
                    SetNewWave(7);
                }
                if (Input.GetKeyDown(KeyCode.Alpha8)) {
                    SetNewWave(8);
                }
                if (Input.GetKeyDown(KeyCode.Alpha9)) {
                    SetNewWave(9);
                }
            }
        }
    }


    public static AudioSource SpawnLoudAudio(AudioClip newAudioClip, Vector2 pitch = new Vector2(), float newVolume = 1f) {

        float sfxPitch;
        if (pitch.x <= 0.1f) {
            sfxPitch = 1;
        } else {
            sfxPitch = Random.Range(pitch.x, pitch.y);
        }

        AudioSource audioObject = pool_LoudAudioSource.Spawn(new Vector3(0f, 0f, 0f)).GetComponent<AudioSource>();
        audioObject.GetComponent<AudioSource>().pitch = sfxPitch;
        audioObject.PlayWebGL(newAudioClip, newVolume);
        return audioObject;
        // audio object will set itself to inactive after done playing.
    }
}
