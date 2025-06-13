using System.Collections;
using UnityEngine;

public class Figure : MonoBehaviour
{
    public SpriteRenderer srend;
    public SpriteRenderer me;
    public int id,timeOfMelt=50;
    public bool ice;
    public GameObject iceprefab;


    void Start()
    {
        me=GetComponent<SpriteRenderer>();
    }


    public void setImage(Sprite s){
        srend.sprite=s;
    }


    public void setColler(Color s){
        me.color=s;
    }


    public void frontEnt(Vector2 t){
        srend.sortingOrder+=15;me.sortingOrder+=15;
        setLayer();
        transform.position=t;
        Destroy(GetComponent<Rigidbody2D>());   //чтобы игрушка не упала
    }


    public void setLayer(){
        gameObject.layer=3;
    }


    public void setFreeze(){
        iceprefab.active=true;
        StartCoroutine(melting());
        ice=true;
    }


    IEnumerator melting(){
        
        yield return new WaitForSeconds(timeOfMelt);//Лед растает в эту секунду.
        ice=false;
        iceprefab.active=false;
        
    }

}
