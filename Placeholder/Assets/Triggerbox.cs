using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;


public class Triggerbox : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private GameObject buttonObject;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      buttonObject.SetActive(false);
        text.enabled = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            text.enabled = true;
            buttonObject.SetActive(true);
            print("Office enter text has been disabled");
        }
       
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            text.enabled = false;
            buttonObject.SetActive(false);
            print("Office enter text has been disabled");
        }
    }
    
}
