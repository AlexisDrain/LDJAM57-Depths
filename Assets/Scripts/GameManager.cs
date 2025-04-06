using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public List<GameObject> waves = new List<GameObject>();
    public static int currentWave = 0;

    public static GameManager myGameManager;
    public static Transform playerTrans;

    public static Image bottomBarFill;
    public static Animator waveTextAnim;

    private static Pool pool_LoudAudioSource;

    public static ParticleSystem particles_Blood;
    public static ParticleSystem particles_BloodAboveWater;
    public static ParticleSystem particles_Water;
    void Awake()
    {
        myGameManager = GetComponent<GameManager>();
        playerTrans = GameObject.Find("Player").transform;

        bottomBarFill = GameObject.Find("BottomBarFill").GetComponent<Image>();
        waveTextAnim = GameObject.Find("WaveText").GetComponent<Animator>();

        pool_LoudAudioSource = transform.Find("Pool_LoudAudioSource").GetComponent<Pool>();

        particles_Blood = transform.Find("Particles_Blood").GetComponent<ParticleSystem>();
        particles_BloodAboveWater = transform.Find("Particles_BloodAboveWater").GetComponent<ParticleSystem>();
        particles_Water = transform.Find("Particles_Water").GetComponent<ParticleSystem>();

        SetNewWave(0);
    }


    public void SetNewWave(int newWaveIndex) {
        for (int i = 0; i < waves.Count; i++) {
            waves[i].SetActive(false);
        }
        waves[newWaveIndex].SetActive(true);
        waveTextAnim.GetComponent<TextMeshProUGUI>().text = $"Wave {newWaveIndex+1} of 10";
        waveTextAnim.SetTrigger("ShowText");
        bottomBarFill.fillAmount = 0;
    }
    /*
    // Update is called once per frame
    void Update()
    {
        
    }
    */

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
