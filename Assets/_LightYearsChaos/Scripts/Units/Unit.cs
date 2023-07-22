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
        
        [SerializeField] private int teamId;
        [SerializeField] private GameObject selectionObject;
        [SerializeField] private List<Skill> skillsGiven = new List<Skill>();
        [SerializeField] private List<Weapon> weaponsGiven = new List<Weapon>();
        [SerializeField] private UnitType type;
        [SerializeField] private UnitSensor sensor;

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


        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();

            movement = GetComponent<MovementComponent>();
            movement.Setup(agent);;

            skill = GetComponent<SkillComponent>();
            skill.Setup(skillsGiven);

            weapon = GetComponent<WeaponComponent>();
            weapon.Setup(weaponsGiven, skill);

            health = GetComponent<HealthComponent>();
            health.Setup(this);

            stateManager = GetComponent<UnitStateManager>();
            stateManager.Setup(this, new IdleState(this, stateManager));

            sensor.Setup(teamId, weapon.MaxFiringRange * 2);
        }


        private void Update()
        {
            var delta = Time.deltaTime;

            skill.Tick(delta);
            movement.Tick(delta);
            health.Tick(delta);
            stateManager.Tick(delta);
        }


        public void Attack (Unit target)
        {
            foreach (var weapon in Weapon.Weapons)
            {
                weapon.Fire(target);
            }
        }
    }
}