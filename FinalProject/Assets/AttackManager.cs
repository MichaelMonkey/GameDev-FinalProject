using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using Quaternion = UnityEngine.Quaternion;
using System;
using Unity.VisualScripting;
public class AttackManager : MonoBehaviour
{
    public Attack DefaultAttackPrefab;
    public int gameScale = 3;

    [Header("Attack Lists")]
    public List<Attack> playerAttacks;
    public List<Attack> enemyAttacks;

    [Header("Attack Groupings")]
    public GameObject PlayerAttacks;
    public GameObject EnemyAttacks;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerAttacks = this.transform.GetChild(0).gameObject;
        EnemyAttacks = this.transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addNewAttack(Vector3 location, int direction, char source){
        GameObject parent = EnemyAttacks;
        List<Attack> parentList = enemyAttacks;
        if(source == 'P'){
            parent = PlayerAttacks;
            parentList = playerAttacks;
        }
        Vector3 startLocation = location + (gameScale * getBoardDirection(direction));
        Attack newAttack = Instantiate(DefaultAttackPrefab, startLocation, Quaternion.identity, parent.transform);
        newAttack.setup(startLocation, direction, 2, 1, 1, 2);
        //location, direction, duration, warning, travel, damage
        parentList.Add(newAttack);
    }

    public void removeAttack(Attack attack){
        List<Attack> ownerList = attackOwner(attack);
        if(ownerList.Count == 0){
            return;
        }
        int index = -1;
        for(int i = 0; i < ownerList.Count; i++){
            if(ownerList[i] == attack){
                index = i;
                //print("Found in list");
            }
        }
        Attack attackRemoving = ownerList[index];
        attackRemoving.destoryCurrSpaces();
        //Attack attackRemoving = ownerList.Find(attack); 
        ownerList.Remove(attackRemoving);
        Destroy(attackRemoving);
    }

    public List<Attack> attackOwner(Attack attack){
        int ownerInt = -1;
        if(playerAttacks.Contains(attack)){
            ownerInt = 0;
        } else if(enemyAttacks.Contains(attack)){
            ownerInt = 1;
        }
        if(ownerInt == 0){
            return playerAttacks;
        } else if(ownerInt == 1){
            return enemyAttacks;
        } else {
            return new List<Attack>();;
        }
    }

    public void updatePlayerAttacks(){
        for(int i = 0; i < playerAttacks.Count; i++){
            Attack currAttack = playerAttacks[i];
            Boolean success = currAttack.updateAttack();
            if(success == false){
                playerAttacks.Remove(currAttack);
                Destroy(currAttack);
                i--;
            }
        }
    }

    public Vector3 getBoardDirection(int direction){
        Vector3 ret;
        if(direction == 1){
            ret = new Vector3(-1, 0, 0);
        } else if (direction == 2){
            ret = new Vector3(0, 0, 1);
        } else if (direction == 3){
            ret = new Vector3(1, 0, 0);
        } else {
            ret = new Vector3(0, 0, -1);
        }
        return ret;
    }
}
