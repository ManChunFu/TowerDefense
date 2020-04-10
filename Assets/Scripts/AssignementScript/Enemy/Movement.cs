using AI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tools;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] MapScriptable m_MapScriptable = null;
    [SerializeField] EnemyTypesScriptable m_EnemyTypes = null;

    private Vector3 m_StartPoint, m_EndPoint = default;
    private Dijkstra m_Dijkstra = null;
    private List<Vector2Int> m_Path;
    private bool m_ReachNextStep = false;
    private Health m_Health = null;
    private Animator m_Animator = null;
    private float m_Pivot = 0;
    private int m_Speed = 0;
    public bool IsDead = false;

    private void Awake()
    {
        if (m_MapScriptable == null)
            throw new MissingReferenceException("Missing reference of MapScriptable object.");

        if (m_EnemyTypes == null)
            throw new MissingReferenceException("Missing reference of EnemyTypes Scriptable Object.");

        if (m_EnemyTypes != null)
            m_Speed = m_EnemyTypes.Speed;

        m_Health = GetComponent<Health>();

        m_Animator = GetComponent<Animator>();

        m_Pivot = Mathf.Abs(transform.GetChild(1).transform.position.y) ;
    }
    private void OnEnable()
    {
        m_Health.OnDead += Die;
        m_Health.OnHealthChanged += Damage;

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

        m_Path = m_Dijkstra.FindPath(m_StartPoint.ToVector2Int(m_MapScriptable.CellSize), m_EndPoint.ToVector2Int(m_MapScriptable.CellSize)).ToList();
        transform.position = m_StartPoint;

        m_Speed = m_EnemyTypes.Speed;
        IsDead = false;
    }

    private void Update()
    {
        if (m_Path.Any())
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
            m_Animator.SetBool("isWalking", false);
            m_ReachNextStep = false;
            gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        m_Health.OnDead -= Die;
        m_Health.OnHealthChanged -= Damage;
    }

    private void Die(bool isDead)
    {
        IsDead = isDead;
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
        if (!IsDead)
        {
            StartCoroutine(SlowMotion(slowDown, affectTime));
        }
    }
    private IEnumerator SlowMotion(int slowDown, float affectTime)
    {
        if (IsDead)
        {
            yield break; 
        }

        m_Speed = slowDown;
        yield return new WaitForSeconds(affectTime);
        m_Speed = m_EnemyTypes.Speed;
    }

    private void Damage(int healthPoint)
    {
        if (IsDead)
        {
            return;
        }
        m_Animator.SetTrigger("Damaged");
    }
}
