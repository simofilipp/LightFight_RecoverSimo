using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Experimental.Rendering.Universal;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] Volume volume;
    Vignette vignette;
    int health;
    private void Awake()
    {
        health = 5;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Debug.Log("Sono morto");
            //terminare il gioco perchè siamo morti, far comparire una ui per riprovare
            GameManager.Instance.CambiaFaseGioco(FaseDiGioco.FaseMorte);

            //effetto vignetta per fare capire che si è stati colpiti
        }
    }

    public void SubisciDanno(int damage)
    {
        health -= damage;
        if (volume.profile.TryGet(out vignette))
            vignette.intensity.value += 0.15f;
    }
}
