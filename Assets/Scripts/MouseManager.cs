using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MouseManager : MonoBehaviour
{   
    public int tipoTile; // 0 vazio // 1 parede // 2 chave // 3 bau
    private  Tile tileSelecionado;
    [SerializeField] private Sprite[] sprites;

    // Start is called before the first frame update
    void Start()
    {
        sprites[0] = null;
    }

    // Update is called once per frame
    void Update()
    {   
        Moviment();
        if(Input.GetMouseButton(0)) MudarTile();
        Teclas();
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------


    private void OnTriggerStay2D(Collider2D other){
        if(other.CompareTag("Tile")){
            tileSelecionado = other.GetComponent<Tile>();
        }
    }

    void OnTriggerExit2D(Collider2D other){
        if(other.CompareTag("Tile")){
            tileSelecionado = null;
        }
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------
    public void TileVazio(){ tipoTile = 0;}
    public void TileParede(){ tipoTile = 1;}
    public void TileChave(){ tipoTile = 2;}
    public void TileBau(){ tipoTile = 3;}

    //-----------------------------------------------------------------------------------------------------------------------------------------------------
    private void MudarTile(){
        if(tileSelecionado != null){
           tileSelecionado.AlterarTile(sprites[tipoTile], tipoTile);
        }
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------

    private void Moviment(){
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0,0,10);
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------

    private void Teclas(){
        if(Input.GetKeyDown(KeyCode.Alpha1)) TileVazio();
        if(Input.GetKeyDown(KeyCode.Alpha2)) TileParede();
        if(Input.GetKeyDown(KeyCode.Alpha3)) TileChave();
        if(Input.GetKeyDown(KeyCode.Alpha4)) TileBau();
    }
}
