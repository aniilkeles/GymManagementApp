using System;

using System.Collections.Generic;

using System.Linq;

using System.Text;

using System.Threading.Tasks;



namespace GymManagementApp.Models

{

    internal class WorkoutPlan

    {

        private int sessionsPerWeek;



        public int Id { get; set; }



        public string PlanName { get; set; }



        public Trainer Trainer { get; set; }



        public int SessionsPerWeek

        {

            get { return sessionsPerWeek; }

            private set

            {

                if (value >= 1 && value <= 7)

                {

                    sessionsPerWeek = value;

                }

            }

        }



        public WorkoutPlan(int id, string planName, Trainer trainer, int sessionsPerWeek)

        {

            Id = id;

            PlanName = planName;

            Trainer = trainer;

            SetSessionsPerWeek(sessionsPerWeek);

        }



       

        public void SetSessionsPerWeek(int value)

        {

            SessionsPerWeek = value;

        }

        public override string ToString()

        {

            return $"{PlanName} - {SessionsPerWeek} days/week (Trainer: {Trainer.FullName})";

        }



    }

}