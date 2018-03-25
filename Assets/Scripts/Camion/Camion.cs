using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CamionStates;
using GlobalStates;

public class Camion : MonoBehaviour
{
    public FSM fsm;

    // Variables de elsa
    public int comidaCamion;
    public GameObject Mercado;

  

    void InitMinerData()
    {
        comidaCamion = 10;
        Usuario.mandarCamion = false;//variable para mandarlo al mercado cambiar a usuario
        
    }

    // Use this for initialization
    void Start()
    {
        InitMinerData();

        // Hay que hacer la fsm del agente
        fsm = new FSM(gameObject, this);

        // Crear los estados en que puede estar 
        IrGranja granja = new IrGranja(this);
        Abastecer abastecer = new Abastecer(this);
       

        // Asignarle a cada estado los eventos que puede tener
        //work.AddEvent(EventList.events.imHome);
       
        // Hay que agregarlos a la FSM
        fsm.AddState(GlobalStateID.abastecer, abastecer);
        fsm.AddState(StateID.IrGranja, granja);


        // Indicar cual es el estado inicial
        // fsm.ChangeState(GlobalStateID.abastecer);
        fsm.ChangeState(StateID.IrGranja);
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
