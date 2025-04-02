using System;
using System.Data;
using System.Threading;
//using Unity.Collections;
using UnityEngine;

using System.Collections;
public class MoveManager : MonoBehaviour
{
    public GameManager gameManager;
    public Enemy currentEnemy;
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

    public void enemyHaveTurn(Enemy enemy){
        currentEnemy = enemy;
        int[] p_coords = findPlayerOnBoard();
        int p_x = p_coords[0];
        int p_z = p_coords[1];
        int[] e_coords = findEnemyOnBoard();
        int e_x = e_coords[0];
        int e_z = e_coords[1];
        //int ret = -1;
        Debug.Log("Player on board at [x,z]: "+ p_x+","+p_z);
        Debug.Log("Enemy on board at [x,z]: "+ e_x+","+e_z);
    }

    
    public Boolean playerWithinEnemyRange(int e_x, int e_z){
        int[] p_coords = findPlayerOnBoard();
        int p_x = p_coords[0];
        int p_z = p_coords[1];
        if(InXLine(e_x, p_x, e_z, p_z, 1)){
            Debug.Log("EyesOnPlayer:ZLine!");
            return true;
        }
        return false;
    }

    public int[] findPlayerOnBoard(){
        Player player = gameManager.player;
        int x = (int) player.transform.position.x / 3;
        int z = (int) player.transform.position.z / 3;
        int[] ret = {x, z};
        return ret;
    }
    public int[] findEnemyOnBoard(){
        int x = (int) currentEnemy.transform.position.x / 3;
        int z = (int) currentEnemy.transform.position.z / 3;
        int[] ret = {x, z};
        return ret;
    }

    public int[,] GetListOfLegalEnemyMoves(int from_x, int from_z){
        int[,] ret = new int[0,2];
        int boardSizeX = gameManager.boardSizeX;
        int boardSizeZ = gameManager.boardSizeZ;
        for(int x = 0; x < boardSizeX; x++){
            for(int z = 0; z < boardSizeZ; z++){
                int[] curr_move = {x, z};

            }
        }
        /*
        List<int> validMoves; 
        validMoves = new List<int>();
        */
        return ret;
    }

    public int[,] addLegalMove(int[,] original, int[] add){
        int size = original.Length;
        int[,] ret = new int[size+1,2];
        for(int i = 0; i < size; i++){
            ret[i,0] = original[i,0];
            ret[i,1] = original[i,1];
        }
        ret[size,0] = add[0];
        ret[size,1] = add[1];
        return ret;
    }

    public int isEnemyMoveLegal(int from_x, int from_z, int to_x, int to_z){
        if(OutOfBounds(to_x, to_z)){
            return -1;
        }
        if(InXLine(from_x, to_x, from_z, to_z, 1) || InZLine(from_x, to_x, from_z, to_z, 1) ){
            if(IsFreeSpace(to_x, to_z)){
                return 0;
            }
        }
        return -1;
    }

}
