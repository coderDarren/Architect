using UnityEngine;
using System.Collections;

public class CharacterAnimController : MonoBehaviour {

    CharacterMovement character;
    Animator animator;

    void Start()
    {
        character = GetComponent<CharacterMovement>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        animator.SetBool("Grounded", character.grounded);
        animator.SetFloat("Running", character.forwardInput);
        //animator.SetFloat("Walking", character.walkInput);
    }
}
