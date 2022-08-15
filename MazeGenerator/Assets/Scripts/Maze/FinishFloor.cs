using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FinishFloor : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<BoxCollider>().isTrigger = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = null;
        if(other.TryGetComponent(out player))
        {
            GameAssistant.Instance.EnableYouWinPanel();
        }
    }
}
