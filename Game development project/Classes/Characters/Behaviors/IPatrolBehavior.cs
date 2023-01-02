using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_development_project.Classes.Characters.Behaviors
{
    internal interface IPatrolBehavior
    {
        public float CurrentDistance { get; set; }
        public float PatrolDistance { get; set; }
        public float ChasingSpeed { get; set; }
        void Patrol();
    }
}
