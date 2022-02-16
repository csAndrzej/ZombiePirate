using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour
{
    private SpriteRenderer mSpriteRenderer;
    private Color baseColor;

    public void RunEffect(SpriteRenderer sr, Color bc)
    {
        mSpriteRenderer = sr;
        baseColor = bc;
        StartCoroutine("CastDamageEffect");
    }

    IEnumerator CastDamageEffect()
    {
        
        mSpriteRenderer.color = Color.red;

        for (float time = 0; time < 1.0f; time += Time.deltaTime / 1)
        {
            mSpriteRenderer.color = Color.Lerp(Color.red, baseColor, time);
            yield return null;
        }

        mSpriteRenderer.color = baseColor;

    }
}
