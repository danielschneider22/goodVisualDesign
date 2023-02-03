using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBeatManager : MonoBehaviour
{
    public bool isClosed;
    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void ToggleDoor()
    {
        isClosed = !isClosed;
        animator.SetBool("IsOpen", isClosed);
    }
}
