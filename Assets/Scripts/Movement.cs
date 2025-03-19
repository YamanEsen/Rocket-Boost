using System.Numerics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
   [SerializeField] InputAction thrust;
   [SerializeField] InputAction rotation;
   [SerializeField] float thrustStrength = 100f;
   [SerializeField] float rotationStrength = 100f;
   [SerializeField] AudioClip mainEngineSFX;
   [SerializeField] ParticleSystem mainEngineParticals;
   [SerializeField] ParticleSystem rightThrustParticals;
   [SerializeField] ParticleSystem leftThrustParticals;

   Rigidbody rb;
   
   AudioSource audioSource;

   
   private void Start()
   {
       rb = GetComponent<Rigidbody>();
       audioSource = GetComponent<AudioSource>();
   }
  
   private void OnEnable()
   {
        rotation.Enable();
        thrust.Enable();
   }

    private void FixedUpdate()
    {
       ProcessThrust();
       ProcessRotation();
    }

    private void ProcessThrust(){
        if(thrust.IsPressed()){
            rb.AddRelativeForce(UnityEngine.Vector3.up * thrustStrength * Time.fixedDeltaTime);
            if(!audioSource.isPlaying){
                audioSource.PlayOneShot(mainEngineSFX);
            }
            if(!mainEngineParticals.isPlaying){
                mainEngineParticals.Play();
            }
            
       }
       else{
            audioSource.Stop();
            mainEngineParticals.Stop();
       }
    }

    private void ProcessRotation(){
        float rotationInput = rotation.ReadValue<float>();
        if(rotationInput < 0){
            ApplyRotation(rotationStrength);
            if(!rightThrustParticals.isPlaying){
                leftThrustParticals.Stop();
                rightThrustParticals.Play();
            }
        }
        else if(rotationInput > 0){
            ApplyRotation(-rotationStrength);
            if(!leftThrustParticals.isPlaying){
                rightThrustParticals.Stop();
                leftThrustParticals.Play();
            }
        }
        else{
            rightThrustParticals.Stop();
            leftThrustParticals.Stop();
        }
    }

    private void ApplyRotation(float rotationThisFrame){
        rb.freezeRotation = true;
        transform.Rotate(UnityEngine.Vector3.forward * rotationThisFrame * Time.fixedDeltaTime);
        rb.freezeRotation = false;
    }
    
}
