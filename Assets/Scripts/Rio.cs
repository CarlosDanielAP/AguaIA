using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Rio : MonoBehaviour {

    public int basura;
    public int contaminacion;
    public Text Basura;
    public Text Contaminacion;
    private bool wait;
    private int limpia;
    private bool trabajar;

   void InitRioData()
    {
        contaminacion = 0;
        wait=false;
        trabajar=false;
    }

	// Use this for initialization
	void Start () {
        InitRioData();

        

	}
	
	// Update is called once per frame
	void Update () {
        Basura.text = basura.ToString();
        Contaminacion.text = contaminacion.ToString();
      if(trabajar){
          if(limpia>=0){
              if(wait==false){
                  StartCoroutine(limpiarFunction());
                  limpia--;
              }
          }

      }
		
	}

    public void limpiar(){
        if(basura>0){
        trabajar=false;
        limpia++;
        basura--;
        Debug.Log(limpia);
        trabajar=true;
        }
       
        
    }

      IEnumerator limpiarFunction()
        {
           

            wait=true;
            yield return new WaitForSeconds(1f);
            if(contaminacion>0){
            contaminacion--;}
          
            wait=false;
        }

   
}
