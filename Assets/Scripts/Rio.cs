using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Rio : MonoBehaviour {

    public int basura;
    public int contaminacion;
    public Text Basura;
    public Text Contaminacion;

   void InitRioData()
    {
        contaminacion = 50;
    }

	// Use this for initialization
	void Start () {
        InitRioData();

        

	}
	
	// Update is called once per frame
	void Update () {
        Basura.text = basura.ToString();
        Contaminacion.text = contaminacion.ToString();
		
	}

   
}
