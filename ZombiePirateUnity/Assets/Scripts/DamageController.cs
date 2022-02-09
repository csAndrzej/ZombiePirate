using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour
{
    private SpriteRenderer mSpriteRenderer;

    void Awake()
    {
        mSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Run()
    {
        StartCoroutine("CastDamageEffect");
    }

    IEnumerator CastDamageEffect()
    {
        // Original colour of the sprite 
        Color baseColor = mSpriteRenderer.color;
        
        mSpriteRenderer.color = Color.red;

        for (float time = 0; time < 1.0f; time += Time.deltaTime / 1)
        {
            mSpriteRenderer.color = Color.Lerp(Color.red, baseColor, time);
            yield return null;
        }

        mSpriteRenderer.color = baseColor;
    }
}
