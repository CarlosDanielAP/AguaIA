using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GranjaStates;
using GlobalStates;

public class Granja : MonoBehaviour
{
    public FSM fsm;

    // Variables de elsa
    public int cuadritoTrabajando;
   
   
    public GameObject[] Cuadrito;

  

    void InitMinerData()
    {
       
       
        
    }

    // Use this for initialization
    void Start()
    {
        InitMinerData();

        // Hay que hacer la fsm del agente
        fsm = new FSM(gameObject, this);

        // Crear los estados en que puede estar 
       Vacio vacio = new Vacio(this);
        Arar arar = new Arar(this);
        Cosechar cosechar = new Cosechar(this);


        // Asignarle a cada estado los eventos que puede tener
        //work.AddEvent(EventList.events.imHome);

        // Hay que agregarlos a la FSM
        fsm.AddState(StateID.Vacio, vacio);
        fsm.AddState(GlobalStateID.arar, arar);
        fsm.AddState(GlobalStateID.cosechar, cosechar);


        // Indicar cual es el estado inicial
        // fsm.ChangeState(GlobalStateID.abastecer);
        fsm.ChangeState(StateID.Vacio);
        

        // Activo la fsm
        fsm.Activate();
    }

    void Update()
    {
        if (fsm != null && fsm.IsActive())
        {
            fsm.UpdateFSM();
        }
    }
}
