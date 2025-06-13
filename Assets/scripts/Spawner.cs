using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Figure[] figures;
    public Sprite[] sprites;
    public Pole pole;
    int looping=3;
    int shakl,rang,rasm;  //форма, цвет, номер изображения
    Datas[] datas;    //класс находится внизу
    Color[] colors = { Color.red, Color.green, Color.blue, Color.yellow, Color.magenta, Color.cyan };
    int summaFigurih=0;
    List<GameObject> objects;  


    void Start()
    {
        objects=new List<GameObject>();
        StartCoroutine(spawn());
        datas=new Datas[figures.Length*sprites.Length];
        createKomplex();
    }


    void createKomplex(){
        for(int i=0;i<figures.Length;i++){
            for(int j=0;j<sprites.Length;j++){
                Datas d=new Datas();
                d.rang=Random.Range(0,colors.Length);
                d.shakl=i;
                d.rasm=j;
                d.id=i*sprites.Length+j;
                datas[i*sprites.Length+j]=d;
            }
        }
    }


    void sort(){
        Figure figure;
        Datas d;
        for(int i=0;i<datas.Length;i++){
            d=datas[i];
            shakl=d.shakl;
            rang=d.rang;
            rasm=d.rasm;
            figure=Instantiate(figures[shakl],transform.position,transform.rotation).GetComponent<Figure>();
            figure.setImage(sprites[rasm]);
            figure.setColler(colors[rang]);
            figure.id=d.id;
            if(Random.Range(0,10)==7){
                figure.setFreeze();
            }
            summaFigurih++;
            objects.Add(figure.gameObject);
        }
        
    }


    public void sort1(){
        
        foreach(GameObject v in objects){
            if(v.layer!=3){
                v.transform.position=transform.position;  //При нажатии кнопки «Восстановить» все игрушки собираются в одном месте.
            }       
        }
    }


    IEnumerator spawn(){
        while(looping!=0){
            yield return new WaitForSeconds(1);
            looping--;
            sort();
            pole.setSumma(summaFigurih);
        }
    }
    public void deleteElement(GameObject go){
        objects.Remove(go);summaFigurih--;
    }
    
}
class Datas{
    public int rang,shakl,rasm,id;
}