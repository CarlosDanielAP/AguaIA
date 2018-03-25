using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GranjaStates
{
    public enum StateID // Aqui agreguen las claves de cada estado que quieran
    {
     Vacio,
     crecer
    }


    //=============================================================
    //===================================================Vacio
    public class Vacio : State
    {
        private Granja granja;





        public Vacio(Granja _granja)
        {
            granja=_granja;


        }

        public override void OnEnter(GameObject objeto)
        {
          
            Debug.Log("soy una linda granja");
          



        }
        public override void Act(GameObject objeto)
        {
            // if(!cook)



        }
        public override void Reason(GameObject objeto)
        {

            for (int i = 0; i < granja.Cuadrito.Length; i++)
            {


                if (granja.Cuadrito[i].GetComponent<Plantitas>().arar)
                {
                    Debug.Log("vamos a arar" + i);
                  
                   // granja.Cuadrito[i].GetComponent<Plantitas>().arar = false;
                    EventManager.TriggerEvent("Arar");
                    InitBlipState(GlobalStates.GlobalStateID.arar);

                }
                if (granja.Cuadrito[i].GetComponent<Plantitas>().cosechar)
                {
                    Debug.Log("vamos a cosechar" + i);
                    //granja.Cuadrito[i].GetComponent<Plantitas>().cosechar = false;
                    EventManager.TriggerEvent("Cosechar");
                    granja.cuadritoTrabajando = i;
                    InitBlipState(GlobalStates.GlobalStateID.cosechar);
                }
            }

      
           
          

        }
        public override void OnExit(GameObject objeto)
        {
            Debug.Log("vamo a darle");

        }

    }

}