using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Noria : MonoBehaviour 
{
    public FSM fsm;

    // Variables de bob

    public bool girar;
    public int aguaNivel;
    public GameObject rio;
    public Text Niveldeagua;
    private int basura;
  


    void InitNoriaData()
    {
       
       
        aguaNivel = 100;
        girar = false;
       
    }

	// Use this for initialization
	void Start () 
    {
        InitNoriaData();
      
	}
	
	void Update () 
    {
        basura = rio.GetComponent<Rio>().basura;

        if (!girar)
        {
            StartCoroutine(GirarFunction());


        }
        else
        {
            StopCoroutine(GirarFunction());
        }
        

        Niveldeagua.text = aguaNivel.ToString();
		
	}

    IEnumerator GirarFunction()
    {

        //cada dos segundo giramos la noria 
        girar = true;
        yield return new WaitForSeconds(2f);
       // Debug.Log("girar");

        aguaNivel = 100 - basura;

        girar = false;
    }
}
