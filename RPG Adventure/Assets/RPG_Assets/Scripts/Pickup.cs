using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Pickup : MonoBehaviour
{
    public GameObject MessagePanel;
    public void OpenMessagePanel()
    {
        MessagePanel.SetActive(true);

    }
    public void CloseMessagePanel()
    {
        MessagePanel.SetActive(false);

    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            OpenMessagePanel();
        }


    }
    public void OnTriggerExit(Collider other)
    {
        CloseMessagePanel();

    }
}