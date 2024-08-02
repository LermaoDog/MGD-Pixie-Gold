using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Supplies : MonoBehaviour
{
    // Start is called before the first frame update
    public int suppliesValue = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SuppliesManager.Instance.ChangeScore(suppliesValue);
        }
    }
}
