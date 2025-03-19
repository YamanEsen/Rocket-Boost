using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] float levelDelay = 2f;
    [SerializeField] AudioClip successSFX;
    [SerializeField] AudioClip crashSFX;
    [SerializeField] ParticleSystem successParticals;
    [SerializeField] ParticleSystem crashParticals;

    AudioSource audioSource;

    bool isControllable = true;
    bool isCollidable = true;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        RespondToDebugKeys();
    }

    void RespondToDebugKeys(){
        if(Keyboard.current.lKey.isPressed){
            LoadNextLevel();
        }
        else if(Keyboard.current.cKey.wasPressedThisFrame){
            isCollidable = !isCollidable;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (!isControllable || !isCollidable){
            return;
        }
        switch ( collision.gameObject.tag){
            case "Friendly":
                Debug.Log("Friendly.....");
                break;
            case "Finish" :
            StartSuccessSequence();
                
                break;
            default:
            StartCrashSequence();
                
                break;
        }
    }

    private void StartSuccessSequence()
    {
        isControllable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(successSFX);
        successParticals.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelDelay);
    }

    void StartCrashSequence()
    {
        isControllable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(crashSFX);
        crashParticals.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel",levelDelay);
    }

    void ReloadLevel(){
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

    void LoadNextLevel(){
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentScene + 1;

        if(nextScene == SceneManager.sceneCountInBuildSettings){
            nextScene = 0;
        }

        SceneManager.LoadScene(nextScene);
    }
    
}
