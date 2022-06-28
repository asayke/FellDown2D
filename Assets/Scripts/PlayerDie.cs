using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDie : MonoBehaviour
{
    private void Start()
    {
        GameObject.Find("GameCanvas").transform.FindChild("EndGameE").gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag.Equals("DieLine"))
        {
            GameObject.Find("GameCanvas").transform.FindChild("EndGameE").gameObject.SetActive(true);
            Destroy(this.gameObject);

        }
    }
}
