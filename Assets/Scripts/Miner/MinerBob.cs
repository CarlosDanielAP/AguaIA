using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MinerStates;

public class MinerBob : MonoBehaviour 
{
    public FSM fsm;

    // Variables de bob
    public int goldInPockets;
    public int goldInBank;
    public int thirst;
    public int fatigue;

    void InitMinerData()
    {


        goldInPockets = 0;
        goldInBank = 0;
        thirst = 0;
        fatigue = 0;
    }

	// Use this for initialization
	void Start () 
    {
        InitMinerData();
        
        // Hay que hacer la fsm del agente
        fsm = new FSM(gameObject, this);

        // Crear los estados en que puede estar Bob
        Mining mining = new Mining(this);
        DepositInBank deposit = new DepositInBank(this);
        Sleeping sleep = new Sleeping(this);
        Eating eat = new Eating(this);
        Drinking drink = new Drinking(this);

        // Agregar eventos a los estados
        //sleep.AddEvent(EventList.events.dinnerReady);

        // Hay que agregarlos a la FSM
        fsm.AddState(StateID.Mining, mining);
        fsm.AddState(StateID.DepositInBank, deposit);
        fsm.AddState(StateID.Sleeping, sleep);
        fsm.AddState(StateID.Eating, eat);
        fsm.AddState(StateID.Drinking, drink);

        // Indicar cual es el estado inicial
        fsm.ChangeState(StateID.Mining);

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
