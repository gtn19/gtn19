using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneSequenceRunner : MonoBehaviour
{
    public IEnumerator ExecuteSequence(List<Rune> runes, GameObject caster)
    {
        Vector3 currentPosition = caster.transform.position;

        GameObject spellShape;
        if (runes[0].isStartRune)
        {
            spellShape = Instantiate(runes[0].startRunePrefab);
            spellShape.transform.position = currentPosition;


            foreach (var rune in runes)
            {
                if (rune.hasMovement)
                {
                    Vector3 targetPosition = currentPosition + rune.movementOffset;
                    yield return StartCoroutine(MoveToPos(spellShape, targetPosition, rune.moveDuration));
                }

                yield return null;
            }
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
