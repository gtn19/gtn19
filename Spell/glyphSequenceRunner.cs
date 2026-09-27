using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class glyphSequenceRunner : MonoBehaviour
{
    public float speed;
    public IEnumerator ExecuteSequence(List<Glyph> glyphs, GameObject caster, GameObject shapeInstance)
    {
        shapeInstance.AddComponent<SpellHitDetector>();
        Vector3 currentPosition = caster.transform.position;
        foreach (var glyph in glyphs)
        {
            bool hitOccurred = false;
            GameObject hitTarget = null;
            Vector3 targetPos = currentPosition + glyph.direction * glyph.movementDistance;
            float duration = glyph.movementDistance / speed;
            Coroutine movement = StartCoroutine(MoveToPos(shapeInstance, targetPos, duration,
                onHit: (target) => { hitOccurred = true; hitTarget = target; }));

            yield return movement;
            currentPosition = shapeInstance.transform.position;
            if (hitOccurred)
            {
                foreach (var effect in glyph.hitEffects)
                    effect.Apply(caster, hitTarget);

                yield break;
            }
        }
    }
    private IEnumerator MoveToPos(GameObject spellShape, Vector3 targetPos, float duration, Action<GameObject> onHit)
    {
        bool hit = false;
        GameObject hitTarget = null;

        var detector = spellShape.GetComponent<SpellHitDetector>();
        detector.onHit = (target) => { hit = true; hitTarget = target; };
        Vector3 startPosition = spellShape.transform.position;
        float elapsedTime = 0f;
        while (elapsedTime < duration && !hit)
        {
            spellShape.transform.position = Vector3.Lerp(startPosition, targetPos, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        if (hit)
            onHit?.Invoke(hitTarget);
    }
}
