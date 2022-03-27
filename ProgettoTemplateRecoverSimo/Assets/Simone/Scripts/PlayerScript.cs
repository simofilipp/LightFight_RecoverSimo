using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    int health;
    private void Awake()
    {
        health = 3;
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

            //effetto vignetta per fare capire che si è stati colpiti
        }
    }

    public void SubisciDanno(int damage)
    {
        health -= damage;
    }
}
