using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Usuario : MonoBehaviour {
  static  public bool mandarCamion;
    static public bool regando;
    static public bool pressregando;
    public GameObject Camion;
    
    public GameObject Mercado;
    public GameObject Granjero;
    public Text letreritodeabierto;

    // Use this for initialization
    void Start () {
        regando = false;
        pressregando = true;
        

		
	}
	
	// Update is called once per frame
	void Update () {
        if (Mercado.GetComponent<Mercado>().abierto)
        {
           
            Debug.Log("abierto");
            letreritodeabierto.text = "abierto";
        }
        else
        {
            letreritodeabierto.text = "cerrado";
        }
		
	}

    public void manda()
    {
        if (Camion.GetComponent<Camion>().comidaCamion >= 50)
        {
            mandarCamion = true;
            Debug.Log("mandarcamion");
        }
    }

    public void AbrirAguaGranja()
    {
        if (pressregando)
        {
            Debug.Log("pressregando");
           
          
                pressregando = false;
                Granjero.GetComponent<Granjero>().IrRegar = true;
          
            }
           

        }
    }

