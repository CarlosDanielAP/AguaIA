using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ElsaStates;

public class Elsa : MonoBehaviour
{
    public FSM fsm;

    // Variables de elsa

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
        Cooking cook = new Cooking(this);
        Bathroom bath = new Bathroom(this);
        Housework work = new Housework(this);

        // Asignarle a cada estado los eventos que puede tener
        //work.AddEvent(EventList.events.imHome);
       
        // Hay que agregarlos a la FSM
        fsm.AddState(StateID.Cooking, cook);
        fsm.AddState(StateID.Bathroom, bath);
        fsm.AddState(StateID.DoHousework, work);
       
        // Indicar cual es el estado inicial
        fsm.ChangeState(StateID.DoHousework);
       
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
