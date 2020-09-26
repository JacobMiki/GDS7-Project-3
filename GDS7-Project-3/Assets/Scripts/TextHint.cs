using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshPro))]
public class TextHint : MonoBehaviour
{
    [SerializeField] private Color _color;
    [SerializeField][Min(0.1f)] private float _fadeTime;

    private TextMeshPro _tmp;
    private Vector3 _originalPos;
    private Vector3 _startPos;
    private Vector3 _targetPos;
    private float _targetAcc;

    private Coroutine _lerping;

    void OnEnable()
    {
        _tmp = GetComponent<TextMeshPro>();
        _tmp.color = _color;
        _originalPos = transform.localPosition;
        _startPos = _originalPos;
        _targetPos = _startPos + Random.insideUnitSphere * 0.05f;
    }

    private void Update()
    {
        if (Vector3.Distance(transform.localPosition, _targetPos) <= 0.01)
        {
            _startPos = transform.localPosition;
            _targetPos = _originalPos + Random.insideUnitSphere * 0.05f;
            _targetAcc = 0f;
        }
        _targetAcc += Time.deltaTime;
        transform.localPosition = Vector3.Slerp(_startPos, _targetPos, _targetAcc);
    }

    public void SetAlpha(float alpha)
    {
        SetColor(new Color(_color.r, _color.g, _color.b, alpha));
    }

    public void SetColor(Color color)
    {
        if (_lerping != null)
        {
            StopCoroutine(_lerping);
        }
        _lerping = StartCoroutine(LerpColor(_color, color));
    }

    IEnumerator LerpColor(Color startColor, Color targetColor)
    {
        var accumulator = 0f;
        while(accumulator < _fadeTime)
        {
            accumulator += Time.deltaTime;
            _color = Color.Lerp(startColor, targetColor, accumulator / _fadeTime);

            _tmp.color = _color;

            yield return null;
        }
        _lerping = null;
    }
}
