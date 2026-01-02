using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementApp.Models
{
    internal class Member
    {
        // Nullable uyarısını gidermek için başlangıçta null olabilir olarak işaretlendi
        private WorkoutPlan? workoutPlan;
        public bool IsOnline { get; set; } = false;
        public DateTime RegistrationDate { get; set; } = DateTime.Now;
        public int Id { get; set; }

        // C# 8.0 ve sonrası için string'lerin null uyarısı vermemesi için varsayılan boş değer atandı
        private string _fullName = string.Empty;
        public string FullName
        {
            get => _fullName;
            set
            {
                if (!string.IsNullOrWhiteSpace(value)) _fullName = value;
                else throw new ArgumentException("Name cannot be empty");
            }
        }

        public DateTime BirthDate { get; set; }

        public string MembershipType { get; set; } = string.Empty;

        public WorkoutPlan? WorkoutPlan
        {
            get { return workoutPlan; }
        }

        public int Age
        {
            get
            {
                int age = DateTime.Now.Year - BirthDate.Year;

                if (BirthDate.Date > DateTime.Now.AddYears(-age))
                    age--;

                return age;
            }
        }

        public Member(int id, string fullName, DateTime birthDate, string membershipType, WorkoutPlan? plan = null)
        {
            Id = id;
            FullName = fullName;
            BirthDate = birthDate;
            MembershipType = membershipType;
            workoutPlan = plan;
        }

        // Member.cs içindeki metodu bu şekilde güncelle
        public void AssignWorkoutPlan(WorkoutPlan plan)
        {
            this.workoutPlan = plan;
            // Eğer üyelik tipi "Frozen" ise plan atandığında otomatik aktif hale getirmek isteyebilirsin
            if (this.MembershipType == "Frozen")
                this.MembershipType = "Standard";
        }
        public int DaysRemaining
        {
            get
            {
                // Üyelik tipine göre gün sürelerini daha mantıklı seviyelere çekiyoruz
                int totalDays = 30;

                if (MembershipType == "1 Year" || MembershipType == "VIP")
                    totalDays = 365; // VIP ve 1 Yıllık olanlar 1 yıl
                else if (MembershipType == "6 Months" || MembershipType == "Premium")
                    totalDays = 180; // Premium ve 6 Aylık olanlar 6 ay
                else if (MembershipType == "3 Months")
                    totalDays = 90;
                else
                    totalDays = 30; // Standard üyeler 1 ay

                // Hesaplama kısmı aynı kalıyor
                var elapsed = (DateTime.Now - RegistrationDate).Days;
                int remaining = totalDays - elapsed;

                return remaining > 0 ? remaining : 0;
            }
        }

    }
}