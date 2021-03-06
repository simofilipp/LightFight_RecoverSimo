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
    [SerializeField] AudioClip colpoSparato;
    [SerializeField] AudioClip esplosione;

    public Coroutine fuocoNemico;
    AudioSource audioSourceNemico;
    // Start is called before the first frame update
    void Start()
    {
        audioSourceNemico = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //nemico rivolto verso il player
        transform.LookAt(player.transform.position);

        //se senza cannoni, si vince
        if (cannoni.Count == 0)
        {
            GameManager.Instance.CambiaFaseGioco(FaseDiGioco.FaseVittoria);
        }
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
                audioSourceNemico.PlayOneShot(colpoSparato);
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
        //rimuovo cannone dalla lista e lo distruggo in scena
        if (cannoni.Count > 0)
        {
            //Effetto esplosione e distruzione cannone
            var cannoneDistrutto = cannoni[Random.Range(0, cannoni.Count)];
            var solve = cannoneDistrutto.GetComponent<MeshRenderer>().material;
            cannoni.Remove(cannoneDistrutto);
            cannoneDistrutto.transform.GetChild(1).gameObject.SetActive(true);
            audioSourceNemico.PlayOneShot(esplosione);
            LeanTween.value(-1f, 1f, 3f).setOnUpdate((float value) =>
              {
                  solve.SetFloat("_Dissolvenza_animazione", value);
              }).setOnComplete(() =>
              {
                  Destroy(cannoneDistrutto);
              });
            //aumento rateo di fuoco nemico
            rateoDiFuoco -= 0.15f;
            forzaEsplosione += 1.2f;
        }
    }
}
