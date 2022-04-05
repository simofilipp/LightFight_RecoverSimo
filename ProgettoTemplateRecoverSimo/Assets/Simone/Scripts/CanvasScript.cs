using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasScript : MonoBehaviour
{
    Vector3 initialScale;
    float initialPositionY;

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
}
