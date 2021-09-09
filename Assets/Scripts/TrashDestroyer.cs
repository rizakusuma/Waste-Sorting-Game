using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TrashDestroyer : MonoBehaviour
{
    [HideInInspector] public UnityEvent<Trash> OnTrashDestroy = new UnityEvent<Trash>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Trash")
        {
            Trash trash = collision.GetComponent<Trash>();
            if (trash)
            {
                OnTrashDestroy.Invoke(trash);
            }
        }
    }
}
