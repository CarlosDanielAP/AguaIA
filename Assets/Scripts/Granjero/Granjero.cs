using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GranjeroStates;
using GlobalStates;

public class Granjero : MonoBehaviour
{
    public FSM fsm;

    // Variables de elsa
    public bool IrArar;
    public bool IrCosechar;
    public bool IrRegar;
    public bool listo;

    public GameObject agua;

  

    void InitGranjeroData()
    {
       IrArar=false;
        IrCosechar = false;//variable para mandarlo al mercado cambiar a usuario
        IrRegar = false;
        listo = false;
        
    }

    // Use this for initialization
    void Start()
    {
        
        InitGranjeroData();

        // Hay que hacer la fsm del agente
        fsm = new FSM(gameObject, this);

        // Crear los estados en que puede estar 
        Casa casa = new Casa(this);
        Arar arar = new Arar(this);
        Regar regar = new Regar(this);
        Cosechar cosechar = new Cosechar(this);



        // Hay que agregarlos a la FSM
        fsm.AddState(StateID.Casa,casa);
        fsm.AddState(StateID.Regar, regar);
        fsm.AddState(GlobalStateID.arar, arar);
        fsm.AddState(GlobalStateID.cosechar, cosechar);




        // Indicar cual es el estado inicial

        fsm.ChangeState(StateID.Casa);
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
