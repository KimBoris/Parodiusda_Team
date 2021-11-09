using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWeapon : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)   //æ∆¿Ã≈€¿Ã æÓµÚ∞°ø° ∫Œµ˙«˚¿ª ∂ß
    {
        IGetItem item = GameObject.Find("StageManager").GetComponent<IGetItem>();

        if (collision.tag == "Player")
        {
            item.GetItem(1);
            this.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        this.transform.position += new Vector3(-1,0,0) * Time.deltaTime * 1.6f;
    }
}