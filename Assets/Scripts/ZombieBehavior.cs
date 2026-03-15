using UnityEngine;

public class ZombieBehavior : MonoBehaviour
{
    private Animator animator;
    private Rigidbody[] bones;

    void Start()
    {
        animator = GetComponent<Animator>();
        bones = GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody bone in bones)
        {
            bone.isKinematic = true; 
        }
    }

    public void TriggerRagdoll()
    {
        animator.enabled = false; 

        foreach (Rigidbody bone in bones)
        {
            bone.isKinematic = false; 
        }
    }
}