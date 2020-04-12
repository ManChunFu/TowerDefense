using AI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tools;
using UnityEngine;

[RequireComponent(typeof(HealthObserverable))]
public class Movement : MonoBehaviour
{
    [SerializeField] private MapScriptable m_MapScriptable = null;
    [SerializeField] private EnemyTypesScriptable m_EnemyTypes = null;
    [SerializeField] private int m_Damage = 1; 

    private HealthObserverable m_HealthObserverable;
    private CompositeDisposable m_Disposables = new CompositeDisposable();
    private Animator m_Animator = null;
    private Vector3 m_StartPoint, m_EndPoint = default;
    private Dijkstra m_Dijkstra = null;
    private List<Vector2Int> m_Path;
    private PlayerTower m_PlayerTower = null;
    private bool m_ReachNextStep = false;
    private float m_Pivot = 0;
    private int m_Speed = 0;
    private bool m_isDead = false;
   

    private void Awake()
    {
        if (m_MapScriptable == null)
        {
            throw new MissingReferenceException("Missing reference of MapScriptable object.");
        }

        if (m_EnemyTypes == null)
        {
            throw new MissingReferenceException("Missing reference of EnemyTypes Scriptable Object.");
        }

        if (m_EnemyTypes != null)
        {
            m_Speed = m_EnemyTypes.Speed;
        }

        m_HealthObserverable = GetComponent<HealthObserverable>();

        m_Animator = GetComponent<Animator>();

        m_Pivot = Mathf.Abs(transform.GetChild(1).transform.position.y) ;
    }
    private void OnEnable()
    {
        m_isDead = false;
        if (m_HealthObserverable != null)
        {
            m_HealthObserverable.Health.Subscribe(Damage).AddTo(m_Disposables);
            m_HealthObserverable.Health.Where(health => health <= 0).Subscribe(Die).AddTo(m_Disposables);
        }

        if (m_MapScriptable != null)
        {
            m_StartPoint = m_MapScriptable.StartPoint;
            m_EndPoint = m_MapScriptable.EndPoint;
        }

        m_StartPoint += new Vector3(0, m_Pivot, 0);
        m_EndPoint += new Vector3(0, m_Pivot, 0);

        List<Vector2Int> accesibles = m_MapScriptable.Maps.GridCells.
            Where(m => m_MapScriptable.m_MapWalkableDictionary[m.ObjectType]).
            Select(m => new Vector2Int(m.XPos2D, m.YPos2D)).ToList();

        m_Dijkstra = new Dijkstra(accesibles);

        m_Path = m_Dijkstra.FindPath(m_StartPoint.ToVector2Int(m_MapScriptable.CellSize), 
            m_EndPoint.ToVector2Int(m_MapScriptable.CellSize)).ToList();

        transform.position = m_StartPoint;

        m_Speed = m_EnemyTypes.Speed;
    }

    private void Start()
    {
        m_PlayerTower = FindObjectOfType<PlayerTower>();
    }

    private void Update()
    {
        if (m_Path?.Any()??false)
        {
            if (transform.position == m_Path.Last().ToVector3(m_Pivot,m_MapScriptable.CellSize))
            {
                m_Animator.SetBool("isWalking", false);
                m_ReachNextStep = true;
                m_Path.Remove(m_Path.Last());
            }
            else if (m_ReachNextStep)
            {
                transform.LookAt(m_Path.Last().ToVector3(m_Pivot,m_MapScriptable.CellSize));
                m_Animator.SetBool("isWalking", true);
                transform.position = Vector3.MoveTowards(transform.position, m_Path.Last().ToVector3(m_Pivot, m_MapScriptable.CellSize), m_Speed * Time.deltaTime);
            }
        }

        if (transform.position == m_EndPoint)
        {
            if (m_PlayerTower != null)
            {
                m_PlayerTower.TakeDamage(m_Damage);
            }
            m_Animator.SetBool("isWalking", false);
            m_ReachNextStep = false;
            gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        m_Disposables?.Dispose();
    }

    private void Die(int value)
    {
        m_isDead = true;
        m_Animator.SetTrigger("Killed");
        m_ReachNextStep = false;
        Invoke("BackToPool", 1.0f);
    }

    private void BackToPool()
    {
        m_Animator.Rebind();
        gameObject.SetActive(false);
    }

    public void SlowDownImpact(int slowDown, float affectTime)
    {
        if (!m_isDead)
        {
            StartCoroutine(SlowMotion(slowDown, affectTime));
        }
    }
    private IEnumerator SlowMotion(int slowDown, float affectTime)
    {
        if (m_isDead)
        {
            yield break; 
        }

        m_Speed = slowDown;
        yield return new WaitForSeconds(affectTime);
        m_Speed = m_EnemyTypes.Speed;
    }

    private void Damage(int healthPoint)
    {
        if (m_isDead)
        {
            return;
        }
        m_Animator.SetTrigger("Damaged");
    }
}
