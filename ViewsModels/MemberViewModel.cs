using GymManagementApp.Models;
using System.Windows.Media;

namespace GymManagementApp.ViewModels
{
    internal class MemberViewModel : BaseViewModel
    {
        public Member Member { get; }

        // Hocanın örneğindeki gibi: Duruma göre renk döndürüyoruz
        public SolidColorBrush StatusColor
        {
            get
            {
                // Önce dondurulmuş kontrolü (En öncelikli durum)
                if (Member.MembershipType == "Frozen")
                    return new SolidColorBrush(Color.FromRgb(255, 204, 204)); // Soft Kırmızı

                // Plan yoksa gri
                if (Member.WorkoutPlan == null)
                    return new SolidColorBrush(Color.FromRgb(220, 220, 220)); // Gray

                // Üyelik tipine göre diğer renkler
                return Member.MembershipType switch
                {
                    "VIP" => new SolidColorBrush(Color.FromRgb(255, 215, 0)),    // Gold
                    "Premium" => new SolidColorBrush(Color.FromRgb(212, 237, 218)), // Soft Yeşil
                    _ => new SolidColorBrush(Color.FromRgb(173, 216, 230))   // LightBlue
                };
            }
        }
        public MemberViewModel(Member member)
        {
            Member = member;
        }

        // Bir plan atandığında rengin güncellenmesi için tetikleyici
        public void Refresh()
        {
            // Kartın renginin değişmesi için
            OnPropertyChanged(nameof(StatusColor));

            // Hoca ismi ve plan adının güncellenmesi için
            OnPropertyChanged(nameof(Member));


            // Eğer üyelik tipi de plana göre değişiyorsa
            OnPropertyChanged("");

        }
    }
}