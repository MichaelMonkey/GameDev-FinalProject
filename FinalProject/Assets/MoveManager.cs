using System;
using System.Threading;
using UnityEngine;

public class MoveManager : MonoBehaviour
{
    public GameManager gameManager;
    public int gameSize = 3;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int[] GetDirection(int from_x, int from_z, int to_x, int to_z){
        int[] diff = GetDirectional(from_x, from_z, to_x, to_z);
        int dir_x = GiveDifferenceSign(diff[0]);
        int dir_z = GiveDifferenceSign(diff[1]);
        int[] ret = {dir_x, dir_z};
        return ret;
    }

    public int[] GetDirectional(int from_x, int from_z, int to_x, int to_z){
        int diff_x = to_x - from_x;
        int diff_z = to_z - from_z;
        int[] ret = {diff_x, diff_z};
        return ret;
    }

    public int GiveDifferenceSign(int num){
        if(num == 0){
            return 0;
        } else if(num < 0){
            return -1;
        } else {
            return 1;
        }
    }

    public Boolean OutOfBounds(int x, int z){
        int boardSizeX = gameManager.boardSizeX;
        int boardSizeZ = gameManager.boardSizeZ;
        if((x < 0) || (x >= boardSizeX)){
            return true;
        }
        if((x < 0) || (x >= boardSizeZ)){
            return true;
        }
        return false;
    }


    public Boolean IsFreeSpace(int x, int z){
        char[,] board = gameManager.levelBoard;
        if(board[x, z] != '.'){
            return false;
        }
        return true;
    }

    public Boolean IsEnemy(int x, int z){
        char[,] board = gameManager.levelBoard;
        if(board[x, z] != '.'){
            if(board[x, z] != 'P'){
                if(board[x, z] != '#'){
                    return true;
                }
            }
        }
        return false;
    }

    public Boolean OnXRange(int from_x, int to_x, int range, int restrict){
        Boolean forwardCheck = (to_x == from_x - range);
        Boolean backwardCheck = (to_x == from_x + range);
        if(restrict == 1){
            return forwardCheck;
        } else if (restrict == -1){
            return backwardCheck;
        } else {
            return (forwardCheck || backwardCheck);
        }
    }

    public Boolean OnZRange(int from_z, int to_z, int range, int restrict){
        Boolean leftCheck = (to_z == from_z - range);
        Boolean rightCheck = (to_z == from_z + range);
        if(restrict == 1){
            return leftCheck;
        } else if (restrict == -1){
            return rightCheck;
        } else {
            return (leftCheck || rightCheck);
        }
    }

    public Boolean InXLine(int from_x, int to_x, int from_z, int to_z, int range){
        Boolean ret = false;
        for(int z = 1; z < 1+range; z++){
            if(OnZRange(from_z, to_z, z, 0) && OnXRange(from_x, to_x, 0, 0)){
                ret = true;
            }
        }
        return ret;
    }

    public Boolean InZLine(int from_x, int to_x, int from_z, int to_z, int range){
        Boolean ret = false;
        for(int x = 1; x < 1+range; x++){
            if(OnZRange(from_z, to_z, 0, 0) && OnXRange(from_x, to_x, x, 0)){
                ret = true;
            }
        }
        return ret;
    }

    public Boolean playerWithinEnemyRange(int e_x, int e_z){
        Player player = gameManager.player;
        int p_x = (int) player.transform.position.x / 3;
        int p_z = (int) player.transform.position.z / 3;
        if(InXLine(e_x, p_x, e_z, p_z, 1)){
            Debug.Log("EyesOnPlayer:ZLine!");
            return true;
        }
        return false;
    }
}
