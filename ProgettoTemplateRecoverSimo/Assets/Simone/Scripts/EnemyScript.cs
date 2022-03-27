using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] List<GameObject> cannoni;
    [SerializeField] GameObject discoLaser;
    [SerializeField] GameObject discoGrab;
    [SerializeField] GameObject player;
    [SerializeField] float forzaEsplosione;
    [SerializeField] float rateoDiFuoco;

    public Coroutine fuocoNemico;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //nemico rivolto verso il player
        transform.LookAt(player.transform.position);

    }

    IEnumerator FuocoNemico()
    {
        while (GameManager.Instance.faseCorrente==FaseDiGioco.FaseDiDifesa)
        {
            if (!GameManager.Instance.disarmato && cannoni.Count>0)
            {
                //prendo un puntatore a caso tra i cannoni
                var puntatoreCasuale = cannoni[Random.Range(0, cannoni.Count)].transform.GetChild(0);
                //istanzio un disco laser e lo sparo
                GameObject colpo;
                if (GameManager.Instance.lanciaDiscoGrab)
                {
                    //spara il disco grabbabile e si cambia fase di gioco
                    colpo = Instantiate(discoGrab, puntatoreCasuale.position, puntatoreCasuale.rotation);
                    colpo.transform.LookAt(player.transform);
                    GameManager.Instance.lanciaDiscoGrab = false;
                    GameManager.Instance.CambiaFaseGioco(FaseDiGioco.FaseAttacco);

                }
                else
                {
                    colpo = Instantiate(discoLaser, puntatoreCasuale.position, puntatoreCasuale.rotation);
                    Destroy(colpo, 4f);
                }
                colpo.GetComponent<Rigidbody>().AddForce(puntatoreCasuale.forward * forzaEsplosione, ForceMode.VelocityChange);
            }
                yield return new WaitForSeconds(rateoDiFuoco);
        }
    }

    public void RiprendiFuoco()
    {
        if (GameManager.Instance.faseCorrente == FaseDiGioco.FaseIniziale)
            fuocoNemico = StartCoroutine(FuocoNemico());
        else
        {
            StopAllCoroutines();
            StartCoroutine(FuocoNemico());
        }
    }

    public void DistruggiCannone()
    {
        //rimuovo il primo cannone dalla lista e lo distruggo in scena
        if (cannoni.Count > 0)
        {
            var cannoneDistrutto = cannoni[Random.Range(0, cannoni.Count)];
            cannoni.Remove(cannoneDistrutto);
            Destroy(cannoneDistrutto);
        }
    }
}
