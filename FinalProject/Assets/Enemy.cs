using System;
using System.Numerics;
using Random = UnityEngine.Random;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using Quaternion = UnityEngine.Quaternion;

public class Enemy : MonoBehaviour
{
    //[Header("Health")]
    public int maxHealth = 3;
    public int currentHealth = 3;
    public float enemySpeed = 3f;

    /*
    [Header("Enemy Types")]
    public GameObject[] tester;
    public enum EnemyType {PAWN};
*/
    void Start()
    {
        
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
}
