using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetController : MonoBehaviour
{
    public Text scoreText;

    int score; // default 0

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            score = score + 1;

            scoreText.text = "Hits: " + score;
        }
    }
}
