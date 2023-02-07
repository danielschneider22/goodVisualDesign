using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseLightWithMusic : MonoBehaviour
{
    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void TogglePulse()
    {
        if(animator != null)
        {
            animator.SetTrigger("Pulse");
        }
        
    }
}
