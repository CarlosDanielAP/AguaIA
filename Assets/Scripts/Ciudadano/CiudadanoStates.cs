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
        private bool entrar;
        private bool durmiendo;
        private bool ocupado;
        // Una referencia a la corutina
        private Coroutine descansaCoroutine;

        public Casa(Ciudadano _ciudadano)
        {
               ciudadano = _ciudadano;
            triggerName = "Casa";
         
            
        }

        public override void OnEnter(GameObject objeto)
        {Debug.Log(triggerName+"222222222222222222");
        
            //abrir el candado
            ciudadano.ocupado=false;
            descansando = false;
            durmiendo=true;
            ocupado=false;
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
            //sucio carrosucio hambre enojo
            if(!durmiendo){ 
              
              if(ciudadano.sucio>=5){
                  SetAnimationTrigger("Banarse");
                  fsm.myMono.StopCoroutine(descansaCoroutine);
                  InitBlipState(StateID.Banarse);
                  return;
                  
              }
              if(ciudadano.hambre>=5){
                  SetAnimationTrigger("Comer");
                  fsm.myMono.StopCoroutine(descansaCoroutine);
                  ChangeState(StateID.Comer);
                  return;
              }

             
              return;
              
              }
               
               
               if(ciudadano.enojo>=5){
                  SetAnimationTrigger("Basura");
                  fsm.myMono.StopCoroutine(descansaCoroutine);
                  ChangeState(StateID.TirarBasura);
                  return;

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
            if(ciudadano.cansado<=0){
                durmiendo=false;
            }
        }
        IEnumerator entrarFunction()
        {
            yield return new WaitForSeconds(2f);
            entrar=true;
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
                    Debug.Log(fsm.GetBlipState().triggerName+"33333333333333");
                 SetAnimationTrigger(fsm.GetBlipState().triggerName);

                    RevertBlipState();



                }

            }
            if (ciudadano.noria.GetComponent<Noria>().aguaNivel <= 0)
            {
                ciudadano.enojo++;
                fsm.myMono.StopCoroutine(ShowerCoroutine);
                Debug.Log("nohayagua");
                SetAnimationTrigger("Casa");
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
             
          

        }
        public override void Act(GameObject objeto)
        {

            if (!comido)
            {
                EatCoroutine = fsm.myMono.StartCoroutine(EatFunction());
                 if (ciudadano.comida <= 0)
            {
                  fsm.myMono.StopCoroutine(EatCoroutine);
                Debug.Log("notengo comida");
                ciudadano.enojo++;
              
            SetAnimationTrigger("Mercado");
                    ChangeState(StateID.Comprar);
                
            }

            }




        }
        public override void Reason(GameObject objeto)
        {

           

            if (ciudadano.hambre <= 0)
                {


                    fsm.myMono.StopCoroutine(EatCoroutine);
                    //ya no tiene hambre va a lavar los platos
                    SetAnimationTrigger("Trastes");
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
            ciudadano.platossucios++;
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
                    SetAnimationTrigger("Carro");
                    ChangeState(StateID.LavaCarro);
                }
            }




            if (ciudadano.noria.GetComponent<Noria>().aguaNivel <= 0)
            {
                ciudadano.enojo++;
                fsm.myMono.StopCoroutine(platosCoroutine);
                Debug.Log("no hay agua ptm");
                ciudadano.enojo++;
SetAnimationTrigger("Casa");
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
        {  triggerName = "Carro";
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
                    SetAnimationTrigger("Casa");
                    ChangeState(StateID.Casa);
                }

                if (ciudadano.sucio >= 5)
                {
                    SetAnimationTrigger("Banarse");
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
                SetAnimationTrigger("Casa");
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
                    SetAnimationTrigger("Casa");
                    ChangeState(StateID.Casa);
                }
    
        }
        public override void OnExit(GameObject objeto)
        {
        
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
            fsm.myMono.StartCoroutine(ViajeFunction());
            Debug.Log("hola mercadito");
            EventManager.StartListening("Abierto", OnEvent);
            EventManager.StartListening("Cerrado", OnEvent2);
            ciudadano.cansado+=10;
           
           
           






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
               
                
                 fsm.myMono.StartCoroutine(CasaFunction());}
            }

            }
           
        }
        public override void OnExit(GameObject objeto)
        {
            Debug.Log("vamono a casa");
            EventManager.StopListening("Abierto", OnEvent);
            EventManager.StopListening("Cerrado", OnEvent2);
            fsm.myMono.StopAllCoroutines();
            
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
              SetAnimationTrigger("Casa");
            yield return new WaitForSeconds(15f);
            Debug.Log("hhhhhhhhhhhhhhhhhhhhhhhhhhhh");
           
             ChangeState(StateID.Casa);
           
        }
        public override void OnEvent()
        {
            abierto = true;
            EventManager.StopListening("Cerrado",OnEvent);
          
        }

        public override void OnEvent2()
        {
            abierto = false;
          
            EventManager.StopListening("Abierto",OnEvent);
          
        }
    }

}