using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    public Player player;
    public Transform cameraTransform;
    public GridSelector gridSelector;
    public enum PlayerChooseMove {MOVE, ATTACK};
    public PlayerChooseMove pcm = PlayerChooseMove.MOVE;

    Vector3 selectorPosition = new Vector3(0, 0, 0);

    void Start()
    {
        gridSelector.Disappear();
    }


    void Update()
    {
    }

    public Boolean processKeyClicks(){
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
            player.revertPosition();
        }
        player.MoveWithCC(finalMovement, 1);
        return didMove;
    }

    public void processMouseClicks(){
         if(Input.GetMouseButtonDown(0)){
            Debug.Log("Pressed left-click.");
         } else if(Input.GetMouseButtonDown(1)){
            Debug.Log("Pressed right-click.");
            //returnSelector();
         }
    }
    
    public void processArrowClicks(){
        if(isMoving()){
            return;
        }
        Vector3 selectorMovement = Vector3.zero;
        Vector3 globalBackward = new Vector3(1, 0, 0);
        Vector3 globalRight = new Vector3(0, 0, 1);
        if(Input.GetKeyDown(KeyCode.UpArrow)){
            selectorMovement -= globalBackward;
            gridSelector.MoveAroundPlayer(player.transform.position, selectorMovement, player.playerSpeed); 
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow)){
            selectorMovement -= globalRight;
                gridSelector.MoveAroundPlayer(player.transform.position, selectorMovement, player.playerSpeed);
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow)){
            selectorMovement += globalBackward;
            gridSelector.MoveAroundPlayer(player.transform.position, selectorMovement, player.playerSpeed);
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow)){
            selectorMovement += globalRight;
            gridSelector.MoveAroundPlayer(player.transform.position, selectorMovement, player.playerSpeed);
        }
    }


    public void processNumberClicks(){
        if(Input.GetKeyDown(KeyCode.Alpha1)){
            Debug.Log("Switched to moving");
            pcm = PlayerChooseMove.MOVE;
            gridSelector.Disappear();
        } else if(Input.GetKeyDown(KeyCode.Alpha2)){
            Debug.Log("Switched to attacking");
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
