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
    public MoveManager moveManager;
    
    [Header("Collectibles and Collisions")]
    public int keysCollected = 0;
    public int processingCollision = 0;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip pickupSound;
    public AudioClip moveSound;
    public AudioClip hurtSound;
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
        //ApplyGravityWithCC();
    }

    public void simpleMove(Vector3 direction){
        moveCamera();
        if(direction == Vector3.zero){
            return;
        }
        CharacterController cc = GetComponent<CharacterController>();
        int gameScale = moveManager.gameManager.gameScale;
        Vector3 checkPosition = transform.position + direction*gameScale;
        Boolean valid = moveManager.validSpace((int)(checkPosition.x/gameScale),(int)(checkPosition.z/gameScale));
        if(valid){
            cc.enabled = false;
            transform.position = checkPosition;
            transform.LookAt(transform.position + direction);
            moveCamera();
            cc.enabled = true;
        }
    }

    public void MoveWithCC(Vector3 direction, int check){
        if(!playing){
            return;
        }
        if(direction != Vector3.zero){
            print(direction);
        }
        CharacterController cc = GetComponent<CharacterController>();
        cc.Move(direction * playerSpeed);
        transform.LookAt(transform.position + direction);
        moveCamera();
        /*if(check == 1){
            Boolean hit = moveManager.checkPlayerMoveAttack();
            if(hit){
                revertPosition();
            }
        }*/
    }

    public void moveCamera(){
        Vector3 cameraLocation = transform.position;
        cameraLocation.x += cameraOffset.x;
        cameraLocation.y += cameraOffset.y;
        cameraLocation.z += cameraOffset.z;
        cameraObject.transform.position = cameraLocation;
    }

    public void revertPosition(){
        MoveWithCC(-1*previousDirection, -1);
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
        GameObject touched = other.gameObject;
        //print(other.gameObject.tag);
        if(processingCollision > 0){
            //Debug.Log("hit! "+other.gameObject.tag);
            if(touched.CompareTag("AttackBox")){
                Attack attack = touched.transform.parent.ConvertTo<Attack>();
                hurtEvent(attack.damage);
                moveManager.gameManager.attackManager.removeAttack(attack);
                Destroy(attack);
                //print("AttackBox removed and destroyed");
            }
            if (touched.CompareTag("Exit")){
                moveManager.gameManager.nextLevel();
            }
            processingCollision = 0;
        } else {
            processingCollision += 1;
        }
        /*if(other.gameObject.CompareTag("Key")){
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
        }*/
        /*
        if(other.gameObject.CompareTag("Wall")){
            wallEvent();
        }
        if(other.gameObject.CompareTag("Exit")){
            StartCoroutine(WinEvent());
        }
        if(other.gameObject.CompareTag("Trap")){
            StartCoroutine(DeathEvent());
        }*/
    }

    void hurtEvent(int damage){
        currentHealth -= damage;
        if(currentHealth <= 0){
            deathEvent();
        } else {
            audioSource.resource = hurtSound;
            audioSource.Play();
        }
    }

    void deathEvent(){

    }

    public void returnToStartPosition(){
        CharacterController cc = GetComponent<CharacterController>();
        cc.enabled = false;
        transform.position = new Vector3(0, 0, 0);
        cc.enabled = true;
    }

/*
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
*/
}
