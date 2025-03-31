using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using Quaternion = UnityEngine.Quaternion;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Vector3 globalBackward = new Vector3(1, 0, 0);
    public Vector3 globalRight = new Vector3(0, 0, 1);

    public Camera GameCamera;
    public GameObject testHealthArea;

    public GameObject HealthBarDisplayPrefab;

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


   

    void Start()
    {
        loadLevel(0);
        playerHealthDisplay.intializeHealthBar(player.maxHealth, PlayerBarPrefab, PlayerBarFaded, PlayerBarActive);
        initEnemyHealthBarDisplays();
        
    }

    // Update is called once per frame
    void Update()
    {
        playerHealthDisplay.displayHealthBar(player.maxHealth, player.currentHealth);
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
    }

    public void initEnemyHealthBarDisplays(){
        enemyCanvases = new List<Canvas>();
        enemyHealthBars = new List<HealthBarDisplay>();
        for(int i = 0; i < enemies.Count; i++){
            addEnemyHealthBarDisplay(enemies[i]);
        }
    }

    public void addEnemyHealthBarDisplay(Enemy enemy){
        Vector3 enemyPosition = enemy.transform.position;
        GameObject newCanvas = Instantiate(enemyHealthCanvasPrefab, enemyPosition, Quaternion.identity, enemy.transform);
        Canvas newEnemyCanvas = newCanvas.ConvertTo<Canvas>();
        newEnemyCanvas.transform.rotation = GameCamera.transform.rotation;
        enemyCanvases.Add(newEnemyCanvas);
        GameObject newBarDisplay = Instantiate(HealthBarDisplayPrefab, new Vector3(enemyPosition.x, enemyPosition.y+2, enemyPosition.z), Quaternion.identity, newEnemyCanvas.transform);
        HealthBarDisplay newHealthBarDisplay = newBarDisplay.ConvertTo<HealthBarDisplay>();
        newHealthBarDisplay.GetComponent<RectTransform>();
        newHealthBarDisplay.intializeHealthBar(enemy.maxHealth, EnemyBarPrefab, EnemyBarFaded, EnemyBarActive);
        newHealthBarDisplay.displayHealthBar(enemy.maxHealth, enemy.currentHealth);
        enemyHealthBars.Add(newHealthBarDisplay);
    }

    public void displayEnemyHealthBars(){
        for(int i = 0; i < enemies.Count; i++){
            enemyHealthBars[i].displayHealthBar(enemies[i].maxHealth, enemies[i].currentHealth);
        }
    }

}
