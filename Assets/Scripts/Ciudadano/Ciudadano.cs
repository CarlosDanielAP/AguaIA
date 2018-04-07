using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CiudadanoStates;

public class Ciudadano : MonoBehaviour 
{
    public FSM fsm;

    // Variables de bob
  
   
    public int hambre;
    public bool ocupado;
    public int sucio;
    public int cansado;
    public GameObject noria;
    public GameObject mercado;
    
    public int platossucios;
    public int enojo;
    public int carrosucio;
    public int comida;
  
    
    void InitCitizenData()
    {
       
        hambre = 5;
        sucio = 5;
        cansado = 5;
        platossucios = 0;
        enojo=0;
        carrosucio = 5;
        comida = 10;
    }

	// Use this for initialization
	void Start () 
    {
        InitCitizenData();
        
        // Hay que hacer la fsm del agente
        fsm = new FSM(gameObject, this);

        // Crear los estados en que puede estar Bob
        Casa casa = new Casa(this);
        Banarse bath = new Banarse(this);
        Comer comer = new Comer(this);
        LavaPlatos lavaplatos = new LavaPlatos(this);
        LavaCarro lavacarro = new LavaCarro(this);
        TirarBasura tirarBasura = new TirarBasura(this);
        Comprar comprar = new Comprar(this);
     




        // Agregar eventos a los estados
        //sleep.AddEvent(EventList.events.dinnerReady);

        // Hay que agregarlos a la FSM
        fsm.AddState(StateID.Casa, casa);
        fsm.AddState(StateID.Banarse, bath);
        fsm.AddState(StateID.Comer, comer);
        fsm.AddState(StateID.LavaPlatos, lavaplatos);
        fsm.AddState(StateID.LavaCarro, lavacarro);
        fsm.AddState(StateID.TirarBasura, tirarBasura);
        fsm.AddState(StateID.Comprar, comprar);



        // Indicar cual es el estado inicial
        fsm.ChangeState(StateID.Casa);

        // Activo la fsm
        fsm.Activate();
	}
	
	void Update () 
    {
		if(fsm != null && fsm.IsActive())
        {
            fsm.UpdateFSM();
        }
	}
}
