using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using UnityEngine.UI;

public class GridSelector : MonoBehaviour
{
    [Header("Materials")]
    public Material solidMaterial;
    public Material transMaterial;
    [Header("Colors")]
    public Color[] colors = {Color.HSVToRGB(0, 1, 1)};
    public float solid = 0.75f;
    public float transparent = 0.1f;


    void Start()
    {
        setColor(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move(Vector3 direction, float speed){
        transform.position += direction*speed;
    }

    public void MoveAroundPlayer(Vector3 position, Vector3 direction, float speed){
        position += direction*speed;
        Teleport(position);
    }

    public void Teleport(Vector3 position){
        transform.position = position;
    }

    public void Disappear(){
        Teleport(new Vector3(-100, 0, -100));
    }

    public void setColor(int set){
        if(set > colors.Length){
            return;
        }
        Color colorSolid = colors[set];
        colorSolid.a = solid;
        Color colorTrans = colors[set];
        colorTrans.a = transparent;
        solidMaterial.color = colorSolid;
        transMaterial.color = colorTrans;
    }
}
