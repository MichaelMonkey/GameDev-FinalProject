using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using Quaternion = UnityEngine.Quaternion;
using UnityEngine.SceneManagement;
using UnityEditor.Callbacks;
using System.Collections;
using System;
using Unity.Cinemachine;
using System.Runtime.CompilerServices;
using System.Linq;
using Unity.VisualScripting;

public class Player : MonoBehaviour
{
    [Header("Health")]
    public int maxHealth = 10;
    public int currentHealth = 10;

    [Header("Movement")]
    public float playerSpeed;
    public float gravity = -9.8f;
    public Vector3 previousDirection = new Vector3(0, 0, 0);
    
    [Header("Keys")]
    public int keysCollected = 0;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip pickupSound;
    public AudioClip bumpSound;
    public AudioClip deathSound;
    public AudioClip winSound;

    [Header("Camera")]
    public Camera cameraObject;
    public Vector3 cameraOffset = new Vector3(6, 12, 0);
    Boolean playing = true;
    public Boolean turn = true;

    void Start()
    {
    }

    void Update()
    {
        ApplyGravityWithCC();
    }

    public void MoveWithCC(Vector3 direction){
        if(!playing){
            return;
        }
        CharacterController cc = GetComponent<CharacterController>();
        cc.Move(direction * playerSpeed);
        transform.LookAt(transform.position + direction);
        moveCamera();
    }

    public void moveCamera(){
        Vector3 cameraLocation = transform.position;
        cameraLocation.x += cameraOffset.x;
        cameraLocation.y += cameraOffset.y;
        cameraLocation.z += cameraOffset.z;
        cameraObject.transform.position = cameraLocation;
    }

    public void revertPosition(){
        MoveWithCC(-1*previousDirection);
        previousDirection = new Vector3(0, 0, 0);
    }

    Vector3 gravityVelocity = Vector3.zero;
    public void ApplyGravityWithCC(){
        CharacterController cc = GetComponent<CharacterController>();
        if(cc.isGrounded){
            gravityVelocity = Vector3.zero;
            return;
        }
        gravityVelocity.y += gravity * Time.deltaTime;
        cc.Move(gravityVelocity * Time.deltaTime);
    }
    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("hit! "+other.gameObject.tag);
        if(other.gameObject.CompareTag("Key")){
            pickupEvent();
            Destroy(other.gameObject);
        }
        if(other.gameObject.CompareTag("Gate")){
            if(keysCollected > 0){
                gateEvent();
                Destroy(other.gameObject);
            } else {
                wallEvent();
            }
        }
        if(other.gameObject.CompareTag("Wall")){
            wallEvent();
        }
        if(other.gameObject.CompareTag("Exit")){
            StartCoroutine(WinEvent());
        }
        if(other.gameObject.CompareTag("Trap")){
            StartCoroutine(DeathEvent());
        }
    }

    void pickupEvent(){
        audioSource.resource = pickupSound;
        audioSource.Play();
        keysCollected++;
    }
    void gateEvent(){
        audioSource.resource = pickupSound;
        audioSource.Play();
        keysCollected--;
    }

    void wallEvent(){
        revertPosition();
        audioSource.resource = bumpSound;
        audioSource.Play();
    }

    

    IEnumerator DeathEvent(){
        playing = false;
        audioSource.resource = deathSound;
        audioSource.Play();
        yield return new WaitForSeconds(deathSound.length+0.1f);
        SceneManager.LoadScene("LoseScreen");
    }
    IEnumerator WinEvent(){
        playing = false;
        audioSource.resource = winSound;
        audioSource.Play();
        yield return new WaitForSeconds(winSound.length+0.1f);
        SceneManager.LoadScene("WinScreen");
    }
}
