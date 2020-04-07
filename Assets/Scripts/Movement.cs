using AI;
using System.Collections.Generic;
using System.Linq;
using Tools;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] MapScriptable _mapScriptable = null;
    [SerializeField] EnemyTypesScriptable _enemyTypes = null;

    private Vector3 _startPoint, _endPoint = default;
    private Dijkstra _dijkstra = null;
    private List<Vector2Int> _path;
    private bool _reachNextStep = false;

    private Animator _animator;
    private float _mPivot;

    private void Awake()
    {
        if (_mapScriptable == null)
            throw new MissingReferenceException("Missing reference of MapScriptable object.");

        if (_enemyTypes == null)
            throw new MissingReferenceException("Missing reference of EnemyTypes Scriptable Object.");

        _animator = GetComponent<Animator>();

        _mPivot = Mathf.Abs(transform.GetChild(1).transform.position.y) ;
    }
    private void OnEnable()
    {
        if (_mapScriptable != null)
        {
            _startPoint = _mapScriptable.StartPoint;
            _endPoint = _mapScriptable.EndPoint;
        }

        _startPoint += new Vector3(0, _mPivot, 0);
        _endPoint += new Vector3(0, _mPivot, 0);

        List<Vector2Int> accesibles = _mapScriptable.Map.GridCells.Where(m => _mapScriptable.MapWalkableDictionary[m.ObjectType]).Select(m => new Vector2Int(m.XPos2D, m.YPos2D)).ToList();
        _dijkstra = new Dijkstra(accesibles);

        _path = _dijkstra.FindPath(_startPoint.ToVector2Int(_mapScriptable.CellSize), _endPoint.ToVector2Int(_mapScriptable.CellSize)).ToList();
        transform.position = _startPoint; 
    }

    private void Update()
    {
        if (_path.Any())
        {
            if (transform.position == _path.Last().ToVector3(_mPivot,_mapScriptable.CellSize))
            {
                _animator.SetBool("isWalking", false);
                _reachNextStep = true;
                _path.Remove(_path.Last());
            }
            else if (_reachNextStep)
            {
                transform.LookAt(_path.Last().ToVector3(_mPivot,_mapScriptable.CellSize));
                _animator.SetBool("isWalking", true);
                transform.position = Vector3.MoveTowards(transform.position, _path.Last().ToVector3(_mPivot, _mapScriptable.CellSize), _enemyTypes._speed * Time.deltaTime);
            }
        }

        if (transform.position == _endPoint)
        {
            transform.position = _startPoint;
            _animator.SetBool("isWalking", false);
            _reachNextStep = false;
            gameObject.SetActive(false);
        }
    }
}
