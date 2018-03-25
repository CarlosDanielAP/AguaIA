using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MinerStates
{
    public enum StateID // Aqui agreguen las claves de cada estado que quieran
    {
        Mining,
        DepositInBank,
        Drinking,
        Sleeping,
        Eating
    }

    //=============================================================
    //===================================================Mining
    public class Mining : State
    {
        private MinerBob bob;

        // Semaforo o candado para tiempos
        private bool working;
        // Una referencia a la corutina
        private Coroutine workingCoroutine;
        
        public Mining(MinerBob _bob)
        {
            bob = _bob;
        }

        public override void OnEnter(GameObject objeto)
        {
            working = false; // Abrir el candado o apagar el semáforo para permitir la corutina
            Debug.Log("Bob: Llegando a la mina!");
        }
        public override void Act(GameObject objeto)
        {
            // Verificar que el candado este abierto o el semaforo apagado
            if( ! working)
            {
                workingCoroutine = fsm.myMono.StartCoroutine( WorkingFunction() );
            }
        }
        public override void Reason(GameObject objeto)
        {
            if(bob.goldInPockets >= 20)
            {
                // Si por alguna razón sigue en ejecución la corutina, la detenemos
                fsm.myMono.StopCoroutine(workingCoroutine);

                // Si tiene animacion, la ejecutamos
                SetAnimationTrigger("BankDeposit");

                ChangeState(StateID.DepositInBank);
            }
            if(bob.thirst >= 25)
            {
                // Si por alguna razón sigue en ejecución la corutina, la detenemos
                fsm.myMono.StopCoroutine(workingCoroutine);

                // Si tiene animacion, la ejecutamos
                SetAnimationTrigger("Drinking");

                ChangeState(StateID.Drinking);
            }
        }
        public override void OnExit(GameObject objeto)
        {
            Debug.Log("Bob: Me voy de la mina");
        }

        IEnumerator WorkingFunction()
        {
            // Cerrar el candado o prender el semáforo
            working = true;

            // Ejecuto las acciones del estado
            yield return new WaitForSeconds(0.5f);
            Debug.Log("Bob: Trabajando ando");
            bob.goldInPockets++; // Gana oro
            bob.thirst++; // También se cansa


            // Abro el candado o apago el semáforo
            working = false;
        }
    }

    //=============================================================
    //===================================================DepositInBank
    public class DepositInBank : State
    {
        private MinerBob bob;

        // Declaro el semáforo
        private bool deposit;
        // Referencia a la corutina
        private Coroutine depositCoroutine;

        public DepositInBank(MinerBob _bob)
        {
            bob = _bob;
        }

        public override void OnEnter(GameObject objeto)
        {
            deposit = false;
            Debug.Log("Bob: Entrando al banco");
        }
        public override void Act(GameObject objeto)
        {
            if( ! deposit)
            {
                depositCoroutine = fsm.myMono.StartCoroutine( DepositFunction() );
            }
        }
        public override void Reason(GameObject objeto)
        {
            if (bob.goldInPockets == 0)
            {
                if (bob.goldInBank < 40)
                {
                    fsm.myMono.StopCoroutine(depositCoroutine);

                    // Si hay animación, la ejecuto
                    SetAnimationTrigger("Mining");

                    ChangeState(StateID.Mining);
                }
                else
                {
                    fsm.myMono.StopCoroutine(depositCoroutine);

                    // Si hay animación, la ejecuto
                    SetAnimationTrigger("Sleeping");

                    ChangeState(StateID.Sleeping);
                }
            }
        }
        public override void OnExit(GameObject objeto)
        {
            Debug.Log("Bob: Dejando el banco con mis esperanzas ¬¬");
        }

        IEnumerator DepositFunction()
        {
            // Prendo la variable
            deposit = true;

            // Acciones
            yield return new WaitForSeconds(5f);
            Debug.Log("Bob: Depositando mi salario");
            bob.goldInBank += bob.goldInPockets;
            bob.goldInPockets = 0;

            // Apagamos la variable
            deposit = false;
        }
    }

    //=============================================================
    //=================================================== Sleeping
    public class Sleeping : State
    {
        private MinerBob bob;

        private bool sleeping;
        private Coroutine sleepingCoroutine;
        private bool rested;
        private bool dinnerReady;

        public Sleeping(MinerBob _bob)
        {
            bob = _bob;
            triggerName = "Sleeping";
            dinnerReady = false;
        }

        public override void OnEnter(GameObject objeto)
        {
            sleeping = false;
            rested = false;
            Debug.Log("Bob: llego a dormir");
            // Avisa que llego a Elsa
            // EVENT
            EventManager.TriggerEvent("imHome");
            EventManager.StartListening("dinnerReady",OnEvent);
        }
        public override void Act(GameObject objeto)
        {
            if (!sleeping)
            {
                sleepingCoroutine = fsm.myMono.StartCoroutine(SleepFunction());
            }


        }
        public override void Reason(GameObject objeto)
        {
            if (dinnerReady)
            {
                // Tiene que ir a la cocina
                SetAnimationTrigger("Eating");

                fsm.myMono.StopCoroutine(sleepingCoroutine);

                dinnerReady = false;

                ChangeState(StateID.Eating);
            }
            if(rested)
            {
                // Descansado, se va a la mina
                SetAnimationTrigger("Mining");
                fsm.myMono.StopCoroutine(sleepingCoroutine);
                ChangeState(StateID.Mining);
            }
        }
        public override void OnExit(GameObject objeto)
        {
            Debug.Log("Elsa: Dejando la cocina un rato");
            // EVENT
            EventManager.StopListening("dinnerReady", OnEvent);
        }

        IEnumerator SleepFunction()
        {
            sleeping = true;

            Debug.Log("Bob: duerme");
            yield return new WaitForSeconds(15f);
            rested = true;
            
            sleeping = false;
        }
        public override void OnEvent()
        {
            dinnerReady = true;
        }
    }

    //=============================================================
    //=================================================== Eating
    public class Eating : State
    {
        private MinerBob bob;

        private bool eating;
        private Coroutine eatingCoroutine;
        private bool full;

        public Eating(MinerBob _bob)
        {
            bob = _bob;
            triggerName = "Eating";
        }

        public override void OnEnter(GameObject objeto)
        {
            eating = false;
            full = false;
            Debug.Log("Bob: Llega a comer");
        }
        public override void Act(GameObject objeto)
        {
            if (!eating)
            {
                eatingCoroutine = fsm.myMono.StartCoroutine(EatFunction());
            }


        }
        public override void Reason(GameObject objeto)
        {
            
            if (full)
            {
                // Ya comió, podría preguntar por la fatiga

                SetAnimationTrigger("Mining");
                fsm.myMono.StopCoroutine(eatingCoroutine);
                ChangeState(StateID.Mining);
            }
        }
        public override void OnExit(GameObject objeto)
        {
            Debug.Log("Bob: Dejo de comer, se va a la mina");
        }

        IEnumerator EatFunction()
        {
            eating = true;

            Debug.Log("Bob: comiendo...");
            yield return new WaitForSeconds(5f);
            full = true;

            eating = false;
        }
    }

    //=============================================================
    //=================================================== Drinking
    public class Drinking : State
    {
        private MinerBob bob;

        private bool drinking;
        private Coroutine drinkingCoroutine;
        private bool full;

        public Drinking(MinerBob _bob)
        {
            bob = _bob;
            triggerName = "Drinking";
        }

        public override void OnEnter(GameObject objeto)
        {
            drinking = false;
            full = false;
            Debug.Log("Bob: A saciar la sed con unos traguitos");
        }
        public override void Act(GameObject objeto)
        {
            if (!drinking)
            {
                drinkingCoroutine = fsm.myMono.StartCoroutine(DrinkFunction());
            }


        }
        public override void Reason(GameObject objeto)
        {

            if (full)
            {
                SetAnimationTrigger("Mining");
                fsm.myMono.StopCoroutine(drinkingCoroutine);
                ChangeState(StateID.Mining);
            }
        }
        public override void OnExit(GameObject objeto)
        {
            Debug.Log("Bob: Basta de copitas por ahora");
        }

        IEnumerator DrinkFunction()
        {
            drinking = true;

            Debug.Log("Bob: Echando una copita...");
            yield return new WaitForSeconds(1f);
            bob.thirst--;

            full = bob.thirst == 0 ? true : false;

            drinking = false;
        }
    }
}