using System;
using System.Data;
using System.Threading;
//using Unity.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

using System.Collections;
using Unity.VisualScripting;
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
        int[] ret = new int[] {dir_x, dir_z};
        return ret;
    }

    public int[] GetDirectional(int from_x, int from_z, int to_x, int to_z){
        int diff_x = to_x - from_x;
        int diff_z = to_z - from_z;
        int[] ret = new int[] {diff_x, diff_z};
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
        if(board[x, z] == 'p'){
            return true;
        }
        return false;
    }

    public Boolean IsPlayer(int x, int z){
        int[] p_coords = findPlayerOnBoard();
        int p_x = p_coords[0];
        int p_z = p_coords[1];
        return (p_x == x) && (p_z == z);
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

    public Boolean checkPlayerMoveAttack(){
        int[] p_coords = findPlayerOnBoard();
        int p_x = p_coords[0];
        int p_z = p_coords[1];
        Boolean ret = IsEnemy(p_x, p_z);
        if(ret){
            Debug.Log("Player attacked enemy");
            damageEnemy(findEnemyAt(p_x, p_z), 2);
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
        Debug.Log("Enemy on board at [x,z]: "+ e_x+","+e_z);
        if(playerWithinEnemyRange()){
            Debug.Log("Attacked player on board at [x,z]: "+ p_x+","+ p_z);
            damagePlayer(1);
        }
        //int[] move = new int[] {0, 0};
        /*int[,] attackMoves = getListOfLegalEnemyAttackMoves(e_x, e_z);
        if(attackMoves.Length > 0){
            Debug.Log("Attack move");
            move = pickFromMoveList(attackMoves);
            Debug.Log(move[0]+","+move[1]);
        } else {
            int[,] randomMoves = getListOfRandomMoves();
            if(randomMoves.Length > 0){
                Debug.Log("Random move");
                move = pickFromMoveList(randomMoves);
            Debug.Log(move[0]+","+move[1]);
            }
        }
        if(move == new int[] {0, 0}){
            Debug.Log("No moves");
        }
        */
        //int ret = -1;
    }

    public void damagePlayer(int damage){
        gameManager.player.currentHealth -= damage;
        /*if(gameManager.player.currentHealth <= 0){
            Destroy(gameManager.player);
        }*/
    }

    public Enemy findEnemyAt(int x, int z){
        for(int i = 0; i < gameManager.enemies.Count; i++){
            currentEnemy = gameManager.enemies[i];
            int e_x = (int) currentEnemy.transform.position.x / 3;
            int e_z = (int) currentEnemy.transform.position.z / 3;
            if((e_x == x) && (e_z == z)){
                return currentEnemy;
            }
        }
        return currentEnemy;
    }

    public void damageEnemy(Enemy enemy, int damage){
        enemy.currentHealth -= damage;
        /*if(enemy.currentHealth <= 0){
            Destroy(enemy);
        }*/
    }
    
    public Boolean playerWithinEnemyRange(){
        int[] e_coords = findEnemyOnBoard();
        int e_x = e_coords[0];
        int e_z = e_coords[1];
        int[] p_coords = findPlayerOnBoard();
        int p_x = p_coords[0];
        int p_z = p_coords[1];
        if(InZLine(e_x, p_x, e_z, p_z, 1)){
            Debug.Log("EyesOnPlayer:ZLine!");
            return true;
        } else if (InXLine(e_x, p_x, e_z, p_z, 1)){
            Debug.Log("EyesOnPlayer:XLine!");
            return true;
        }
        return false;
    }

    public int[] findPlayerOnBoard(){
        Player player = gameManager.player;
        int x = (int) player.transform.position.x / 3;
        int z = (int) player.transform.position.z / 3;
        int[] ret = new int[] {x, z};
        return ret;
    }
    public int[] findEnemyOnBoard(){
        int x = (int) currentEnemy.transform.position.x / 3;
        int z = (int) currentEnemy.transform.position.z / 3;
        int[] ret = new int[] {x, z};
        return ret;
    }

    public int[,] getListOfLegalEnemyAttackMoves(int from_x, int from_z){
        int[,] ret = new int[0,2];
        int boardSizeX = gameManager.boardSizeX;
        int boardSizeZ = gameManager.boardSizeZ;
        for(int x = 0; x < boardSizeX; x++){
            for(int z = 0; z < boardSizeZ; z++){
                int[] curr_move = new int[] {x, z};
                if(isEnemyMoveLegal(from_x, from_z, x, z) == 1){
                    Debug.Log("LegalMove: "+x+","+z);
                    ret = addLegalMove(ret, curr_move);
                }
            }
        }
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
            if(IsPlayer(to_x, to_z)){
                return 1;
            }
        }
        return -1;
    }

    public int[] randomMoveDirection(int dir){
        int[] ret = new int[2];
        if(dir == 0){
            ret[0] = -1;
            ret[1] = 0;
        } else if(dir == 1){
            ret[0] = 1;
            ret[1] = 0;
        } else if(dir == 2){
            ret[0] = 0;
            ret[1] = -1;
        } else if(dir == 3){
            ret[0] = 0;
            ret[1] = 1;
        }
        return new int[2];
    }

    /*public int[,] getListOfRandomMoves(){
        int[] e_coords = findEnemyOnBoard();
        int e_x = e_coords[0];
        int e_z = e_coords[1];
        int[,] ret = new int[0,2];
        for(int i = 0; i < 4; i++){
            int[] randomMove = randomMoveDirection(i);
            int x = randomMove[0];
            int z = randomMove[1];
            if(!OutOfBounds(e_x+x, e_z+z)){
                ret = addLegalMove(ret, randomMove);
            }
        }
        return ret;
    }*/

    public int[] pickFromMoveList(int[,] moveList){
        int size = moveList.Length;
        Debug.Log("Size: "+size);
        int[] ret = new int[2];
        if(size <= 0){
            ret[0] = 0;
            ret[1] = 0;
        } else {
            int pick = (int) Random.Range(0, size-1);
            Debug.Log("Pick: "+pick);
            ret[0] = moveList[pick, 0];
            ret[1] = moveList[pick, 1];
        }
        return ret;
    }

}
