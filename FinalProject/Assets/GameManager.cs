using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using Quaternion = UnityEngine.Quaternion;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq.Expressions;
using System;
using System.Collections;
using UnityEditor.Rendering.Universal;

public class GameManager : MonoBehaviour
{
    [Header("Environment")]
    public Vector3 globalBackward = new Vector3(1, 0, 0);
    public Vector3 globalRight = new Vector3(0, 0, 1);
    public int gameScale = 3;

    public Camera GameCamera;

    [Header("Turns and Moves")]
    public Boolean playerTurn;
    public Boolean playerAttackTurn;
    public Boolean enemyTurn;
    public Boolean enemyAttackTurn;
    public float turnStaggerTime = 2f;
    public MoveManager moveManager;

    [Header("Level Generation")]
    public int currentLevel = 0;
    public char[,] levelBoard;
    public int boardSizeX;
    public int boardSizeZ;
    public LevelGenerator levelGenerator;
    [Header("Player")]
    public Player player;
    public PlayerInputHandler playerInputHandler;
    public HealthBarDisplay playerHealthDisplay;
    public GameObject PlayerBarPrefab;
    public Color PlayerBarFaded = Color.gray;
    public Color PlayerBarActive = Color.green;

    [Header("Enemy")]
    public int enemyCount = 0;
    public GameObject enemyHealthCanvasPrefab;
    public GameObject EnemyBarPrefab;
    public Color EnemyBarFaded = Color.gray;
    public Color EnemyBarActive = Color.red;
    
    public List<Enemy> enemies;
    public List<HealthBarDisplay> enemyHealthBars;
    public List<Canvas> enemyCanvases;

    [Header("Health Bars")]
    public GameObject HealthBarDisplayPrefab;
    public float overheadOffset = 0.5f;

    [Header("Attacks")]
    public AttackManager attackManager;

    [Header("Data")]
    public PlayerPrefsLoader playerPrefsLoader;
    public int playerHealthMult;
    public int playerWarning;
    public int enemyHealthMult;
    public int enemyWarning;

    public SoundBox soundBox;

    void Start()
    {
        playerHealthMult = playerPrefsLoader.playerHealthMult;
        playerWarning = playerPrefsLoader.playerWarning;
        enemyHealthMult = playerPrefsLoader.enemyHealthMult;
        enemyWarning = playerPrefsLoader.enemyWarning;
        attackManager.playerWarning = playerWarning;
        attackManager.enemyWarning = enemyWarning;
        player.maxHealth *= playerHealthMult;
        loadLevel(0);
        playerHealthDisplay.intializeHealthBar(player.maxHealth, PlayerBarPrefab, PlayerBarFaded, PlayerBarActive, player.transform.position, playerHealthMult);
    }

    // Update is called once per frame
    void Update()
    {
        playerHealthDisplay.displayHealthBar(player.maxHealth, player.currentHealth/*, player.transform.position*/);
        displayEnemyHealthBars();
        doPlayerTurn();
        doPlayerAttackTurn();
        doEnemyTurn();
        doEnemyAttackTurn();
        considerRestart();
        removeDeadEnemies();
    }

    public void loadLevel(int level){
        //soundBox.stopMusic();
        player.returnToStartPosition();
        player.currentHealth = player.maxHealth;
        playerTurn = true;
        playerAttackTurn = false;
        enemyTurn = false;
        enemyAttackTurn = false;
        generateLevelBoard(level);
        levelGenerator.setColors(level);
        levelGenerator.generateLevelTiles(levelBoard, boardSizeX, boardSizeZ);
        enemies = levelGenerator.generateEnemies(levelBoard, boardSizeX, boardSizeZ, enemyHealthMult, level);
        levelGenerator.generatePillar(boardSizeX, boardSizeZ);
        levelGenerator.generateAuras(levelBoard, boardSizeX, boardSizeZ);
        initEnemyHealthBarDisplays();
        currentLevel = level;
        soundBox.startMusic();
    }



