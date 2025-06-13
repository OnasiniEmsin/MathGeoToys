using UnityEngine.SceneManagement;
using UnityEngine;

public class Pole : MonoBehaviour
{
    public Transform[] tochki;    //7 мест в action баре
    public int[] employment;         //7 мест для игрушек. 0=пусто, 1=занято
    Figure[] figures;             //немного места для игрушек. 0=пусто, 1=занято
    int allFiguresNumber=0,myFiguresNumber;  //количество игрушек на полу и на баре
    public Spawner spawner;
    void Start()
    {
        figures=new Figure[tochki.Length];
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null)   //Когда мышь касается игрушки
            {
                // Rangni o‘zgartirish
                Figure sr = hit.collider.GetComponent<Figure>();
                if (sr != null)
                {
                    if(sr.ice==false){
                        setDraw(sr); //Игрушка переместилась на барную стойку.
                    }
                }
            }
        }
    }
    public void setDraw(Figure f){
        for(int i=0;i<tochki.Length;i++){
            if(employment[i]==0){
                employment[i]=1;//Игрушка переместилась на барную стойку.
                f.frontEnt(tochki[i].position);
                figures[i]=f;
                areOne();
                return;
            }
        }
    }
    void areOne(){
        int odinakovieIgr=0,idOfArray=0;
        myFiguresNumber=0;
        for(int i=0;i<tochki.Length;i++){ 
            if(employment[i]==1){                    //если это место не пустует
                idOfArray=figures[i].id;
                odinakovieIgr=0;
                for(int j=i;j<tochki.Length;j++){    //Причина, по которой «j» равно «i», заключается в том, что «j» проверяет элементы после «i».
                    if(employment[j]==1){            //если это место не пустует
                        if(figures[j].id==idOfArray){
                            odinakovieIgr++;
                            if(odinakovieIgr==3){
                                destroyFigure(figures[j].id);
                            }
                        }
                    }     
                }
                myFiguresNumber++;
            }
        }
        if(myFiguresNumber>=tochki.Length){
            lose();
        }
    }
    void destroyFigure(int ij){
        for(int j=0;j<figures.Length;j++){
            if(employment[j]==1){
                if(figures[j].id==ij){
                    spawner.deleteElement(figures[j].gameObject);
                    Destroy(figures[j].gameObject);
                    myFiguresNumber=0;
                    employment[j]=0;
                    
                    allFiguresNumber--;
                    if(allFiguresNumber==0){
                        win();
                    }
                }
            }
        }
    }
    public  void setSumma(int summa){
        allFiguresNumber=summa;
    }
    void win(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    void lose(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+2);
    }
    public int getSumma(){
        return allFiguresNumber;
    }
}
