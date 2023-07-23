using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace LightYearsChaos
{
    [Flags]
    public enum UnitType
    {
        // Starcraft, Stronghold, Age of Empires or Empire Earth units? Why not all?

        // Medieval land
        Swordsman = 0,
        Pikeman,
        Spearman,
        Archer,
        Magician,
        Priest,
        HorseRider,
        CamelRider,
        ElephantRider,
        Chariotry,
        Catapult,
        Trebuchet,

        // Modern land
        Gunner,
        LandTransport,
        Tank,
        RobotFighter,

        // Air
        FighterPlane,
        TransportPlane,

        // Sea
        Submarine,
        FighterShip,
        TransportShip,

        //Space
        SpaceFighter,
        SpaceTransport,
        
        COUNT
    }


    [Serializable]
    public class Unit : MonoBehaviour
    {
        private NavMeshAgent agent;
        private HealthComponent health;
        private MovementComponent movement;
        private WeaponComponent weapon;
        private SkillComponent skill;
        private UnitStateManager stateManager;
        private UnitSensor sensor;
        private Animator anim;

        [SerializeField] private int teamId;
        [SerializeField] private GameObject selectionObject;
        [SerializeField] private List<Skill> skillsGiven = new List<Skill>();
        [SerializeField] private List<Weapon> weaponsGiven = new List<Weapon>();
        [SerializeField] private UnitType type;
        
        public NavMeshAgent Agent { get { return agent; } }
        public HealthComponent Health { get { return health; } }
        public MovementComponent Movement { get { return movement; } }
        public SkillComponent Skill { get { return skill; } }
        public WeaponComponent Weapon { get { return weapon; } }
        public int TeamId { get { return teamId; } }
        public GameObject SelectionObject { get {  return selectionObject; } }
        public UnitType Type { get { return type; } }
        public UnitStateManager StateManager { get { return stateManager; } }
        public UnitSensor Sensor { get {  return sensor; } }
        public Animator Anim { get { return anim; } }

        public event Action OnDeath;


        private void Awake()
        {
            anim = GetComponent<Animator>();

            agent = GetComponent<NavMeshAgent>();
            agent.updateRotation = false;

            movement = GetComponent<MovementComponent>();
            movement.Setup(agent); ;

            skill = GetComponent<SkillComponent>();
            skill.Setup(this, skillsGiven);

            weapon = GetComponent<WeaponComponent>();
            weapon.Setup(this, weaponsGiven, skill);

            health = GetComponent<HealthComponent>();
            health.Setup(this);
            health.OnDeath += () => { OnDeath?.Invoke(); };

            sensor = GetComponent<UnitSensor>();
            sensor.Setup(this);

            stateManager = GetComponent<UnitStateManager>();
            stateManager.Setup(this, new IdleState(this, stateManager));

            OnDeath += () =>
            {
                gameObject.SetActive(false);
            };
        }


        private void Update()
        {
            var delta = Time.deltaTime;

            skill.Tick(delta);
            movement.Tick(delta);
            health.Tick(delta);
            stateManager.Tick(delta);
        }
    }
}