    public void generateLevelBoard(int level){
        if(level == 1){
            boardSizeX = 6;
            boardSizeZ = 10;
            char[,] board = new char[boardSizeX, boardSizeZ]; 
            for(int x = 0; x < boardSizeX; x++){
                for(int z = 0; z < boardSizeZ; z++){
                    board[x,z] = '.';
                }
            }
            levelBoard = board;
            levelBoard[0,0] = 'P';
            levelBoard[4,2] = '#';
            levelBoard[4,3] = '#';
            levelBoard[4,4] = '#';
            levelBoard[4,5] = '#';
            levelBoard[4,6] = '#';
            levelBoard[4,7] = '#';
            //levelBoard[3,2] = 'p';
            levelBoard[2,3] = 'p';
            levelBoard[3,2] = 'p';
            levelBoard[5,8] = 'p';
            levelBoard[4,9] = 'p';
            levelBoard[0,7] = '+';
            levelBoard[1,8] = '+';
            levelBoard[1,9] = '+';
        } else if (level == 2) {
            boardSizeX = 8;
            boardSizeZ = 3;
            char[,] board = new char[boardSizeX, boardSizeZ]; 
            for(int x = 0; x < boardSizeX; x++){
                for(int z = 0; z < boardSizeZ; z++){
                    board[x,z] = '.';
                }
            }
            levelBoard = board;
            levelBoard[0,0] = 'P';
            levelBoard[0,1] = '#';
            levelBoard[1,1] = '#';
            levelBoard[2,1] = '#';
            levelBoard[1,1] = '#';
            levelBoard[4,1] = '#';
            levelBoard[5,1] = '#';
            levelBoard[7,1] = '#';
            levelBoard[7,2] = 'p';
            levelBoard[6,1] = 'p';
            levelBoard[5,1] = '+';
            levelBoard[3,1] = '+';
        }else if(level == 3){
            boardSizeX = 5;
            boardSizeZ = 5;
            char[,] board = new char[boardSizeX, boardSizeZ]; 
            for(int x = 0; x < boardSizeX; x++){
                for(int z = 0; z < boardSizeZ; z++){
                    board[x,z] = '.';
                }
            }
            levelBoard = board;
            levelBoard[0,0] = 'P';
            levelBoard[1,1] = '#';
            levelBoard[1,2] = '#';
            levelBoard[3,3] = 'p';
            levelBoard[4,0] = 'p';
            levelBoard[4,3] = 'p';
            levelBoard[2,2] = '+';
        } else {
            boardSizeX = 6;
            boardSizeZ = 4;
            char[,] board = new char[boardSizeX, boardSizeZ]; 
            for(int x = 0; x < boardSizeX; x++){
                for(int z = 0; z < boardSizeZ; z++){
                    board[x,z] = '.';
                }
            }
            levelBoard = board;
            levelBoard[0,0] = 'P';
            levelBoard[4,2] = '#';
            levelBoard[4,3] = '#';
            levelBoard[2,3] = 'p';
            levelBoard[3,2] = 'p';
            levelBoard[3,3] = 'p';
            levelBoard[5,2] = '+';
        }
        
    }

    public void initEnemyHealthBarDisplays(){
        enemyCanvases = new List<Canvas>();
        enemyHealthBars = new List<HealthBarDisplay>();
        //addEnemyHealthBarDisplay(testEnemy);
        for(int i = 0; i < enemies.Count; i++){
            addEnemyHealthBarDisplay(enemies[i]);
        }
        /*for(int i = 0; i < enemies.Count; i++){
            addEnemyHealthBarDisplay(enemies[i]);
        }*/
    }

    public void addEnemyHealthBarDisplay(Enemy enemy){
        Canvas newEnemyCanvas = creatNewCanvas(enemy.transform.position, enemy.transform);
        enemyCanvases.Add(newEnemyCanvas);
        GameObject newBarDisplay = Instantiate(HealthBarDisplayPrefab, new Vector3(0, 0, 0), Quaternion.identity, newEnemyCanvas.transform);
        newBarDisplay.transform.localPosition = Vector3.zero;
        HealthBarDisplay newHealthBarDisplay = newBarDisplay.ConvertTo<HealthBarDisplay>();
        newHealthBarDisplay.intializeHealthBar(enemy.maxHealth, EnemyBarPrefab, EnemyBarFaded, EnemyBarActive, new Vector3(0, 0, 0), enemyHealthMult);
        newHealthBarDisplay.transform.localPosition = new Vector3(0, overheadOffset, 0);
        enemyHealthBars.Add(newHealthBarDisplay);
       /* Vector3 enemyPosition = enemy.transform.position;
        Debug.Log(enemyPosition);
        //GameObject newCanvas = Instantiate(enemyHealthCanvasPrefab, enemyPosition, Quaternion.identity, enemy.transform);
        Canvas newEnemyCanvas = testHealthArea;//newCanvas.ConvertTo<Canvas>();
        Vector3 cavnasPosition = newEnemyCanvas.transform.position;
        GameObject newBarDisplay = Instantiate(HealthBarDisplayPrefab, cavnasPosition + new Vector3(0, 2, 0), Quaternion.identity, newEnemyCanvas.transform);
        HealthBarDisplay newHealthBarDisplay = newBarDisplay.ConvertTo<HealthBarDisplay>();
        Debug.Log(newHealthBarDisplay.GetComponent<Transform>().position);
        Debug.Log(newHealthBarDisplay.GetComponent<RectTransform>().position);
        //newEnemyCanvas.transform.rotation = GameCamera.transform.rotation;
        enemyCanvases.Add(newEnemyCanvas);
        newHealthBarDisplay.intializeHealthBar(enemy.maxHealth, EnemyBarPrefab, EnemyBarFaded, EnemyBarActive, 0);
        newHealthBarDisplay.displayHealthBar(enemy.maxHealth, enemy.currentHealth);
        enemyHealthBars.Add(newHealthBarDisplay);
    */
    }

