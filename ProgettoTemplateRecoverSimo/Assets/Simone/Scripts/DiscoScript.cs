using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoScript : Item
{
    public GameObject discorotto;
    public GameObject discoIntero;
    private void Awake()
    {
        Damage = 1;
    }
    private void Start()
    {
        discoIntero.LeanRotateAround(transform.up, 360, 0.4f).setLoopCount(50);
    }
    private void OnCollisionEnter(Collision collision)
    {
        //derivo la classe da item in modo da settare il damage, il player andrà a leggere questo valore per subire danni
        Debug.Log(collision.gameObject.name);

        //distruggo il disco nel caso vada contro un muro invisibile
        if(collision.gameObject.tag== "RestartFuocoNemico")
        {
            Destroy(this.gameObject);
        }

        if (collision.gameObject.tag == "Player")
        {
            //togliere vita al player
            collision.gameObject.GetComponent<PlayerScript>().SubisciDanno(Damage);
            Destroy(this.gameObject);
        }
    }

    public void EsplodiDisco(Vector3 puntoEsplosione)
    {
        discoIntero.SetActive(false);
        discorotto.SetActive(true);
        for(int i = 0; i < discorotto.transform.childCount; i++)
        {
            discorotto.transform.GetChild(0).GetComponent<Rigidbody>().AddExplosionForce(8, puntoEsplosione,10,0,ForceMode.Impulse);
        }
        Destroy(this.gameObject, 1f);
    }
}
