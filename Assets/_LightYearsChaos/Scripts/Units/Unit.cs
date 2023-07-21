using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace LightYearsChaos
{
    public class Unit : MonoBehaviour
    {
        private NavMeshAgent agent;
        private MovementComponent movement;
        private WeaponComponent weapon;
        private SkillComponent skill;
        
        [SerializeField] private int teamId;
        [SerializeField] private GameObject selectionObject;
        [SerializeField] private List<Skill> skillsGiven = new List<Skill>();
        [SerializeField] private List<Weapon> weaponGiven = new List<Weapon>();


        public MovementComponent Movement { get { return movement; } }
        public SkillComponent Skill { get { return skill; } }
        public WeaponComponent Weapon { get { return weapon; } }
        public int TeamId { get { return teamId; } }
        public GameObject SelectionObject { get {  return selectionObject; } }


        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();

            movement = GetComponent<MovementComponent>();
            movement.Setup(agent);

            skill.Setup(skillsGiven);

            weapon.Setup(weaponGiven);
        }


        private void Update()
        {
            Skill.Tick(Time.deltaTime);
        }


        public void Attack (Unit target)
        {

        }
    }
}