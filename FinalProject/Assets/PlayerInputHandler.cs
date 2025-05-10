using System;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    public Player player;
    public Transform cameraTransform;
    public GridSelector gridSelector;
    public enum PlayerChooseMove {MOVE, ATTACK};
    public PlayerChooseMove pcm = PlayerChooseMove.MOVE;

    int setDirection = -1;
    Vector3 selectorPosition = new Vector3(0, 0, 0);

    void Start()
    {
        gridSelector.Disappear();
    }


    void Update()
    {
    }

    public Boolean processMovementClicks(){
        if(isAttacking()){
            return false;
        }
        Boolean didMove = false;
        Vector3 finalMovement = Vector3.zero;
        Vector3 globalBackward = new Vector3(1, 0, 0);
        Vector3 globalRight = new Vector3(0, 0, 1);

        if(Input.GetKeyDown(KeyCode.W)){
            finalMovement -= globalBackward;
            player.previousDirection = finalMovement;
            didMove = true;
            gridSelector.Disappear();
        }
        else if(Input.GetKeyDown(KeyCode.A)){
            finalMovement -= globalRight;
            player.previousDirection = finalMovement;
            didMove = true;
            gridSelector.Disappear();
        }
        else if(Input.GetKeyDown(KeyCode.S)){
            finalMovement += globalBackward;
            player.previousDirection = finalMovement;
            didMove = true;
            gridSelector.Disappear();
        }
        else if(Input.GetKeyDown(KeyCode.D)){
            finalMovement += globalRight;
            player.previousDirection = finalMovement;
            didMove = true;
            gridSelector.Disappear();
        } else if(Input.GetKeyDown(KeyCode.RightBracket)){
            //player.revertPosition();
            player.returnToStartPosition();
        }
        player.simpleMove(finalMovement);
        //player.MoveWithCC(finalMovement, 1);
        return didMove;
    }

    public void processMouseClicks(){
         if(Input.GetMouseButtonDown(0)){
         } else if(Input.GetMouseButtonDown(1)){
            //returnSelector();
         }
    }
    
    public int processAttackClicks(){
        if(isMoving()){
            return -1;
        }
        int direction = -1;
        Vector3 selectorMovement = Vector3.zero;
        Vector3 globalBackward = new Vector3(1, 0, 0);
        Vector3 globalRight = new Vector3(0, 0, 1);
        
        if(Input.GetKeyDown(KeyCode.W)){
            selectorMovement -= globalBackward;
            gridSelector.MoveAroundPlayer(player.transform.position, selectorMovement, player.playerSpeed); 
            setDirection = 1;
            player.moveManager.gameManager.soundBox.playSystem(0);
        }
        else if(Input.GetKeyDown(KeyCode.A)){
            selectorMovement -= globalRight;
            gridSelector.MoveAroundPlayer(player.transform.position, selectorMovement, player.playerSpeed);
            setDirection = 4;
            player.moveManager.gameManager.soundBox.playSystem(0);
        }
        else if(Input.GetKeyDown(KeyCode.S)){
            selectorMovement += globalBackward;
            gridSelector.MoveAroundPlayer(player.transform.position, selectorMovement, player.playerSpeed);
            setDirection = 3;
            player.moveManager.gameManager.soundBox.playSystem(0);
        }
        else if(Input.GetKeyDown(KeyCode.D)){
            selectorMovement += globalRight;
            gridSelector.MoveAroundPlayer(player.transform.position, selectorMovement, player.playerSpeed);
            setDirection = 2;
            player.moveManager.gameManager.soundBox.playSystem(0);
        } else if (Input.GetKeyDown(KeyCode.Space)){
            if(setDirection != -1){
                direction = setDirection;
                //gridSelector.Disappear();
                setDirection = -1;
                player.moveManager.gameManager.soundBox.playSystem(1);
            }
        }
        return direction;
    }


    public void processNumberClicks(){
        if(Input.GetKeyDown(KeyCode.Alpha1)){
            player.moveManager.gameManager.soundBox.playSystem(0);
            //Debug.Log("Switched to moving");
            pcm = PlayerChooseMove.MOVE;
            gridSelector.Disappear();
        } else if(Input.GetKeyDown(KeyCode.Alpha2)){
            player.moveManager.gameManager.soundBox.playSystem(0);
            //Debug.Log("Switched to attacking");
            pcm = PlayerChooseMove.ATTACK;
            gridSelector.Teleport(player.transform.position);
        } 
    }

    public Boolean isMoving(){
        return pcm == PlayerChooseMove.MOVE;
    }
    public Boolean isAttacking(){
        return pcm == PlayerChooseMove.ATTACK;
    }

    void returnSelector(){
        gridSelector.Teleport(player.transform.position);
    }
}
