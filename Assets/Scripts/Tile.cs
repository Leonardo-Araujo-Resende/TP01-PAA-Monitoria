using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{   
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Text percorridoText;
    private Grid gridManager;
    public int tipoTile; // 0 vazio // 1 parede // 2 chave // 3 bau
    public int linha, coluna;
    public int isPercorrido;
 
    // Start is called before the first frame update
    void Start()
    {
        gridManager = GameObject.Find("GameManager").GetComponent<Grid>();
        isPercorrido = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateSprite(Sprite a){
        sprite.sprite = a; 
    }

    public void UpdateTipo(int a){
        tipoTile = a;
    }


    public void AlterarTile(Sprite sprite, int tipo){
        UpdateSprite(sprite);
        UpdateTipo(tipo);

    }

    public void SetPosicaoMatriz(int a, int b){
        linha = a;
        coluna = b;
    }

    public void UpdateTextPercorrido(){
        percorridoText.text = isPercorrido.ToString();
    }

    public void DesativaTextPercorrido(){
        percorridoText.text = "";
    }
}
