using System.Collections;
using UnityEngine;

public class RaccoonAnimateDialogue : MonoBehaviour
{
    IEnumerator Start()
    {
        Animator  animator = gameObject.GetComponent<Animator>();
        while (true) {
            yield return new WaitForSeconds(10f);
            animator.SetTrigger("Scratch");
        }
    }

}
