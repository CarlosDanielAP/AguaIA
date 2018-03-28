using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CamionStates
{
    public enum StateID // Aqui agreguen las claves de cada estado que quieran
    {
       IrGranja
    }


    //=============================================================
    //===================================================IrGranja
    public class IrGranja : State
    {
        private Camion camion;





        public IrGranja(Camion _camion)
        {
            camion=_camion;


        }

        public override void OnEnter(GameObject objeto)
        {
            Usuario.mandarCamion = false;
            Debug.Log("lelgando a la granja");
          



        }
        public override void Act(GameObject objeto)
        {
            // if(!cook)



        }
        public override void Reason(GameObject objeto)
        {
            if (Usuario.mandarCamion)
            {
                 SetAnimationTrigger("IrTienda");
                Debug.Log("vamos a entregar");
                InitBlipState(GlobalStates.GlobalStateID.abastecer);
            }
          

        }
        public override void OnExit(GameObject objeto)
        {
            Debug.Log("vamooooooonooooooos");

        }

    }

}