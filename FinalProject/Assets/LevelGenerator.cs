using System.Runtime.InteropServices;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using System;
using System.Collections;

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
    public List<GameObject> tiles;

    [Header("Board")]
    public int boardMinSize;
    public int boardMaxSize;
    public float yPosition = -0.25f;
    public Transform Board;
    public GameObject PillarPrefab;
    public GameObject pillar;

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
        } else if(level == 2){
            tileHue = 0.77f;
            tileSat = 0.9f;
            tileVLight = 1f;
            tileVDark = 0.8f;
        } else if(level == 3){
            tileHue = 0.33f;
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
                    tiles.Add(newTile);
                }
            }
        }
    }

    public List<Enemy> generateEnemies(char[,] levelBoard, int boardSizeX, int boardSizeZ, int enemyHealthMult){
        List<Enemy> enemies = new List<Enemy>();
        for(int x = 0; x < boardSizeX; x++){
            for(int z = 0; z < boardSizeZ; z++){
                if(levelBoard[x,z] == 'p'){
                    int xPosition = 0 + x*tileScale;
                    int zPosition = 0 + z*tileScale;
                    Enemy newEnemy = Instantiate(EnemyPawnPrefab, new Vector3(xPosition, enemyPawnYOffset, zPosition), Quaternion.identity, EnemyPieces).ConvertTo<Enemy>();
                    newEnemy.enemyType = 'p';
                    newEnemy.maxHealth *= enemyHealthMult;
                    newEnemy.currentHealth *= enemyHealthMult;
                    enemies.Add(newEnemy);
                    //Renderer tileRender = newTile.GetComponent<Renderer>();
                    //tileRender.material = TileMaterial;
                    //tileRender.material.color = tileColor;
                }
            }
        }
        return enemies;
    }

    public void generatePillar( int boardSizeX, int boardSizeZ){
        int xPosition = (boardSizeX-1)*tileScale;
        int zPosition = (boardSizeZ-1)*tileScale;
        pillar = Instantiate(PillarPrefab, new Vector3(xPosition, 0, zPosition), Quaternion.identity, Board);
        pillar.transform.rotation = Quaternion.Euler(-90, 0, 0);
    }

    public void removeAllTiles(){
        for(int i = 0; i < tiles.Count; i++){
            GameObject currTile = tiles[i];
            tiles.Remove(currTile);
            Destroy(currTile);
            i--;
        }
    }

    public void removePillar(){
        Destroy(pillar);
    }

}
