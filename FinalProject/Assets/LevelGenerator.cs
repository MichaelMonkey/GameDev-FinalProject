using System.Runtime.InteropServices;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class LevelGenerator : MonoBehaviour
{

    [Header("Tile")]
    public GameObject TilePrefab;
    public int tileScale = 3;
    public Material TileMaterial;
    public float tileHue = 0.1f;
    private float tileSat = 0.9f;
    private float tileVLight = 1f;
    private float tileVDark = 0.8f;

    [Header("Board")]
    public int boardMinSize;
    public int boardMaxSize;
    public float yPosition = -0.25f;
    public Transform Board;

    [Header("Enemy")]
    public Transform EnemyPieces;
    public GameObject EnemyPawnPrefab;
    public float enemyPawnYOffset = 0f;
    void Start()
    {
    }


    void Update()
    {
        
    }

    public void setColors(int level){
        if(level == 1){
            tileHue = 0.1f;
            tileSat = 0.9f;
            tileVLight = 1f;
            tileVDark = 0.8f;
        } else {
            tileHue = 0f;
            tileSat = 0f;
            tileVLight = 1f;
            tileVDark = 0.2f;
        }
    }

    public void generateLevelTiles(char[,] levelBoard, int boardSizeX, int boardSizeZ){
        Color colorLight = Color.HSVToRGB(tileHue, tileSat, tileVLight);
        Color colorDark = Color.HSVToRGB(tileHue, tileSat, tileVDark);
        Color tileColor;
        for(int x = 0; x < boardSizeX; x++){
            for(int z = 0; z < boardSizeZ; z++){
                if(levelBoard[x,z] != '#'){
                    if((x+z) %2 == 0){
                        tileColor = colorLight;
                    } else {
                        tileColor = colorDark;
                    }
                    int xPosition = 0 + x*tileScale;
                    int zPosition = 0 + z*tileScale;
                    GameObject newTile = Instantiate(TilePrefab, new Vector3(xPosition, yPosition, zPosition), Quaternion.identity, Board);
                    Renderer tileRender = newTile.GetComponent<Renderer>();
                    tileRender.material = TileMaterial;
                    tileRender.material.color = tileColor;
                }
            }
        }
    }

    public List<Enemy> generateEnemies(char[,] levelBoard, int boardSizeX, int boardSizeZ){
        List<Enemy> enemies = new List<Enemy>();
        for(int x = 0; x < boardSizeX; x++){
            for(int z = 0; z < boardSizeZ; z++){
                if(levelBoard[x,z] == 'p'){
                    int xPosition = 0 + x*tileScale;
                    int zPosition = 0 + z*tileScale;
                    GameObject newEnemyPawn = Instantiate(EnemyPawnPrefab, new Vector3(xPosition, enemyPawnYOffset, zPosition), Quaternion.identity, EnemyPieces);
                    enemies.Add(newEnemyPawn.ConvertTo<Enemy>());
                    //Renderer tileRender = newTile.GetComponent<Renderer>();
                    //tileRender.material = TileMaterial;
                    //tileRender.material.color = tileColor;
                }
            }
        }
        return enemies;
    }

}
