using System.Collections;
using UnityEngine;

/// <summary>
/// Lightweight camera helper, ported from the Love2D Camera module (shake/bounds/zoom).
/// In Unity, full camera control is usually handled by Cinemachine, so this stays
/// intentionally minimal: attach it to your Camera and call the public methods
/// when you need a quick shake, a clamp to level bounds, or a smooth zoom.
/// </summary>
[RequireComponent(typeof(Camera))]
public class CameraUtils : MonoBehaviour
{
    private Camera _cam;
    private Vector3 _originalPos;
    private Coroutine _shakeRoutine;

    public Rect bounds; // world-space bounds the camera should stay within (x, y = min corner)

    private void Awake()
    {
        _cam = GetComponent<Camera>();
    }

    public void Shake(float duration, float magnitude)
    {
        if (_shakeRoutine != null) StopCoroutine(_shakeRoutine);
        _shakeRoutine = StartCoroutine(ShakeRoutine(duration, magnitude));
    }

    private IEnumerator ShakeRoutine(float duration, float magnitude)
    {
        _originalPos = transform.localPosition;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            transform.localPosition = _originalPos + new Vector3(x, y, 0f);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = _originalPos;
        _shakeRoutine = null;
    }

    public void ClampToBounds()
    {
        if (bounds.width <= 0 || bounds.height <= 0) return;

        float halfHeight = _cam.orthographicSize;
        float halfWidth = halfHeight * _cam.aspect;

        float clampedX = Mathf.Clamp(transform.position.x, bounds.xMin + halfWidth, bounds.xMax - halfWidth);
        float clampedY = Mathf.Clamp(transform.position.y, bounds.yMin + halfHeight, bounds.yMax - halfHeight);

        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }

    public void ZoomTo(float targetSize, float duration)
    {
        StartCoroutine(ZoomRoutine(targetSize, duration));
    }

    private IEnumerator ZoomRoutine(float targetSize, float duration)
    {
        float startSize = _cam.orthographicSize;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            _cam.orthographicSize = Mathf.Lerp(startSize, targetSize, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        _cam.orthographicSize = targetSize;
    }
}
