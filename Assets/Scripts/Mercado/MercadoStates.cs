using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MercadoStates
{
    public enum StateID // Aqui agreguen las claves de cada estado que quieran
    {
        Vender,
      
    }

    //=============================================================
    //===================================================Vender
    public class Vender : State
    {
        private Mercado mercado;


        
       

        public Vender(Mercado _mercado)
        {
            mercado = _mercado;
          
           
        }

        public override void OnEnter(GameObject objeto)
        {
           mercado.abierto = true;
            
            Debug.Log("welcome to de mercadito");
            EventManager.TriggerEvent("Abierto");
            EventManager.StartListening("Abierto", OnEvent);




        }
        public override void Act(GameObject objeto)
        {
           // if(!cook)
          
            
            
        }
        public override void Reason(GameObject objeto)
        {
            if (Usuario.mandarCamion)
            {
                InitBlipState(GlobalStates.GlobalStateID.abastecer);
            }
            if (mercado.comidaMercado > 0)
            {
                EventManager.TriggerEvent("Abierto");
                EventManager.StartListening("Abierto", OnEvent);
            }

            if (mercado.comidaMercado <= 0)
            {
               // Debug.Log("yano tengo comida");
                EventManager.TriggerEvent("Cerrado");
                mercado.abierto = false;
                EventManager.StopListening("Abierto", OnEvent);

            }
          
        }
        public override void OnExit(GameObject objeto)
        {
            Debug.Log("estamos cerrados");
            
        }

        public override void OnEvent()
        {
            mercado.abierto = true;
           // Debug.Log("abierto");
        }

    }

    
}