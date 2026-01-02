using System;

using System.Collections.Generic;

using System.Linq;

using System.Text;

using System.Threading.Tasks;



namespace GymManagementApp.Models

{

    internal class Trainer

    {

        public int Id { get; set; }



        public string FullName { get; set; }



        public string Specialty { get; set; }

        public string AssignedPlanName { get; set; }

        public Trainer(int id, string fullName, string specialty, string assignedPlanName)

        {

            Id = id;

            FullName = fullName;

            Specialty = specialty;
            AssignedPlanName = assignedPlanName;
        }
        public override string ToString()
        {
            return string.IsNullOrEmpty(AssignedPlanName)
                ? FullName
                : $"{FullName} ({AssignedPlanName})";
        }
    }

}