using AxGrid;
using UnityEngine;

public class ParticleEffect : MonoBehaviour
{
    [SerializeField] private Transform[] _pathPoints;
    [SerializeField] private float _speed = 1f;
    [SerializeField] private bool _disaleOnFinish;

    private int currentPointIndex = 0;

    private void OnEnable()
    {
        currentPointIndex = 0;
        transform.position = _pathPoints[currentPointIndex].position;
    }

    private void Update()
    {
        if (_pathPoints.Length > 1)
        {
            var currentPoint = _pathPoints[currentPointIndex].position;
            var nextPoint = _pathPoints[(currentPointIndex + 1) % _pathPoints.Length].position;
            var direction = nextPoint - currentPoint;

            transform.position += direction.normalized * _speed * Time.deltaTime;
            
            if (Vector2.Distance(transform.position, nextPoint) <= 0.6f)
            {
                currentPointIndex = (currentPointIndex + 1) % _pathPoints.Length;

                if (_disaleOnFinish && currentPointIndex == 0)
                {
                    Settings.Invoke(Keys.AllBonusesHaveBeenPaid);
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
