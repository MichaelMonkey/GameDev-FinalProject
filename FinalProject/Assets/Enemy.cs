using System;
using System.Numerics;
using Random = UnityEngine.Random;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using Quaternion = UnityEngine.Quaternion;
/*
using UnityEngine.SceneManagement;
using UnityEditor.Callbacks;
using System.Collections;
using Unity.Cinemachine;
using System.Runtime.CompilerServices;
using System.Linq;
*/
using Unity.VisualScripting;

public class Enemy : MonoBehaviour
{
    //[Header("Health")]
    public int maxHealth = 3;
    public int currentHealth = 3;
    public float enemySpeed = 3f;
    public char enemyType = 'p';

    public int processingCollision = 0;
    public MoveManager moveManager;
    public GameObject temp;
    //public Attack storeAttack;
    /*
    [Header("Enemy Types")]
    public GameObject[] tester;
    public enum EnemyType {PAWN};
*/
    void Start()
    {
        moveManager = GameObject.Find("MoveManager").ConvertTo<MoveManager>();
    }

    void Update()
    {
    }

    public void randomMove(){
        Move(randomDirection());
    }

    public void Move(Vector3 direction){
        transform.position += direction * enemySpeed;
        transform.LookAt(transform.position + direction);
    }

    public Vector3 randomDirection(){
        Vector3 globalBackward = new Vector3(1, 0, 0);
        Vector3 globalRight = new Vector3(0, 0, 1);
        Vector3 retDirection = Vector3.zero;
        int dir = Random.Range(0,4);
        if(dir == 0){
            retDirection -= globalBackward;
        } else if(dir == 1){
            retDirection += globalBackward;
        } else if(dir == 2){
            retDirection -= globalRight;
        } else if(dir == 3){
            retDirection += globalRight;
        }
        return retDirection;
    }


    void OnTriggerEnter(Collider other)
    {
        GameObject touched = other.gameObject;
        //print(other.gameObject.tag);
        if(processingCollision > 0){
            processingCollision = 0;
            //Debug.Log("hit! "+other.gameObject.tag);
            if(touched.CompareTag("AttackBox")){
                Attack attack = touched.transform.parent.ConvertTo<Attack>();
                moveManager.gameManager.attackManager.removeAttack(attack);
                Destroy(attack);
                playHurtSound();
                currentHealth -= attack.damage;
            }
        } else {
            processingCollision += 1;
        }
    }

    public void playHurtSound(){
        AudioSource audioSource = this.GetComponent<AudioSource>();
        audioSource.Play();
    }
}
