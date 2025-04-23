using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;
using Sequence = DG.Tweening.Sequence;

public class DoTweenTesting : MonoBehaviour
{
    [SerializeField] private float _endScale;
    [Space()]
    [SerializeField] private Vector3 _endValue;
    [SerializeField] private float _animationTime;
    [SerializeField] private Ease _ease;
    [SerializeField] private MeshRenderer _meshRenderer;
    
    private Sequence _sequence;
    private List<Color> _colors = new List<Color>();
    
    private void Start()
    {
        _sequence = DOTween.Sequence();
        
        _sequence.Append(transform.DOMove(_endValue, _animationTime).SetEase(_ease));
        _sequence.Join(transform.DOScale(_endValue, _animationTime).SetEase(_ease));
        _sequence.AppendCallback(()=> print("Hello World!"));
        _sequence.AppendInterval(2f);
        _sequence.Append(_meshRenderer.material.DOColor(Color.red, _animationTime).SetEase(_ease));
        
        _colors.GetRandom();
    }

    private void OnDestroy()
    {
        _sequence?.Kill();
    }
    
}

public static class CollectionExtension
{
    public static T GetRandom<T>(this IEnumerable<T> collection)
    {
        var enumerable = collection.ToList();
        var randomIndex = Random.Range(0, enumerable.Count());
        return enumerable[randomIndex];
    }
}
