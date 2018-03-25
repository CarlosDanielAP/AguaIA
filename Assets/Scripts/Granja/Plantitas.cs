using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plantitas : MonoBehaviour
{
    public int agua;
    public int vida;
    public GameObject granjero;

    public bool arar;
    public bool cosechar;
    public bool listoParaCrecer;
    
    static public int tiempoArado;
   

    public bool sepuedearar;
    public bool sepuedecosechar;

    private bool abierto;
    private bool pause;

    private Coroutine RegarCoroutine;

    float time;


    // Use this for initialization
    void Start()
    {
        
        arar = false;
        cosechar = false;
        listoParaCrecer = false;
        tiempoArado = 5;

        sepuedearar = true;
        sepuedecosechar = false;
        abierto = false;
    }

    // Update is called once per frame
    void Update()
    {
       
        //Debug.Log(time);

        // StopCoroutine(RegarCoroutine);
        if (arar)
              {
            //Debug.Log("haaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
                  StartCoroutine(ArarFunction());
              }
        if (cosechar)
        {
            StartCoroutine(CosecharFunction());
        }
            
   

        if (Usuario.regando)
        {
            if (listoParaCrecer)
            {
                
                if (!abierto)
                {
                    Debug.Log("creciendo");

                    StartCoroutine(CrecerFunction(10-time));
                    

                    // StartCoroutine(CrecerFunction());
                }
                if (abierto)
                {
                    time += Time.deltaTime;
                }
            }
        }

        if (!Usuario.regando)
        {
            if (listoParaCrecer)
            {

               
                //abierto = true;
                StopAllCoroutines();
                Debug.Log("pausa");
                abierto = false;
               
            }

        }

        if (sepuedecosechar)
        {
            if (Usuario.regando)
            {
                time += Time.deltaTime;
            }

            if (time >= 15)
            {
                Debug.Log("plantitas muertas che wey");
                sepuedecosechar = false;
                sepuedearar = true;
            }
        }




        }

    IEnumerator ArarFunction()
    {


        Debug.Log("arando ando...");
        yield return new WaitForSeconds(tiempoArado);

        Debug.Log("listoparacrecer");
        listoParaCrecer = true;
        arar = false;
        time = 0;
    }
    IEnumerator CrecerFunction(float tiempo)
    {
        

        abierto = true;
        Debug.Log("creciendoPlantitas");
        yield return new WaitForSeconds(tiempo);
        abierto = false;
        Debug.Log("plantitas listas para ser cosechadas");
        //time = 0;
        sepuedecosechar = true;
        listoParaCrecer = false;
      
    }
    IEnumerator CosecharFunction()
    {


        Debug.Log("cosechandoando ando...");
        yield return new WaitForSeconds(tiempoArado);

        Debug.Log("listopara ser arada de nuevo");
        cosechar = false;
      
        
    }



    public void seleccion()
    {
        if (sepuedearar)
        {
            
            
            if (Usuario.pressregando)
            {
                if (!Usuario.regando)
                {
                    Debug.Log("estoy siendo arado");
                    arar = true;
                    sepuedearar = false;
                }
               
            }

        }
        if (sepuedecosechar)
        {
            if (!Usuario.regando)
            {
                Debug.Log("estoy siendo cosechado");
                cosechar = true;
                sepuedecosechar = false;
            }
        }
      
    }
}
