using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoScript : Item
{
    public GameObject discorotto;
    private void Awake()
    {
        Damage = 1;
    }
    private void Start()
    {
        transform.LeanRotateAround(transform.up, 360, 0.4f).setLoopCount(50);
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

    public void EsplodiDisco()
    {
        discorotto.SetActive(true);
        for(int i=0; i < discorotto.transform.childCount; i++)
        {
            var pezzoRB=discorotto.transform.GetChild(i).GetComponent<Rigidbody>();
            pezzoRB.AddExplosionForce(10, discorotto.transform.position, 3);
        }
    }
}
