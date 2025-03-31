using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using Quaternion = UnityEngine.Quaternion;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq.Expressions;

public class GameManager : MonoBehaviour
{
    [Header("Environment")]
    public Vector3 globalBackward = new Vector3(1, 0, 0);
    public Vector3 globalRight = new Vector3(0, 0, 1);

    public Camera GameCamera;

    [Header("Level Generation")]
    public int currentLevel = 0;
    public char[,] levelBoard;
    public int boardSizeX;
    public int boardSizeZ;
    public LevelGenerator levelGenerator;
    [Header("Player")]
    public Player player;
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

   

    void Start()
    {
        loadLevel(0);
        playerHealthDisplay.intializeHealthBar(player.maxHealth, PlayerBarPrefab, PlayerBarFaded, PlayerBarActive, player.transform.position);
        initEnemyHealthBarDisplays();
        
    }

    // Update is called once per frame
    void Update()
    {
        playerHealthDisplay.displayHealthBar(player.maxHealth, player.currentHealth/*, player.transform.position*/);
        displayEnemyHealthBars();
    }

    public void loadLevel(int level){
        generateLevelBoard(level);
        levelGenerator.setColors(level);
        levelGenerator.generateLevelTiles(levelBoard, boardSizeX, boardSizeZ);
        enemies = levelGenerator.generateEnemies(levelBoard, boardSizeX, boardSizeZ);
        currentLevel = level;
    }

    public void generateLevelBoard(int level){
        if(level == 1){

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
        }
        levelBoard[0,0] = 'P';
        levelBoard[4,2] = '#';
        levelBoard[4,3] = '#';
        levelBoard[3,2] = 'p';
        levelBoard[2,3] = 'p';
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
        newHealthBarDisplay.intializeHealthBar(enemy.maxHealth, EnemyBarPrefab, EnemyBarFaded, EnemyBarActive, new Vector3(0, 0, 0));
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

}
