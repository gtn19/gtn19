using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class glyphSequenceRunner : MonoBehaviour
{
    public IEnumerator ExecuteSequence(List<Glyph> glyphs, GameObject caster)
    {
        Vector3 currentPosition = caster.transform.position;

        GameObject spellShape;
        foreach (var glyph in glyphs)
        {
            Vector3 targetPosition = currentPosition + glyph.direction * glyph.movementDistance;
            yield return StartCoroutine(MoveToPos(spellShape, targetPosition, glyph.movementDuration));
        }
    }

    private IEnumerator MoveToPos(GameObject spellShape, Vector3 targetPos, float duration)
    {
        Vector3 startPosition = spellShape.transform.position;
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            spellShape.transform.position = Vector3.Lerp(startPosition, targetPos, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

    }
}
