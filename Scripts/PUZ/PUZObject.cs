using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;
using System;
using UnityEngine.AI;
using PUZ.Timer;

namespace PUZ.Behaviour
{
    public enum PuzType
    {
        Blue = 0,
        Purple,
        Yellow,
        Green

    }

    public abstract class PUZObject : MonoBehaviour
    {
        protected StateMachine _stateMachine;
        [SerializeField]
        protected PlayerDetector _playerDetector;
        [SerializeField]
        protected AudioSource _audioSource;
        [SerializeField]
        private PuzzStats _stats;
        [SerializeField]
        private PuzzAIConfig _aiConfig;
        [SerializeField]
        private PuzzPlayerDetectionConfig _playerDetectionConfig;
        [SerializeField]
        protected Animator _animator;

        public PuzType PuzTypeValue  { get; set; } 

        private float _health;
        private float _walkingSpeed = 0.3f;
        private float _runningSpeed = 30;
        private float _stamina;

        private float _visibility;
        private float _fearLevel;
        private float _hunger;

        protected bool _isDetected = false;
        public bool IsAlerted  { get; set; }
        public bool IsScared  { get; set; }
        protected bool _isFlying = false;
        public bool IsWalking  { get; set; }
        protected bool _isHiding = false;
        protected bool _isRunning = false;
        protected bool _isIdling = false;
        protected bool _isCaptured = false;

        public bool IsFleeing { get; set; }

        Dance dance;
        Hide hide;
        Joke joke;
        Run run;
        Scared scared;
        Walk walk;
        Alert alert;
        Idle idle; 

        protected NavMeshAgent _navMeshAgent;
        public NavMeshAgent GetAgent => _navMeshAgent;

        public float timerForFleeing = 10f;
        [SerializeField]
        FleeTimer _timeOutTimer;

        protected void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _timeOutTimer = GetComponent<FleeTimer>();
            _playerDetector = GetComponent<PlayerDetector>();
            
            _playerDetector.StartPlayerDetection();
            
            _stateMachine = new StateMachine();

            dance = new Dance(_animator, this);
            hide = new Hide(_animator, this);
            joke = new Joke(_animator, this);
            run = new Run(_animator,this);
            scared = new Scared(_animator, this);
            walk = new Walk(_animator, this);
            alert = new Alert(_animator, this);
            idle = new Idle(_animator, this);

            At(scared, run,  () => IsScared == true && IsFleeing == false && AnimatorFinishedPlaying("Scared") );
            At(alert, scared, () => IsAlerted == true && IsScared == true && AnimatorFinishedPlaying("ReactToPlayer") );
            At(alert, run,  () => IsAlerted == true && IsFleeing == false && AnimatorFinishedPlaying("ReactToPlayer"));
            At(walk, alert,  () => IsWalking == true && IsAlerted == true);
            At(walk, scared,  () => IsWalking == true && IsScared == true);
            At(alert, walk,  () => IsWalking == true && IsAlerted == false);
            At(run, walk, () => IsFleeing == false && RandomnessAction() > 0.5f);
            At(run, idle, () => IsFleeing == false && RandomnessAction() < 0.5f);
            At(idle, walk, () => IsWalking == false && RandomnessAction() < 0.2f);
            At(idle, alert, () => IsAlerted == true); 

            IsFleeing = false;
        }

        protected void Start() => StartStateMachine();

        protected void StartStateMachine() => _stateMachine.SetState(walk);

        void At(IState to, IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);

        protected void Update()
        {
            _stateMachine.Tick();
            _stateMachine.DebugState();
        }

        public void Hide() {}

        public void Walk() 
        {
            _navMeshAgent.speed = _walkingSpeed;
            _animator.Play("Walking2");
        }

        public void Run() 
        {
            _navMeshAgent.speed = _runningSpeed;
            _animator.Play("Run");
            IsFleeing = true;
           
            _timeOutTimer.StartTimer(10f, StopRun);
        }

        private void StopRun()
        {
            IsFleeing = false;
            _timeOutTimer.StopTimer();
            IsAlerted = false;
            IsScared = false;
        } 

        public void Dance() {}

        public void Cook() {}

        public void Eat() {}

        public void Drink() {}

        public void Interact() {}

        public void Scared()
        {
            _animator.Play("Scared");
        }
        
        public void Alerted()
        {
            _animator.Play("ReactToPlayer");
        }

        public void Idling()
        {
            _animator.Play("Idle");
        }

        public Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask) 
        {
            Vector3 randDirection = UnityEngine.Random.insideUnitSphere * dist;
    
            randDirection += origin;
    
            NavMeshHit navHit;
    
            NavMesh.SamplePosition (randDirection, out navHit, dist, layermask);
    
            return navHit.position;
        }

        public Vector3 LastKnownPlayerPosition() => _playerDetector.CurrentPlayerPosition();

        private void LoadStats()
        {
            _health = _stats.Health;
            _walkingSpeed = _stats.WalkingSpeed;
            _runningSpeed = _stats.RunningSpeed;
            _stamina = _stats.Stamina;

            _visibility = _stats.Visibility;
            _fearLevel = _stats.FearLevel;
            _hunger = _stats.Hunger;
        }

        private void LoadAIConfig()
        {
            _health = _stats.Health;
            _walkingSpeed = _stats.WalkingSpeed;
            _runningSpeed = _stats.RunningSpeed;
            _stamina = _stats.Stamina;

            _visibility = _stats.Visibility;
            _fearLevel = _stats.FearLevel;
            _hunger = _stats.Hunger;
        }

        private void PlayerDetectionConfig()
        {
            _health = _stats.Health;
            _walkingSpeed = _stats.WalkingSpeed;
            _runningSpeed = _stats.RunningSpeed;
            _stamina = _stats.Stamina;

            _visibility = _stats.Visibility;
            _fearLevel = _stats.FearLevel;
            _hunger = _stats.Hunger;
        }

        public void SetStatsFile(PuzzStats file) => _stats = file;

        public void SetAIConfigFile(PuzzAIConfig file) => _aiConfig = file;

        public void SetPuzzleConfigFile(PuzzPlayerDetectionConfig file) => _playerDetectionConfig = file;

        protected float RandomnessAction() => UnityEngine.Random.Range(0f, 1f);

        public void PlayActionPointOfInterest(PointOfInterest pointOfInterest)
        {
            switch(pointOfInterest)
            {
                case PointOfInterest.Cooking: break;
                case PointOfInterest.Dancing: break;
                case PointOfInterest.Interact: break;
                case PointOfInterest.Drink: break;
                case PointOfInterest.Eat: break;
                default : break;
            }
        }

        bool AnimatorFinishedPlaying(string stateName) => AnimatorFinishedPlaying() && _animator.GetCurrentAnimatorStateInfo(0).IsName(stateName);

        bool AnimatorFinishedPlaying() => _animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f;
    }
}