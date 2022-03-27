using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FaseDiGioco
{
    FaseDiDifesa,
    FaseAttacco,
    FaseIniziale
}

public class GameManager : Singleton<GameManager>
{
    //la classe singleton deriva gia da monobehaviour, basta imparentare questa e si ha un singleton

    public int dischiDistrutti = 0;
    public bool lanciaDiscoGrab;
    public bool disarmato;
    public FaseDiGioco faseCorrente;


    [SerializeField] EnemyScript enemy;
    // Start is called before the first frame update
    void Start()
    {
        faseCorrente = FaseDiGioco.FaseIniziale;    
    }

    // Update is called once per frame
    void Update()
    {
        if (dischiDistrutti >= 10)
        {
            dischiDistrutti = 0;
            Debug.Log("Disco Grabbable");
            //istanziare disco grabbabile
            lanciaDiscoGrab = true;
        }
    }

    public void CambiaFaseGioco(FaseDiGioco fdg)
    {
        faseCorrente = fdg;
    }

    public void RiprendiFuocoNemico()
    {
        //Riprende a sparare il nemico 
        faseCorrente = FaseDiGioco.FaseDiDifesa;
        enemy.RiprendiFuoco();
    }
}

