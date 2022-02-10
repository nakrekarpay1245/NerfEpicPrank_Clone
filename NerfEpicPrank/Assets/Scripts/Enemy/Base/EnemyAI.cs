using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{
    #region STATE PARAMETERS
    [Tooltip("Durum Makinesi")]
    [Header("Durum Makinesi ve Durumlar")]
    public StateMachine stateMachine;

    private ChaseState chaseState;
    private IdleState idleState;
    private SuspicionState suspicionState;
    private SearchState searchState;
    #endregion

    #region COMPONENT PARAMETERS
    [Header("Components")]
    [HideInInspector] public FieldOfView fieldOfView;
    [HideInInspector] public EnemyHealth enemyHealth;
    [HideInInspector] public Animator animator;
    [HideInInspector] public AudioSource audioSource;

    [Header("Düşman Obje")]
    [HideInInspector] public Transform target;
    #endregion

    #region OTHER PARAMETERS
    [Header("Hareket Hızı")]
    public float moveSpeed;

    [Header("Dönme Hızı")]
    public float rotateSpeed;

    [Header("Alarm Değişkenleri")]
    [Tooltip("Chase' e geçerken alarm süresi")]
    public float chaseAlarmTime;

    [Tooltip("Suspicion' a geçerken alarm süresi")]
    public float suspicionAlarmTime;

    [Tooltip("Search' a geçerken alarm süresi")]
    public float searchAlarmTime;

    [Tooltip("Alarm ibaresi")]
    public GameObject alarmDisplay;

    [Tooltip("Alarm seviyesi göstergesi")]
    public Image alarmImage;

    [Tooltip("Durma anında çalınacak ses")]
    public AudioClip idleClip;

    [Tooltip("Koşma anında çalınacak ses")]
    public AudioClip runClip;

    [Tooltip("Etrafında dönme(bizi arama) anında çalınacak ses")]
    public AudioClip searchClip;

    [Tooltip("Şüphelenme anında çalınacak ses")]
    public AudioClip suspicionClip;

    [Tooltip("Can ve uyarı göstergeleri için yerel Canvas")]
    public GameObject localCanvas;
    #endregion

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponentInChildren<Animator>();
        fieldOfView = GetComponent<FieldOfView>();
        enemyHealth = GetComponent<EnemyHealth>();
        target = fieldOfView.target.transform;
        alarmImage.fillAmount = 0;
        StateGenerator();
        Idle();
    }
    private void Update()
    {
        stateMachine.UpdateStates();
        localCanvas.transform.rotation = Camera.main.transform.rotation;
    }

    private void FixedUpdate()
    {
        stateMachine.FixedUpdateStates();
    }

    #region STATE CHANGERS
    public void Chase()
    {
        stateMachine.ChangeStates(chaseState);
    }
    public void Idle()
    {
        stateMachine.ChangeStates(idleState);
    }
    public void Suspicion()
    {
        stateMachine.ChangeStates(suspicionState);
    }
    public void Search()
    {
        stateMachine.ChangeStates(searchState);
    }
    #endregion


    #region CALLBACKS
    private void ChaseCallback(int index)
    {
        if (index == 0)
        {
            Idle();
        }
        else if (index == 2)
        {
            Suspicion();
        }
        else if (index == 4)
        {
            Search();
        }
        else
        {
            Chase();
        }
    }

    private void IdleCallback(int index)
    {
        if (index == 2)
        {
            Suspicion();
        }
        else if (index == 3)
        {
            Chase();
        }
        else if (index == 4)
        {
            Search();
        }
        else
        {
            Idle();
        }
    }

    private void SuspicionCallback(int index)
    {
        if (index == 0)
        {
            Idle();
        }
        else if (index == 3)
        {
            Chase();
        }
        else if (index == 4)
        {
            Search();
        }
        else
        {
            Suspicion();
        }
    }
    private void SearchCallback(int index)
    {
        if (index == 0)
        {
            Idle();
        }
        else if (index == 2)
        {
            Suspicion();
        }
        else if (index == 3)
        {
            Chase();
        }
        else
        {
            Search();
        }
    }

    #endregion


    #region NEW STATES  
    private void StateGenerator()
    {
        NewChase();
        NewIdle();
        NewSuspicion();
        NewSearch();
    }

    private ChaseState NewChase()
    {
        chaseState = new ChaseState(ChaseCallback, audioSource, animator, searchClip, moveSpeed,
            fieldOfView, enemyHealth, this);
        return chaseState;
    }
    private IdleState NewIdle()
    {
        idleState = new IdleState(IdleCallback, audioSource, animator, idleClip, alarmDisplay,
            fieldOfView, enemyHealth, this);
        return idleState;
    }
    private SuspicionState NewSuspicion()
    {
        suspicionState = new SuspicionState(SuspicionCallback, audioSource, animator,
            suspicionClip, rotateSpeed, alarmDisplay, alarmImage, fieldOfView, enemyHealth,
            this);
        return suspicionState;
    }
    private SearchState NewSearch()
    {
        searchState = new SearchState(SearchCallback, audioSource, animator,
            searchClip, rotateSpeed, alarmDisplay, alarmImage, fieldOfView, enemyHealth,
            this);
        return searchState;
    }
    #endregion

}
