using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DiscoGrab : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
            //deve riprendere il fuoco nel caso io distrugga questo disco
        if (collision.gameObject.tag == "RestartFuocoNemico" || collision.gameObject.tag == "Sword")
        {
            //fare scomparire temporaneamente la spada mentro lo ho in mano

            //effetto quando il disco si scontra

            //se non afferro o manco il nemico il disco si distrugge e il nemico torna a spararmi
            GameManager.Instance.RiprendiFuocoNemico();
            Destroy(this.gameObject);
        }
        if (collision.gameObject.tag == "Enemy")
        {
            EnemyScript nemico;
            //se colpisco il nemico distruggo un cannone
            if (collision.gameObject.GetComponent<EnemyScript>())
            {
                nemico = collision.gameObject.GetComponent<EnemyScript>();

            }
            else
            {
                nemico = collision.transform.parent.GetComponent<EnemyScript>();
            }
            
            nemico.DistruggiCannone();
            GameManager.Instance.RiprendiFuocoNemico();
            Destroy(this.gameObject);
        }
    }
}