    public Canvas creatNewCanvas(Vector3 position, Transform parent){
        GameObject newCanvas = Instantiate(enemyHealthCanvasPrefab, position, Quaternion.identity, parent);
        Canvas newEnemyCanvas = newCanvas.ConvertTo<Canvas>();
        newEnemyCanvas.transform.localPosition = Vector3.zero;
        return newEnemyCanvas;
    }

    public void displayEnemyHealthBars(){
        //enemyHealthBars[0].displayHealthBar(3,3/*, new Vector3(0, 0, 0)*/);
        for(int i = 0; i < enemyHealthBars.Count; i++){
            Enemy enemy = enemies[i];
            enemyHealthBars[i].displayHealthBar(enemy.maxHealth, enemy.currentHealth);
        }
        //enemyHealthBars[1].displayHealthBar(enemies[0].maxHealth, enemies[0].currentHealth);
        for(int i = 0; i < enemyCanvases.Count; i++){
            enemyCanvases[i].transform.rotation = GameCamera.transform.rotation;
        }
        /*for(int i = 0; i < enemies.Count; i++){
            enemyHealthBars[i].displayHealthBar(enemies[i].maxHealth, enemies[i].currentHealth);
        }*/
    }

    public void doPlayerTurn(){
        if(playerTurn == true){
            playerInputHandler.processNumberClicks();
            //playerInputHandler.processMouseClicks();
            int attackDirection = playerInputHandler.processAttackClicks();
            Boolean didMove = playerInputHandler.processMovementClicks();
            if(didMove){
                StartCoroutine(SwitchTurnStagger());
            }
            if(attackDirection != -1){
                attackManager.addNewAttack(player.transform.position, attackDirection, 'P');
                StartCoroutine(SwitchTurnStagger());
            }
        }
        //playerInputHandler.processMouseClicks();
    }
    public void doPlayerAttackTurn(){
        if(playerAttackTurn == true){
            attackManager.updatePlayerAttacks();
            StartCoroutine(SwitchTurnStagger());
        }
    }

    public void doEnemyTurn(){
        if(enemyTurn == true){
            for(int i = 0; i < enemies.Count; i++){
                moveManager.enemyHaveTurn(enemies[i]);
                //enemies[i].randomMove();
            }
            StartCoroutine(SwitchTurnStagger());
        }
    }

    public void doEnemyAttackTurn(){
        if(enemyAttackTurn == true){
            attackManager.updateEnemyAttacks();
            StartCoroutine(SwitchTurnStagger());
        }
    }

    IEnumerator SwitchTurnStagger(){
        if(playerTurn){
            playerTurn = false;
            yield return new WaitForSeconds(turnStaggerTime);
            playerAttackTurn = true;
        } else if (playerAttackTurn){
            playerAttackTurn = false;
            yield return new WaitForSeconds(turnStaggerTime);
            enemyTurn = true;
        } else if (enemyTurn) {
            enemyTurn = false;
            yield return new WaitForSeconds(turnStaggerTime);
            enemyAttackTurn = true;
        } else if (enemyAttackTurn) {
            enemyAttackTurn = false;
            yield return new WaitForSeconds(turnStaggerTime);
            playerTurn = true;
        }
    }

    public void removeDeadEnemies(){
        for(int i = 0; i < enemies.Count; i++){
            Enemy currEnemy = enemies[i];
            if(currEnemy.currentHealth <= 0){
                Canvas currCanvas = enemyCanvases[i];
                HealthBarDisplay currHealthBar = enemyHealthBars[i];
                enemies.Remove(currEnemy);
                enemyCanvases.Remove(currCanvas);
                enemyHealthBars.Remove(currHealthBar);
                int old_x = (int)(currEnemy.transform.position.x / gameScale);
                int old_z = (int)(currEnemy.transform.position.z / gameScale);
                levelBoard[old_x,old_z] = '.';
                soundBox.playSFX(2);
                Destroy(currEnemy.ConvertTo<GameObject>());
                i--;
            }
        }
    }

    public void removeAllEnemies(){
        for(int i = 0; i < enemies.Count; i++){
            Enemy currEnemy = enemies[i];
            Canvas currCanvas = enemyCanvases[i];
            HealthBarDisplay currHealthBar = enemyHealthBars[i];
            enemies.Remove(currEnemy);
            enemyCanvases.Remove(currCanvas);
            enemyHealthBars.Remove(currHealthBar);
            Destroy(currEnemy.ConvertTo<GameObject>());
            i--;
        }
    }

    public void considerRestart(){
        if((player.currentHealth <= 0) || (player.transform.position.y < -5)){
            soundBox.playSFX(3);
            establishLevel();
            player.moveCamera();
        }
    }

    public void nextLevel(){
        currentLevel += 1;
        establishLevel();
    }

    public void establishLevel(){
        removeAllEnemies();
        levelGenerator.removeAllTiles();
        levelGenerator.removePillar();
        levelGenerator.removeAllAuras();
        loadLevel(currentLevel);
    }

}
