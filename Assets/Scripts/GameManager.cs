using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static bool startedGameOnce = false;
    public static bool gameIsPaused = false;
    public static bool playerIsAlive = true;
    // public static bool playerInUpgradeMenu = false;
    public bool cheatsActivated = false;

    public UnityEvent initGameEvent = new UnityEvent();
    public UnityEvent upgrade0Event = new UnityEvent();
    public UnityEvent upgrade1Event = new UnityEvent();
    public List<GameObject> waves = new List<GameObject>();
    public static int currentWave = 0;
    public static int totalKills = 0;

    public static GameManager myGameManager;
    public static Transform playerTrans;
    public static Transform playerSpawn;
    public static CircleController playerCircle;

    public static GameObject tutorial1;
    public static GameObject tutorial2;
    public static GameObject ending;
    public static GameObject pressR;
    public static GameObject mainMenu;
    public static GameObject credits;
    public static GameObject upgrades;
    public static HeartsCounter heartsCounter;
    public static Image bottomBarFill;
    public static Animator waveTextAnim;
    public static TextMeshProUGUI text_totalKills;

    private static Pool pool_LoudAudioSource;
    public static Pool pool_EnemySpear;
    public static Pool pool_EnemyMissile;
    public static Pool pool_EnemyEyeBullet;
    public static Pool pool_Explosion;

    public static ParticleSystem particles_Blood;
    public static ParticleSystem particles_BloodAboveWater;
    public static ParticleSystem particles_Water;

    public static LayerMask worldMask;
    public static LayerMask bulletsMask;
    public static LayerMask entityMask;
    public static LayerMask triggersMask;

    void Awake()
    {
        myGameManager = GetComponent<GameManager>();
        playerTrans = GameObject.Find("Player").transform;
        playerSpawn = GameObject.Find("PlayerSpawn").transform;
        playerCircle = GameObject.Find("Player/Circle/Triangle").GetComponent<CircleController>();

        tutorial1 = GameObject.Find("Canvas/Tutorial1");
        tutorial1.SetActive(false);
        tutorial2 = GameObject.Find("Canvas/Tutorial2");
        tutorial2.SetActive(false);
        ending = GameObject.Find("Canvas/Ending");
        ending.SetActive(false);
        pressR = GameObject.Find("Canvas/PressR");
        pressR.SetActive(false);
        mainMenu = GameObject.Find("Canvas/MainMenu");
        credits = GameObject.Find("Canvas/Credits");
        credits.SetActive(false);
        upgrades = GameObject.Find("Canvas/Upgrades");
        upgrades.SetActive(false);
        heartsCounter = GameObject.Find("Canvas/HeartsCounter").GetComponent<HeartsCounter>();
        bottomBarFill = GameObject.Find("BottomBarFill").GetComponent<Image>();
        waveTextAnim = GameObject.Find("WaveText").GetComponent<Animator>();
        text_totalKills = GameObject.Find("TotalKills").GetComponent<TextMeshProUGUI>();

        pool_LoudAudioSource = transform.Find("Pool_LoudAudioSource").GetComponent<Pool>();
        pool_EnemySpear = transform.Find("Pool_EnemySpear").GetComponent<Pool>();
        pool_EnemyMissile = transform.Find("Pool_EnemyMissile").GetComponent<Pool>();
        pool_EnemyEyeBullet = transform.Find("Pool_EnemyEyeBullet").GetComponent<Pool>();
        pool_Explosion = transform.Find("Pool_Explosion").GetComponent<Pool>();

        particles_Blood = transform.Find("Particles_Blood").GetComponent<ParticleSystem>();
        particles_BloodAboveWater = transform.Find("Particles_BloodAboveWater").GetComponent<ParticleSystem>();
        particles_Water = transform.Find("Particles_Water").GetComponent<ParticleSystem>();


        worldMask = LayerMask.NameToLayer("World");
        bulletsMask = LayerMask.NameToLayer("Bullets");
        entityMask = LayerMask.NameToLayer("Entity");
        triggersMask = LayerMask.NameToLayer("Triggers");

        initGameEvent.Invoke();
        Time.timeScale = 0f;
        SetNewWave(0);
        PauseGame();
        // StartWave(); called in Start Game button
    }
    public void ResumeGameButton() {
        GameManager.gameIsPaused = false;
        GameManager.mainMenu.SetActive(false);
        Time.timeScale = 1f;
    }
    public void PauseGame() {
        GameManager.gameIsPaused = true;
        GameManager.mainMenu.SetActive(true);
        Time.timeScale = 0f;
    }
    public void StartGameButton() {
        tutorial1.SetActive(true);
    }
    public void StartWave() {
        Time.timeScale = 1f;
        waves[currentWave].SetActive(true);
        gameIsPaused = false;
        startedGameOnce = true;
    }
    public void SetNewWave(int newWaveIndex) {
        for (int i = 0; i < waves.Count; i++) {
            waves[i].SetActive(false);
        }
        currentWave = newWaveIndex;
        waveTextAnim.transform.Find("WaveText").GetComponent<TextMeshProUGUI>().text = $"Wave <color=#0086FF>{newWaveIndex+1}</color> of 10";
        waveTextAnim.SetTrigger("ShowText");
        bottomBarFill.fillAmount = 0;

        if (newWaveIndex == 0) {
            tutorial1.SetActive(false);
            tutorial2.SetActive(false);
            upgrade0Event.Invoke();
        }
        else if (newWaveIndex == 1) {
            tutorial1.SetActive(false);
            tutorial2.SetActive(true);
            upgrade1Event.Invoke();
        } else {
            tutorial1.SetActive(false);
            tutorial2.SetActive(false);
        }
            StartWave(); // this was in the Upgrade Skill menu before
        /*
        if(newWaveIndex != 0) {
            upgrades.SetActive(true);
            playerInUpgradeMenu = true;
            Cursor.visible = true;
        }
        */
    }

    void Update()
    {

        if (startedGameOnce == true && Input.GetButtonDown("Pause")) {
            GameManager.myGameManager.PauseGame();
            GameManager.mainMenu.SetActive(true);
            GameManager.credits.SetActive(false);
        }
        if (playerIsAlive == false) {
            if(Input.GetButtonDown("Restart")) {
                SetNewWave(0);
                StartWave();

                GameManager.totalKills = 0;
                GameManager.text_totalKills.text = $"Kills: 0";
                GameManager.pressR.SetActive(false);

                GameManager.playerTrans.GetComponent<PlayerHealth>().currentHealth = 5;
                GameManager.heartsCounter.SetCurrentHealth(5);
                GameManager.playerTrans.GetComponent<Rigidbody>().position = GameManager.playerSpawn.position;
                GameManager.playerTrans.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                GameManager.playerTrans.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
                GameManager.playerTrans.position = GameManager.playerSpawn.position;
                playerIsAlive = true;
            }
        }
        if (cheatsActivated) {
            if(Input.GetKey(KeyCode.LeftShift)) {
                if(Input.GetKeyDown(KeyCode.Alpha1)) {
                    currentWave += 1;
                    SetNewWave(currentWave);
                }
                /*
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
                */
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
