using Unity.VisualScripting;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    public Player player;
    public Transform cameraTransform;
    public GridSelector gridSelector;

    Vector3 selectorPosition = new Vector3(0, 0, 0);

    void Start()
    {
    }


    void Update()
    {
        if(player.turn == true){
            processKeyClicks();
        }
        processMouseClicks();
        //processArrowClicks();
    }

    void processKeyClicks(){
        Vector3 finalMovement = Vector3.zero;
        Vector3 globalBackward = new Vector3(1, 0, 0);
        Vector3 globalRight = new Vector3(0, 0, 1);

        if(Input.GetKeyDown(KeyCode.W)){
            finalMovement -= globalBackward;
            player.previousDirection = finalMovement;
            player.turn = false;
        }
        else if(Input.GetKeyDown(KeyCode.A)){
            finalMovement -= globalRight;
            player.previousDirection = finalMovement;
            player.turn = false;
        }
        else if(Input.GetKeyDown(KeyCode.S)){
            finalMovement += globalBackward;
            player.previousDirection = finalMovement;
            player.turn = false;
        }
        else if(Input.GetKeyDown(KeyCode.D)){
            finalMovement += globalRight;
            player.previousDirection = finalMovement;
            player.turn = false;
        } else if(Input.GetKeyDown(KeyCode.RightBracket)){
            player.revertPosition();
        }
        player.MoveWithCC(finalMovement);
    }

    void processMouseClicks(){
         if(Input.GetMouseButtonDown(0)){
            Debug.Log("Pressed left-click.");
         } else if(Input.GetMouseButtonDown(1)){
            Debug.Log("Pressed right-click.");
            returnSelector();
         }
    }
    
    void processArrowClicks(){
        Vector3 selectorMovement = Vector3.zero;
        Vector3 globalForward = new Vector3(0, 0, 1);
        Vector3 globalRight = new Vector3(1, 0, 0);

        if(Input.GetKeyDown(KeyCode.UpArrow)){
            selectorMovement += globalForward;
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow)){
            selectorMovement -= globalRight;
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow)){
            selectorMovement -= globalForward;
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow)){
            selectorMovement += globalRight;
        }
        gridSelector.Move(selectorMovement, player.playerSpeed);
    }

    void returnSelector(){
        gridSelector.Teleport(player.transform.position);
    }
}
