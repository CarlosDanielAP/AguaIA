using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CiudadanoStates
{
    public enum StateID // Aqui agreguen las claves de cada estado que quieran
    {
        Casa,
        Banarse,
        Comer,
        LavaPlatos,
        LavaCarro,
        TirarBasura,
        Comprar
        
    }
    
   
     //=============================================================
    //===================================================Casa
    public class Casa : State
    {
        private Ciudadano ciudadano;

        // Semaforo o candado para tiempos
        private bool descansando;
        // Una referencia a la corutina
        private Coroutine descansaCoroutine;

        public Casa(Ciudadano _ciudadano)
        {
            ciudadano = _ciudadano;
        }

        public override void OnEnter(GameObject objeto)
        {
            //abrir el candado
            descansando = false;
            Debug.Log("llegando a casa");
            
        }
        public override void Act(GameObject objeto)
        {
            if (!descansando)
            {
                descansaCoroutine = fsm.myMono.StartCoroutine(descansandoFunction());
            }
           
        }
        public override void Reason(GameObject objeto)
        {
            if (ciudadano.cansado <= 0) {

                fsm.myMono.StopCoroutine(descansaCoroutine);
                Debug.Log("Buenos Dias");
                //si ya descanso pero esta sucio se va a bañar
                if (ciudadano.sucio >= 5)
                {
                 
                    InitBlipState(StateID.Banarse);
                }
                if (ciudadano.hambre >= 5&&ciudadano.comida>2)//si tiene hambre  y tiene comida va a comer
                {
                    
                    ChangeState(StateID.Comer);
                }

                if (ciudadano.comida <= 2)
                {
                   
                    Debug.Log("vamoa a la tienda");
                    ChangeState(StateID.Comprar);
                }

                //si no esta sucio se queda en casa sin nada que hacer
                if (ciudadano.enojo >= 5)
                {
                    Debug.Log("soy cochino");
                    ChangeState(StateID.TirarBasura);
                }
        


            }
          
        }
        public override void OnExit(GameObject objeto)
        {
            Debug.Log("adioscasita");
           
        }

        IEnumerator descansandoFunction()
        {
            descansando = true;
            yield return new WaitForSeconds(0.5f);
            Debug.Log("ZZZzzzzZZzz...");
            ciudadano.carrosucio++;
            ciudadano.cansado--;
            descansando = false;
        }

       
    }
    //=============================================================
    //===================================================Bañarse
    public class Banarse : State
    {
        
        private Ciudadano ciudadano;
        
        

        // Semaforo o cadado para tiempos
        private bool shower;
        private bool limpio;
        // Una referencia a la corutina
        private Coroutine ShowerCoroutine;

        public Banarse(Ciudadano _ciudadano)
        {
            ciudadano = _ciudadano;
        }

       
        
        

        public override void OnEnter(GameObject objeto)
        {
            shower = false;
            Debug.Log("vamo a bañarnos");

        }
        public override void Act(GameObject objeto)
        {
           
                if (!shower)
                {
                    ShowerCoroutine = fsm.myMono.StartCoroutine(showerFunction());

                }
            
            
                

        }
        public override void Reason(GameObject objeto)
        {
            if (ciudadano.noria.GetComponent<Noria>().aguaNivel > 0)
            {
                if (ciudadano.sucio <= 0)
                {


                    fsm.myMono.StopCoroutine(ShowerCoroutine);
                    //ya esta limpio mandar a otro estado
                    RevertBlipState();



                }

            }
            if (ciudadano.noria.GetComponent<Noria>().aguaNivel <= 0)
            {
                ciudadano.enojo++;
                fsm.myMono.StopCoroutine(ShowerCoroutine);
                Debug.Log("nohayagua");
                ChangeState(StateID.Casa);

            }






            }
        public override void OnExit(GameObject objeto)
        {
            Debug.Log("adios regaderita");
        }

        IEnumerator showerFunction()
        {
            shower = true;
            yield return new WaitForSeconds(2f);
            Debug.Log("cantando en el baño.....");
            ciudadano.sucio--;

            ciudadano.noria.GetComponent<Noria>().aguaNivel--;
            shower = false;
            
        }


    }

    //=============================================================
    //===================================================Comer
    public class Comer : State
    {

        private Ciudadano ciudadano;



        // Semaforo o cadado para tiempos
        private bool comido;
        private bool limpio;
        // Una referencia a la corutina
        private Coroutine EatCoroutine;

        public Comer(Ciudadano _ciudadano)
        {
            ciudadano = _ciudadano;
        }





        public override void OnEnter(GameObject objeto)
        {
            comido = false;
            Debug.Log("que hambre tengo");
           ciudadano.comida--;

        }
        public override void Act(GameObject objeto)
        {

            if (!comido)
            {
                EatCoroutine = fsm.myMono.StartCoroutine(EatFunction());

            }




        }
        public override void Reason(GameObject objeto)
        {

            if (ciudadano.comida <= 0)
            {
                Debug.Log("notengo comida");
                ciudadano.enojo++;
                if (ciudadano.platossucios > 0)
                {
                    ChangeState(StateID.LavaPlatos);
                }
                else
                {
                    ChangeState(StateID.Casa);
                }
            }

            if (ciudadano.hambre <= 0)
                {


                    fsm.myMono.StopCoroutine(EatCoroutine);
                    //ya no tiene hambre va a lavar los platos
                    ChangeState(StateID.LavaPlatos);


                }

            
      






        }
        public override void OnExit(GameObject objeto)
        {
            Debug.Log("que buena comida");

        }

        IEnumerator EatFunction()
        {
            comido = true;
            yield return new WaitForSeconds(0.5f);
            Debug.Log("chomp chomp... rico rico");
            ciudadano.hambre--;
            ciudadano.comida--;
            ciudadano.cansado++;
            comido = false;

        }


    }

    //=============================================================
    //===================================================LavarPlatos
    public class LavaPlatos : State
    {

        private Ciudadano ciudadano;



        // Semaforo o cadado para tiempos
        private bool comido;

       
        // Una referencia a la corutina
        private Coroutine platosCoroutine;

        public LavaPlatos(Ciudadano _ciudadano)
        {
            ciudadano = _ciudadano;
        }



        public override void OnEnter(GameObject objeto)
        {
            comido = false;
            Debug.Log("vamo a limpar los platitos");
            ciudadano.platossucios++;

        }
        public override void Act(GameObject objeto)
        {

            if (!comido)
            {
                platosCoroutine = fsm.myMono.StartCoroutine(PlatosFunction());

            }




        }
        public override void Reason(GameObject objeto)
        {
            if (ciudadano.noria.GetComponent<Noria>().aguaNivel > 0)
            {

                if (ciudadano.platossucios <= 0)
                {


                    fsm.myMono.StopCoroutine(platosCoroutine);
                    //ya no tiene platos que lavar
                    Debug.Log("todos los platos estan limpios");
                    ChangeState(StateID.LavaCarro);
                }
            }




            if (ciudadano.noria.GetComponent<Noria>().aguaNivel <= 0)
            {
                ciudadano.enojo++;
                fsm.myMono.StopCoroutine(platosCoroutine);
                Debug.Log("no hay agua ptm");
                ciudadano.enojo++;

                ChangeState(StateID.Casa);
            }




        }
        public override void OnExit(GameObject objeto)
        {
            Debug.Log("vamonos");

        }

        IEnumerator PlatosFunction()
        {
            comido = true;
            yield return new WaitForSeconds(0.5f);
            Debug.Log("lava lava");
            ciudadano.cansado++;
            ciudadano.platossucios--;
            comido = false;

        }


    }

    //=============================================================
    //===================================================LavarCarro
    public class LavaCarro : State
    {
        private Ciudadano ciudadano;



        // Semaforo o cadado para tiempos
        private bool comido;


        // Una referencia a la corutina
        private Coroutine carroCoroutine;

        public LavaCarro(Ciudadano _ciudadano)
        {
            ciudadano = _ciudadano;
        }



        public override void OnEnter(GameObject objeto)
        {
            comido = false;
            Debug.Log("vamo a lavar la nave");
            

        }
        public override void Act(GameObject objeto)
        {

            if (!comido)
            {
                carroCoroutine = fsm.myMono.StartCoroutine(carroFunction());

            }




        }
        public override void Reason(GameObject objeto)
        {
            if (ciudadano.noria.GetComponent<Noria>().aguaNivel > 0)
            {

                if (ciudadano.carrosucio <= 0)
                {


                    fsm.myMono.StopCoroutine(carroCoroutine);
                    //ya sta limpio su auto
                    Debug.Log("cachin cachin que lindo brilla");
                    ChangeState(StateID.Casa);
                }

                if (ciudadano.sucio >= 5)
                {
                    fsm.myMono.StopCoroutine(carroCoroutine);
                    InitBlipState(StateID.Banarse);
                }
                
            }




            if (ciudadano.noria.GetComponent<Noria>().aguaNivel <= 0)
            {
                ciudadano.enojo++;
                fsm.myMono.StopCoroutine(carroCoroutine);
                Debug.Log("no hay agua micarro estara cochino");
                ciudadano.enojo++;

                ChangeState(StateID.Casa);
            }




        }
        public override void OnExit(GameObject objeto)
        {
            Debug.Log("adios carrito");

        }

        IEnumerator carroFunction()
        {
            comido = true;
            yield return new WaitForSeconds(0.5f);
            Debug.Log("lava lava la nave");
            ciudadano.cansado++;
            ciudadano.hambre++;
            ciudadano.sucio++;
            ciudadano.carrosucio--;
            comido = false;

        }
    }
    //=============================================================
    //===================================================TirarBasura
    public class TirarBasura : State
    {

        private Ciudadano ciudadano;



        // Semaforo o cadado para tiempos
        private bool comido;


        // Una referencia a la corutina
        private Coroutine basuraCoroutine;

        public TirarBasura(Ciudadano _ciudadano)
        {
            ciudadano = _ciudadano;
        }



        public override void OnEnter(GameObject objeto)
        {
            comido = false;
            Debug.Log("usando desechables");
              SetAnimationTrigger("Basura");
            

        }
        public override void Act(GameObject objeto)
        {

            if (!comido)
            {
               basuraCoroutine = fsm.myMono.StartCoroutine(basuraFunction());

            }




        }
        public override void Reason(GameObject objeto)
        {
            

                if (ciudadano.enojo <= 0)
                {


                    fsm.myMono.StopCoroutine(basuraCoroutine);
                    //ya no tiene platos que lavar
                    Debug.Log("ya no tengo mas basura");
                    ChangeState(StateID.Casa);
                }
    
        }
        public override void OnExit(GameObject objeto)
        {
            SetAnimationTrigger("Casa");
            Debug.Log("vamono a casa");

        }

        IEnumerator basuraFunction()
        {
            comido = true;
            yield return new WaitForSeconds(5f);
            Debug.Log("tira tirar");
            ciudadano.enojo--;
            ciudadano.cansado++;
            ciudadano.hambre++;
            ciudadano.noria.GetComponent<Noria>().rio.GetComponent<Rio>().basura+=2;
            
            comido = false;

        }


    }

    //=============================================================
    //===================================================Comprar
    public class Comprar : State
    {

        private Ciudadano ciudadano;



        // Semaforo o cadado para tiempos
        private bool comido;
        private bool abierto;

        private bool llegando;
        private bool viajando;


        // Una referencia a la corutina
        private Coroutine basuraCoroutine;

        public Comprar(Ciudadano _ciudadano)
        {
            ciudadano = _ciudadano;
        }



        public override void OnEnter(GameObject objeto)
        {
            abierto = false;
            comido = false;
            llegando=false;
            viajando=false;
            Debug.Log("hola mercadito");
            EventManager.StartListening("Abierto", OnEvent);
            EventManager.StartListening("Cerrado", OnEvent2);
            fsm.myMono.StartCoroutine(ViajeFunction());
             SetAnimationTrigger("Mercado");






        }
        public override void Act(GameObject objeto)
        {
            if(llegando){
            if (abierto)
            {
                Debug.Log("abrido");
                if (!comido)
                {
                    basuraCoroutine = fsm.myMono.StartCoroutine(basuraFunction());

                }
            }

            }


        }
        public override void Reason(GameObject objeto)
        {
            if(llegando){
            if (abierto)
            {
                Debug.Log("yuuuuju comidita");

                if (ciudadano.comida >= 15)
                {
                    //si esta abierto y tiene provisiones suficientes regresa a casa
                    fsm.myMono.StopCoroutine(basuraCoroutine);
                    SetAnimationTrigger("Casa");
                   if(viajando==false){
                    fsm.myMono.StartCoroutine(CasaFunction());
                    }
                    
                }


            }
            if (!abierto)
            {   if(viajando==false){
                fsm.myMono.StopAllCoroutines();
                Debug.Log("caray ya esta cerrado");
                EventManager.StopListening("Cerrado", OnEvent2);
                if (ciudadano.comida < 15)
                {
                    ciudadano.enojo++;
                    Debug.Log("melleva la v.....");
                }
                SetAnimationTrigger("Casa");
                
                 fsm.myMono.StartCoroutine(CasaFunction());}
            }

            }
           
        }
        public override void OnExit(GameObject objeto)
        {
            Debug.Log("vamono a casa");
            EventManager.StopListening("Abierto", OnEvent);
            EventManager.StopListening("Cerrado", OnEvent2);
            
        }

        IEnumerator basuraFunction()
        {
            comido = true;
            
            ciudadano.comida++;
            ciudadano.mercado.GetComponent<Mercado>().comidaMercado--;
           
            Debug.Log("meda un kilito ");
            yield return new WaitForSeconds(5f);
            comido = false;

        }

        IEnumerator ViajeFunction(){
            yield return new WaitForSeconds(11f);
            llegando=true;
        }
         IEnumerator CasaFunction(){
             viajando=true;
             
            yield return new WaitForSeconds(15f);
            Debug.Log("hhhhhhhhhhhhhhhhhhhhhhhhhhhh");
             ChangeState(StateID.Casa);
           
        }
        public override void OnEvent()
        {
            abierto = true;
          
        }

        public override void OnEvent2()
        {
            abierto = false;
          
            EventManager.StopListening("Abierto",OnEvent);
          
        }
    }

}