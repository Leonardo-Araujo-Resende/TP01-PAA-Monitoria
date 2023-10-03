using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Grid : MonoBehaviour
{   

    
    [SerializeField] private int altura, largura;
    [SerializeField] private GameObject tile;
    private Tile[,] matriz;
    [SerializeField] private Text qntSolucoesText;


    //Variaveis do BackTracking
    public int qntChavesTotal;
    public int[] movimentosBacktrancking;
    private int qntTilesMenorCaminho = 0;
    private int qntMenoresCaminhos = 0;
    private int[,] matrizMenorCaminho;


    void Start(){
        matriz = new Tile[altura, largura];
        matrizMenorCaminho = new int[altura, largura];
        CriarGrid();

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M) && CanAcharCaminho()){
            ResetarMatriz();
            BackTrancking(0,0,0, 1);
        
            MostrarMenorCaminho(); 
            qntSolucoesText.enabled = true;
            qntSolucoesText.text = qntMenoresCaminhos.ToString();
            

        }
            
        


        if(Input.GetKeyDown(KeyCode.R))SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void CriarGrid(){

        for(int j = 0; j > -altura; j --){
            for(int i = 0; i < largura; i++){
                Tile aux = Instantiate(tile, new Vector3(i,j,0), Quaternion.identity).GetComponent<Tile>();
                aux.SetPosicaoMatriz(-j,i);
                matriz[-j,i] = aux;
            }
        }
    }

    
    //-----------------------------------------------------------------------------------------------------------------------------------------------------


    private bool CanAcharCaminho(){
        int qntBau = 0;
        int qntChaves = 0;

        for(int j = 0; j < altura; j ++){
            for(int i = 0; i < largura; i++){
                if(matriz[i,j].tipoTile == 2) qntChaves ++;
                if(matriz[i,j].tipoTile == 3) qntBau ++;
            }
        }

        if(qntBau == 1 && qntChaves >= 1){
            qntChavesTotal = qntChaves;
            return true;
        }
        else return false;
    }

    private bool BackTrancking(int linha, int coluna, int qntChaves, int numTile){


        matriz[linha, coluna].isPercorrido = numTile;
        //matriz[linha, coluna].UpdateTextPercorrido();


        if(matriz[linha,coluna].tipoTile == 2){
            qntChaves ++;
        }

        if(matriz[linha,coluna].tipoTile == 3 && qntChaves == qntChavesTotal){ //Achou caminho

            if(qntTilesMenorCaminho == 0){ //Primeira vez q acha caminho
                qntTilesMenorCaminho = numTile;
                qntMenoresCaminhos = 1;
                VarreMatrizCaminho();
            }
            else if(qntTilesMenorCaminho == numTile) qntMenoresCaminhos ++; //Se achou caminho repetido
            else if(numTile < qntTilesMenorCaminho){ //Se achou outro menor caminho
                qntTilesMenorCaminho = numTile;
                qntMenoresCaminhos = 1;
                VarreMatrizCaminho();
            }

            return false; //Se bau e todas chaves true

        }
        if(matriz[linha,coluna].tipoTile == 3 && qntChaves != qntChavesTotal) return false; // Se bau e n todas chaves false
        
        
        for(int i = 0; i < 4; i++){ //Movimento

            int novaLinha = linha + movimentosBacktrancking[i*2 +0];
            int novaColuna = coluna + movimentosBacktrancking[i*2 +1];

            if(IsDentroMatriz(novaLinha, novaColuna)){ //Dentro Matriz
                if(matriz[novaLinha, novaColuna].tipoTile != 1){ // N for parede
                    if(matriz[novaLinha, novaColuna].isPercorrido == 0){ //N foi percorrido
                        if(BackTrancking(novaLinha, novaColuna, qntChaves, numTile +1)) return false;
                        else{
                            matriz[novaLinha, novaColuna].isPercorrido = 0;
                            //matriz[novaLinha, novaColuna].DesativaTextPercorrido();
                        }
                    }
                }
            }

        }

        

        return false;
    }

    private bool IsDentroMatriz(int linha, int coluna){
        if(linha >= 0 && linha < altura && coluna >= 0 && coluna < largura) return true;
        else return false;
    }

    private void VarreMatrizCaminho(){
        for(int j = 0; j < altura; j ++){
            for(int i = 0; i < largura; i++){
                matrizMenorCaminho[i,j] = matriz[i,j].isPercorrido;
            }
        }
    }

    private void MostrarMenorCaminho(){
        for(int j = 0; j < altura; j ++){
            for(int i = 0; i < largura; i++){
                matriz[i,j].isPercorrido = matrizMenorCaminho[i,j];
                if(matriz[i,j].isPercorrido != 0) matriz[i,j].UpdateTextPercorrido();
                else matriz[i,j].DesativaTextPercorrido();
            }
        }
    }

    private void ResetarMatriz(){
        qntSolucoesText.enabled = false;
        qntTilesMenorCaminho = 0;
        qntMenoresCaminhos = 0;
        for(int j = 0; j < altura; j ++){
            for(int i = 0; i < largura; i++){
                matriz[i,j].isPercorrido = 0;
            }
        }
    }


}


