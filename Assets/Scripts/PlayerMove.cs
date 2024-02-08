using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("Move Settings")]
    [SerializeField] private float _moveSpeed = 1f;
    [SerializeField] private float _rotationSpeed = 270f;
    [SerializeField] private float _minDistance = 0.1f;

    [Header("Route & Prefab")]
    [SerializeField] private LineRenderer _routeLine;
    [SerializeField] private GameObject _playerPrefab;

    [Header("Score")]
    [SerializeField] private TMP_Text _scoreText;
    private int _score = 0;
    private int _pointsForCompletion = 500;

    private Vector3[] _routePoints;
    private int _currentRouteIndex = 0;
    private bool _routeCompleted = false;

    private const int START_INDEX = 0;
    private const int NEXT_POINT_INDEX = 1;

    private void Start()
    {
        InitializeRoutePoints();
        UpdateScoreText();
    }

    private void Update()
    {
        CheckMove();
        CheckRoute();
    }
    private void CheckMove()
    {
        if (!_routeCompleted && _routePoints != null && _routePoints.Length > NEXT_POINT_INDEX)
        {
            if (Input.GetMouseButton(0) || Input.touchCount > 0)
            {
                Move();
            }

        }
    }

    private void CheckRoute()
    {
        if (!_routeCompleted)
        {
            UpdateRouteLine();
        }
    }

    private void InitializeRoutePoints()
    {
        _routePoints = new Vector3[_routeLine.positionCount];
        _routeLine.GetPositions(_routePoints);
        _routePoints[START_INDEX] = _playerPrefab.transform.position;
    }

    private void Move()
    {
        Vector3 currentPosition = _playerPrefab.transform.position;

        if (_routePoints == null || _currentRouteIndex >= _routePoints.Length)
        {
            return;
        }

        Vector3 targetPosition = _routePoints[_currentRouteIndex];
        _playerPrefab.transform.position = Vector3.MoveTowards(currentPosition, targetPosition, _moveSpeed * Time.deltaTime);

        Vector3 destDirection = targetPosition - currentPosition;
        destDirection.y = 0;
        float destDistance = destDirection.magnitude;

        if (currentPosition != targetPosition)
        {
            if (destDistance >= _minDistance)
            {
                Quaternion targetRotation = Quaternion.LookRotation(destDirection);
                _playerPrefab.transform.rotation = Quaternion.RotateTowards(_playerPrefab.transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
            }
        }
        else
        {
            _currentRouteIndex++;

            if (_currentRouteIndex >= _routePoints.Length)
            {
                _routeCompleted = true;
                _score += _pointsForCompletion;
                UpdateScoreText();
            }
        }
    }

    private void RemoveRoutePoint(int indexToRemove)
    {
        if (indexToRemove < START_INDEX || indexToRemove >= _routePoints.Length)
        {
            return;
        }

        List<Vector3> newRoutePointsList = new List<Vector3>(_routePoints);
        newRoutePointsList.RemoveAt(indexToRemove);
        _routePoints = newRoutePointsList.ToArray();

        _routeLine.positionCount = _routePoints.Length;
        _routeLine.SetPositions(_routePoints);
    }

    private void UpdateRouteLine()
    {
        _routePoints[START_INDEX] = _playerPrefab.transform.position;
        _routeLine.SetPosition(START_INDEX, _routePoints[START_INDEX]);

        for (int i = NEXT_POINT_INDEX; i < _routeLine.positionCount; i++)
        {
            if (_routePoints[i] == _routePoints[START_INDEX])
            {
                RemoveRoutePoint(i);
                break;
            }
        }
    }

    private void UpdateScoreText()
    {
        if (_scoreText != null)
        {
            _scoreText.text = "Score: " + _score.ToString();
        }
    }
}