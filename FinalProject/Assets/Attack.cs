using System.Data;
using System.Security.Cryptography;
using UnityEngine;
using System.Collections;using System.Numerics;
using Unity.VisualScripting;
using Vector3 = UnityEngine.Vector3;
using Quaternion = UnityEngine.Quaternion;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq.Expressions;
using System;
using UnityEditor.Rendering.Universal;

public class Attack : MonoBehaviour
{
    [Header("Box Prefabs")]
    public GameObject WarningBoxPrefab;
    public GameObject AttackBoxPrefab;
    
    [Header("Attack Data")]
    public int counter = 0;
    public int duration = 2;
    public int warning = 0;
    public int travel = 0;
    public int damage = 0;
    int buffer = 1;
    public Vector3 location;
    public int direction = -1;
    public int[][] sides;

    [Header("Other")]
    public int xRest = 9;
    public int zRest = 9;
    public GameObject CurrentBox;
    public int gameScale = 3;
    public List<GameObject> currSpaces = new List<GameObject>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public Boolean updateAttack(){
        destoryCurrSpaces();
        CurrentBox = AttackBoxPrefab;
        if(warning > 0){
            CurrentBox = WarningBoxPrefab;
            warning--;
        } else if (buffer > 0){
            buffer--;
        }else if (travel > 0){
            location += gameScale * getBoardDirection(direction);
            travel--;
        } else if (duration > 0){
            duration--;
        } else {
            return false;
        }
        establishSpaces();
        return true;
        /*else if(travel > 0){

        }*/
    }

    public void establishSpaces(){
        List<Vector3> spaces = getAllSpaces();
        for(int i = 0; i < spaces.Count; i++){
            Vector3 spaceLocation = spaces[i];
            GameObject space = Instantiate(CurrentBox, spaceLocation, Quaternion.identity, this.transform);
            currSpaces.Add(space);
        }
    }

    public List<Vector3> getAllSpaces(){
        List<Vector3> ret = new List<Vector3>();
        ret.Add(location);
        return ret;
    }

    public void destoryCurrSpaces(){
        for(int i = 0; i < currSpaces.Count; i++){
            GameObject space = currSpaces[i];
            currSpaces.Remove(space);
            Destroy(space);
            i--;
        }
    }

    public void setup(Vector3 location, int direction, int duration, int warning, int travel, int damage){
        this.location = location;
        this.direction = direction;
        this.duration = duration;
        this.warning = warning;
        this.travel = travel;
        this.damage = damage;
    }

}
