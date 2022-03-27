using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpadaScript : MonoBehaviour
{
    [SerializeField] Transform slotSpada;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "DiscoNemico")
        {
            var discoColpito = collision.gameObject;
            discoColpito.GetComponent<CapsuleCollider>().enabled = true;
            GameManager.Instance.dischiDistrutti += 1;
            Destroy(discoColpito);
            //fare in modo che il disco si disfi prima di distruggerlo

        }

        if (collision.gameObject.tag == "Floor")
        {
            //respawnare la spada nel porta spade e spegnere lo use gravity e comunicare che siamo senza spada se non ci troviamo già in fase di attacco
            RiposizionaSpada();
        }
    }

    private void RiposizionaSpada()
    {
        var rbSpada = GetComponent<Rigidbody>();
        GetComponentInChildren<VolumetricLines.VolumetricLineBehavior>().EndPos = new Vector3(0,0.001f,0);
        rbSpada.useGravity = false;
        rbSpada.velocity = Vector3.zero;
        rbSpada.angularVelocity = Vector3.zero;
        gameObject.transform.position = slotSpada.position;
        gameObject.transform.rotation = slotSpada.rotation;
        GameManager.Instance.disarmato = true;
    }

    public void AttivaGravità()
    {
        //accendere il laser e portarlo a 3.5 sulla y
        LeanTween.value(0, 3.5f, 0.6f).setOnUpdate((float value) =>
        {
            GetComponentInChildren<VolumetricLines.VolumetricLineBehavior>().EndPos = new Vector3(0, value, 0);
        });


        //viene chiamato quando prendo in mano la spada e sono pronto a difendermi
        GetComponent<Rigidbody>().useGravity = true;
        GameManager.Instance.disarmato = false;
        GameManager.Instance.RiprendiFuocoNemico();
    }
}
