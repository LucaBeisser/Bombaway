using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField]
    float radius = 1f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Bomb bomb = collision.collider.GetComponent<Bomb>();

        if (bomb != null)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);

            for (int i = 0; i < colliders.Length; i++)
            {
                IAffectable affectable = colliders[i].GetComponent<IAffectable>();

                if (affectable != null)
                {
                    affectable.OnActivate();
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
