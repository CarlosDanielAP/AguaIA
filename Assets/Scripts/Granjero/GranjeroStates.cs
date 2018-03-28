using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GranjeroStates
{
    public enum StateID // Aqui agreguen las claves de cada estado que quieran
    {
        Casa,
        Regar
    }


    //=============================================================
    //===================================================Casa
    public class Casa : State
    {
        private Granjero granjero;




        public Casa(Granjero _granjero)
        {
            granjero = _granjero;


        }

        public override void OnEnter(GameObject objeto)
        {
            granjero.IrArar = false;
            granjero.IrCosechar = false;
            EventManager.StartListening("Arar", OnEvent);
            EventManager.StartListening("Cosechar", OnEvent2);
            Debug.Log("soy un bello granjero");

          




        }
        public override void Act(GameObject objeto)
        {
            



        }
        public override void Reason(GameObject objeto)
        {
            if (granjero.IrArar)
            {
                Debug.Log("Granjero: vamos a arar");
                 SetAnimationTrigger("Irarar");
                InitBlipState(GlobalStates.GlobalStateID.arar);
                
            }
            if (granjero.IrCosechar)
            {
                Debug.Log("Granjero: vamos a cosechar");
                SetAnimationTrigger("IrCosechar");
                InitBlipState(GlobalStates.GlobalStateID.cosechar);
                
            }

            if (granjero.IrRegar)
            {
                SetAnimationTrigger("IrAgua");
                    ChangeState(StateID.Regar);
            }

        }
        public override void OnExit(GameObject objeto)
        {
            Debug.Log("Granjero:vamo a darle");
            EventManager.StopListening("Arar", OnEvent);
            EventManager.StopListening("Cosechar", OnEvent2);

        }

        public override void OnEvent()
        {
            granjero.IrArar = true;
        }
        public override void OnEvent2()
        {
            granjero.IrCosechar = true;
        }

    }
    //=============================================================
    //===================================================noria
    public class Regar : State
    {
        private Granjero granjero;

        private bool girando;
        private Coroutine girarCoroutine;


        public Regar(Granjero _granjero)
        {
            granjero = _granjero;


        }

        public override void OnEnter(GameObject objeto)
        {
            Debug.Log("vamos a  llave");
            granjero.listo = false;
            girando = false;


        }
        public override void Act(GameObject objeto)
        {
            // if(!cook)
           
            if (!granjero.listo)
            {
                if (!girando)
                {
                    girarCoroutine = fsm.myMono.StartCoroutine(GirarFunction());
                }
            }
        }
        public override void Reason(GameObject objeto)
        {

            if (granjero.listo)
            {
                fsm.myMono.StopCoroutine(girarCoroutine);
                ChangeState(StateID.Casa);
            }

        }
        public override void OnExit(GameObject objeto)
        {
            Debug.Log("regresando");
            granjero.IrRegar = false;
            Usuario.pressregando = true;
            

        }
        IEnumerator GirarFunction()
        {
            girando = true;

            Debug.Log("girando la llave");
            yield return new WaitForSeconds(8f);

            if (!Usuario.regando)
            {
                Usuario.regando = true;
                Debug.Log("abierto");
              granjero.agua.GetComponent<Animator>().SetTrigger("AguaAbierto");
            }
            else
            {
                Usuario.regando = false;
                Debug.Log("cerrado");
                granjero.agua.GetComponent<Animator>().SetTrigger("AguaCerrado");
            }
            granjero.listo = true;
           
           girando = false;
        }



    }

}