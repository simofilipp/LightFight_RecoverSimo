using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.SceneManagement;


/// <summary>
/// Agganciare questo script ai controller vr
/// </summary>

[RequireComponent(typeof(XRBaseController))]
public class HandInputHandler : MonoBehaviour
{
    
    protected XRBaseController controller;
    [SerializeField]
    protected InputActionReference CanvasAction;
    [SerializeField]
    protected AudioClip suono;

    [SerializeField]
    GameObject canva;



    void Awake()
    {
        controller = GetComponent<XRBaseController>();
        CanvasAction.action.performed += PlayCanvas;
    }

    private void Restart(InputAction.CallbackContext obj)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// Per utilizzare la vibrazione del controller
    /// NOTA: se si utilizza la vibrazione agganciare questo component SEMPRE al controller
    /// </summary>
    /// <param name="amplitude"></param>
    /// <param name="duration"></param>
    public void SendHapticImpulse(float amplitude, float duration)
    {
        controller.SendHapticImpulse(amplitude, duration);
    }
    private void PlayCanvas(InputAction.CallbackContext obj)
    {
        if(GameManager.Instance.disarmato && !canva.GetComponent<CanvasScript>().visibile)
        {
            canva.GetComponent<CanvasScript>().MostraCanva();
        }
        else if(GameManager.Instance.disarmato && canva.GetComponent<CanvasScript>().visibile)
        {
            canva.GetComponent<CanvasScript>().SpegniCanva();
        }
        else
        {
            Debug.Log("Niente");
        }
    }

    private void OnDestroy()
    {
        CanvasAction.action.performed -= PlayCanvas;

    }
}
