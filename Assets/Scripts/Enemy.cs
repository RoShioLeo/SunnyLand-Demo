using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator animator;
    protected AudioSource audioSource;

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void Kill()
    {
        animator.SetTrigger("kill");
        audioSource.Play();
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
