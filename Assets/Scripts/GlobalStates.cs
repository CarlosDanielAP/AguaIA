using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GlobalStates
{
   
    public enum GlobalStateID
    {
        abastecer,
        arar,
        cosechar
    }

    public class Abastecer : State


    {
        private Mercado mercado;
        private Camion camion;

        private bool descargando;
        private Coroutine descargaCoroutine;
        private int comidaCamion;
        private int comidaMercado;

        public Abastecer(Camion _camion)
        {
            camion=_camion;
        }

        public Abastecer(Mercado _mercado)
        {
            mercado = _mercado;
        }
        public override void OnEnter(GameObject _object)
        {
          
            if (_object.name == "Camion")
            {
                comidaCamion = camion.comidaCamion;
            }
            
            descargando = false;
            Debug.Log("ontalverdurita");
           
        }
        public override void Act(GameObject objeto)
        {
            if (!descargando)
            {
               descargaCoroutine = fsm.myMono.StartCoroutine(DescargaFunction());
            }


        }

        public override void Reason(GameObject objeto)
        {
            if (objeto.name == "Mercado")
            {
               
                if (mercado.Camion.GetComponent<Camion>().comidaCamion<= 0)
                {
                    Debug.Log("camion vacio listo para vender");
                    fsm.myMono.StopCoroutine(descargaCoroutine);
                    RevertBlipState();
                   
                   
                    
                }
            }

            if (objeto.name == "Camion")
            {
                if (comidaCamion <= 0)
                {
                    
                    camion.Mercado.GetComponent<Mercado>().comidaMercado += comidaMercado;
                    Debug.Log("camionVacio");
                    camion.comidaCamion = 0;
                    fsm.myMono.StopCoroutine(descargaCoroutine);
                    RevertBlipState();
                   
                }
            }


        }
        public override void OnExit(GameObject objeto)
        {

            if(objeto.name=="Camion"){
                 SetAnimationTrigger("IrGranja");
            }
            Debug.Log("Listo la tiendita esta surtida");

        }

        IEnumerator DescargaFunction()
        {
            descargando = true;
            comidaCamion--;
            comidaMercado++;
            
            yield return new WaitForSeconds(0.4f);
            Debug.Log("aquiva otra caja");
           // Debug.Log(comidaCamion);
            descargando = false;
           
        }
    }

    public class Arar : State
    {
        private Granja granja;
        private Granjero granjero;
        
        private bool arando;

        public Arar(Granja _granja)
        {
            granja = _granja;
        }
        public Arar(Granjero _granjero)
        {
            granjero = _granjero;
        }

        public override void OnEnter(GameObject objeto)
        {
            Debug.Log("vamos a arar");
           arando = true;
            fsm.myMono.StartCoroutine(ArarFunction());
        }
        public override void Act(GameObject objeto)
        {
            // if(!cook)



        }
        public override void Reason(GameObject objeto)
        {

            
             
           
                if (arando == false)
                {
                  //si ya termino el tiempo de arado regreso;
                    RevertBlipState();
                }

            
        }

        

        public override void OnExit(GameObject objeto)
        {
            
            Debug.Log("terrenolisto");
            

        }

        IEnumerator ArarFunction()
        {
           
         

            yield return new WaitForSeconds(Plantitas.tiempoArado);
            Debug.Log("arando");
            // Debug.Log(comidaCamion);
            arando= false;

        }

    }

    public class Cosechar : State
    {
        private Granja granja;
        private Granjero granjero;
       
        private bool cosechando;

        public Cosechar(Granja _granja)
        {
            granja = _granja;
        }
        public Cosechar(Granjero _granjero)
        {
            granjero = _granjero;
        }

        public override void OnEnter(GameObject objeto)
        {
            Debug.Log("vamos a cosechar");
            cosechando = true;
             
            fsm.myMono.StartCoroutine(ArarFunction());
            
        }
        public override void Act(GameObject objeto)
        {
            // if(!cook)



        }
        public override void Reason(GameObject objeto)
        {
            

            if (cosechando == false)
            {
              


                //si ya termino el tiempo de arado regreso;
                RevertBlipState();
            }


        }



        public override void OnExit(GameObject objeto)
        {

         if(objeto.name=="Granjero"){
             granjero.camionsin.GetComponent<Camion>().comidaCamion+=25;
         }

        
            Debug.Log("todo en el camion mion");


        }

        IEnumerator ArarFunction()
        {



            yield return new WaitForSeconds(Plantitas.tiempoArado);
            Debug.Log("cosechando");
            // Debug.Log(comidaCamion);
           
            cosechando = false;
        }

       

    }



}   