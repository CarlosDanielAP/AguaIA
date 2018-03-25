using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MercadoStates;
using GlobalStates;

public class Mercado : MonoBehaviour
{
    public FSM fsm;

    // Variables del mercado

    public int comidaMercado;
    public GameObject Camion;
    public bool abierto;

    void InitMinerData()
    {
        comidaMercado = 10;
        
    }

    // Use this for initialization
    void Start()
    {
        InitMinerData();

        // Hay que hacer la fsm del agente
        fsm = new FSM(gameObject, this);

        // Crear los estados en que puede estar 
        Vender vender = new Vender(this);
        Abastecer abastecer = new Abastecer(this);
       

        // Asignarle a cada estado los eventos que puede tener
        //work.AddEvent(EventList.events.imHome);
       
        // Hay que agregarlos a la FSM
        fsm.AddState(GlobalStateID.abastecer, abastecer);
        fsm.AddState(StateID.Vender, vender);
       
       
        // Indicar cual es el estado inicial
        fsm.ChangeState(StateID.Vender);
       
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
