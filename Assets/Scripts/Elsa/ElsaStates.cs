using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ElsaStates
{
    public enum StateID // Aqui agreguen las claves de cada estado que quieran
    {
        Cooking,
        DoHousework,
        Bathroom
    }

    //=============================================================
    //===================================================Cooking
    public class Cooking : State
    {
        private Elsa elsa;

        private bool cook;
        private Coroutine cookCoroutine;
        private bool pooptime;
        private float cookingTime;

        public Cooking(Elsa _elsa)
        {
            elsa = _elsa;
            triggerName = "cook";
            cookingTime = 8f; // Seran los segundos que tarde en cocinar
        }

        public override void OnEnter(GameObject objeto)
        {
            cook = false;
            pooptime = false;
            Debug.Log("Elsa: Entrando a la cocina ¬¬");            
        }
        public override void Act(GameObject objeto)
        {
            if(!cook)
            {
                cookCoroutine = fsm.myMono.StartCoroutine(CookFunction());
            }
            
            
        }
        public override void Reason(GameObject objeto)
        {
            if(pooptime)
            {
                SetAnimationTrigger("bathroom");
                // Detengo la corutina en caso de que siga corriendo
                fsm.myMono.StopCoroutine(cookCoroutine);
                // Cambio de estado
                InitBlipState(StateID.Bathroom);
            }
            if(cookingTime <=0f)
            {
                // Ya acabó de cocinar
                // Reseteo el contador para la siguiente vez que entre
                cookingTime = 8f;

                SetAnimationTrigger("housework");
                fsm.myMono.StopCoroutine(cookCoroutine);

                //EVENT
                EventManager.TriggerEvent("dinnerReady");

                ChangeState(StateID.DoHousework);
            }
        }
        public override void OnExit(GameObject objeto)
        {
            Debug.Log("Elsa: Dejando la cocina un rato");
            
        }

        IEnumerator CookFunction()
        {
            cook = true;

            Debug.Log("Elsa: Cocinando ¬¬");
            yield return new WaitForSeconds(0.4f);
            pooptime = Random.Range(0, 50) == 5 ? true : false;

            cookingTime -= 0.4f;

            cook = false;
        }
    }

    //=============================================================
    //===================================================Bathroom
    public class Bathroom : State
    {
        private Elsa elsa;

        private bool pooping;
        private bool done;
        private Coroutine bathroomCoroutine;

        public Bathroom(Elsa _elsa)
        {
            elsa = _elsa;
            triggerName = "bathroom";
        }

        public override void OnEnter(GameObject objeto)
        {
            pooping = false;
            done = false;
            Debug.Log("Elsa: Entrando al tocador");
        }
        public override void Act(GameObject objeto)
        {
            if(!pooping)
            {
                bathroomCoroutine = fsm.myMono.StartCoroutine(BathroomFunction());
            }
            
        }
        public override void Reason(GameObject objeto)
        {
            if (done)
            {
                // Obtengo el estado del que venía para saber que animación ejecutar
                SetAnimationTrigger(fsm.GetBlipState().triggerName);

                fsm.myMono.StopCoroutine(bathroomCoroutine);

                RevertBlipState();
            }
        }
        public override void OnExit(GameObject objeto)
        {
            Debug.Log("Elsa: Termine de polvearme la nariz");
        }

        IEnumerator BathroomFunction()
        {
            pooping = true;

            Debug.Log("Elsa: Polveandose la nariz");
            yield return new WaitForSeconds(5f);

            pooping = false;
            done = true;
        }
    }

    //=============================================================
    //===================================================Housework
    public class Housework : State
    {
        private Elsa elsa;

        private bool working;
        private Coroutine workingCoroutine;
        private bool bath;
        private bool bobIsHome;
        
        public Housework(Elsa _elsa)
        {
            elsa = _elsa;
            triggerName = "housework";
            bobIsHome = false;
        }

        public override void OnEnter(GameObject objeto)
        {
            working = false;
            bath = false;
            Debug.Log("Elsa: Hace quehacer ¬¬");
            // EVENT
            EventManager.StartListening("imHome", OnEvent);
        }
        public override void Act(GameObject objeto)
        {
            if (!working)
            {
                workingCoroutine = fsm.myMono.StartCoroutine(WorkFunction());
            }


        }
        public override void Reason(GameObject objeto)
        {
            if (bath)
            {
                SetAnimationTrigger("bathroom");

                fsm.myMono.StopCoroutine(workingCoroutine);

                InitBlipState(StateID.Bathroom);
            }

            if(bobIsHome)
            {
                // Tiene que ir a la cocina
                SetAnimationTrigger("cook");

                fsm.myMono.StopCoroutine(workingCoroutine);

                bobIsHome = false;

                ChangeState(StateID.Cooking);
            }
        }
        public override void OnExit(GameObject objeto)
        {
            Debug.Log("Elsa: Dejando la cocina un rato");
            // EVENT
            EventManager.StopListening("imHome", OnEvent);
        }

        IEnumerator WorkFunction()
        {
            working = true;

            Debug.Log("Elsa: Trabajando ¬¬");
            yield return new WaitForSeconds(0.4f);
            bath = Random.Range(0, 100) == 5 ? true : false;

            working = false;
        }
        public override void OnEvent()
        {
            Debug.Log("has event..............................................");
            bobIsHome = true;
        }
    }
}