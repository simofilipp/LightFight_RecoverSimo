using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasScript : MonoBehaviour
{
    Vector3 initialScale;
    float initialPositionY;
    float timer;

    [SerializeField] TMP_Text testoPanel;
    public bool visibile;
    // Start is called before the first frame update
    void Start()
    {
        initialScale = transform.localScale;
        initialPositionY = transform.position.y;
        visibile = false;
        transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (!visibile && GameManager.Instance.faseCorrente != FaseDiGioco.FaseMorte && GameManager.Instance.faseCorrente != FaseDiGioco.FaseVittoria)
        {
            timer += Time.deltaTime;
        }
        else
        {
            if (GameManager.Instance.faseCorrente == FaseDiGioco.FaseMorte)
            {
                testoPanel.text = "GAME OVER";
            }
            else if (GameManager.Instance.faseCorrente == FaseDiGioco.FaseVittoria)
            {
                testoPanel.text = "YOU WIN IN\n" + FormatTime(timer * 1000)+"\nMINUTES";
            }
            else 
            {
                    //stoppo il timer e mostro il tempo attuale
                testoPanel.text = "Tempo: " + FormatTime(timer * 1000);
            }
        }
    }

    public void MostraCanva()
    {
        visibile = true;

        //transform.LeanMoveY(2, 1.4f).setEaseOutQuart();
        transform.LeanScale(initialScale, 0.5f).setEaseOutQuart().setEaseOutBack();
    }

    public void SpegniCanva()
    {
        visibile = false;
        //transform.LeanMoveY(initialPositionY, 1.4f).setEaseOutQuart();
        transform.LeanScale(Vector3.zero, 0.5f).setEaseOutQuart().setEaseInBack();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public string FormatTime(float time)
    {
        int minutes = (int)time / 60000;
        int seconds = (int)time / 1000 - 60 * minutes;
        int milliseconds = (int)time - minutes * 60000 - 1000 * seconds;
        return string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
    }
}
