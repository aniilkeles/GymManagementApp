using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using GymManagementApp.Models;
using GymManagementApp.ViewModels;


namespace GymManagementApp
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<MemberViewModel> memberViewModels = new ObservableCollection<MemberViewModel>();
        private ObservableCollection<WorkoutPlan> workoutPlans = new ObservableCollection<WorkoutPlan>();
        private ObservableCollection<Trainer> trainers = new ObservableCollection<Trainer>();
        private int memberIdCounter = 1;
        private MemberViewModel? _lastSelectedMember;

        public MainWindow()
        {
            InitializeComponent();

            // Veri kaynaklarını UI elementlerine bağlıyoruz
            MembersListBox.ItemsSource = memberViewModels;
            WorkoutPlanComboBox.ItemsSource = workoutPlans;
            TrainerComboBox.ItemsSource = trainers;

            LoadInitialData();
            UpdateOccupancy();
        }
        private void UpdateStatusLog(string message, bool isWarning = false)
        {
            if (StatusLogText != null)
            {
                StatusLogText.Text = $"[{DateTime.Now:HH:mm:ss}] {message}";
                // Uyarı varsa şeridin rengini kırmızı, yoksa koyu lacivert yap
                var parentBorder = StatusLogText.Parent as StackPanel;
                var mainBorder = parentBorder?.Parent as Border;
                if (mainBorder != null)
                {
                    mainBorder.Background = isWarning ?
                        new SolidColorBrush(Color.FromRgb(192, 57, 43)) : // Kırmızı
                        new SolidColorBrush(Color.FromRgb(44, 62, 80));   // Lacivert
                }
            }
        }
        private void LoadInitialData()
        {
            // --- GÜNCEL HOCA LİSTESİ ---
            trainers.Add(new Trainer(1, "Ahmet Erguven", "Bodybuilding Expert", "Muscle Gain"));
            trainers.Add(new Trainer(2, "Mehmet Demir", "Crossfit Specialist", "Crossfit Prep"));
            trainers.Add(new Trainer(3, "Elif Yilmaz", "Yoga & Pilates Coach", "Yoga Relax"));
            trainers.Add(new Trainer(4, "Selim Arslan", "Cardio & HIIT Trainer", "Fat Burn HIIT"));
            trainers.Add(new Trainer(5, "Canan Tekin", "Zumba & Dance", "Zumba Party"));
            trainers.Add(new Trainer(6, "Murat Kaya", "Powerlifting Coach", "Strength Phase"));
            // --- ANTRENMAN PLANLARI ---
            workoutPlans.Add(new WorkoutPlan(1, "Muscle Gain", trainers[0], 5));
            workoutPlans.Add(new WorkoutPlan(2, "Crossfit Prep", trainers[1], 6));
            workoutPlans.Add(new WorkoutPlan(3, "Yoga Relax", trainers[2], 2));
            workoutPlans.Add(new WorkoutPlan(4, "Fat Burn HIIT", trainers[3], 4));
            workoutPlans.Add(new WorkoutPlan(5, "Zumba Party", trainers[4], 3));
            workoutPlans.Add(new WorkoutPlan(6, "Strength Phase", trainers[5], 4));

            // --- 3 TANE DEFAULT ÜYE (BAŞLANGIÇ VERİSİ) ---
            // 1. Üye: VIP
            var m1 = new Member(memberIdCounter++, "Anil Hanlaroglu", DateTime.Now.AddYears(-25), "VIP");
            m1.AssignWorkoutPlan(workoutPlans[0]); // Hoca: Ahmet Erguven
            memberViewModels.Add(new MemberViewModel(m1));

            // 2. Üye: Premium
            var m2 = new Member(memberIdCounter++, "Mert Canak", DateTime.Now.AddYears(-22), "Premium");
            m2.AssignWorkoutPlan(workoutPlans[1]); // Hoca: Mehmet Demir
            memberViewModels.Add(new MemberViewModel(m2));

            // 3. Üye: Standard
            var m3 = new Member(memberIdCounter++, "Tugce Cebis", DateTime.Now.AddYears(-28), "Standard");
            m3.AssignWorkoutPlan(workoutPlans[2]); // Hoca: Elif Yilmaz
            memberViewModels.Add(new MemberViewModel(m3));
        }

        private void AddMember_Click(object sender, RoutedEventArgs e)
        {
            UpdateStatusLog($"{NameTextBox.Text} sisteme başarıyla kayıt edildi ve giriş yaptı.");
            if (string.IsNullOrWhiteSpace(NameTextBox.Text)) return;

            string type = (MembershipComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

            // Yeni üye oluşturuluyor
            var newMember = new Member(memberIdCounter++, NameTextBox.Text, BirthDatePicker.SelectedDate ?? DateTime.Now.AddYears(-20), type);

            // --- HOCA İSMİNİN GÖZÜKMESİ İÇİN DÜZELTME ---
            // Eğer "Primary Trainer" ComboBox'ından bir hoca seçilmişse, onu üyeye atıyoruz.
            if (TrainerComboBox.SelectedItem is Trainer selectedTrainer)
            {
                // Boş bir plan oluşturup seçili hocayı içine koyuyoruz ki kartta hoca ismi gözüksün.
                var autoPlan = new WorkoutPlan(0, "Personal Training", selectedTrainer, 0);
                newMember.AssignWorkoutPlan(autoPlan);
            }

            memberViewModels.Add(new MemberViewModel(newMember));
            UpdateOccupancy();
            // Formu temizle
            NameTextBox.Clear();
            BirthDatePicker.SelectedDate = null;
            TrainerComboBox.SelectedIndex = -1;
        }

        // ... Diğer metodlar (Update, Delete, Freeze vb.) aynı kalacak ...
        private void UpdateMember_Click(object sender, RoutedEventArgs e)
        {
            if (MembersListBox.SelectedItem is MemberViewModel vm)
            {
                vm.Member.FullName = NameTextBox.Text;
                vm.Member.MembershipType = (MembershipComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
                // --- PLAN GÜNCELLEME DÜZELTMESİ ---
                // Eğer Quick Assign Plan ComboBox'ından bir plan seçilmişse onu ata
                // 2. DOĞUM TARİHİ GÜNCELLEME (Yaşın değişmesini sağlayan kısım)
                if (BirthDatePicker.SelectedDate.HasValue)
                {
                    vm.Member.BirthDate = BirthDatePicker.SelectedDate.Value;
                }
                if (WorkoutPlanComboBox.SelectedItem is WorkoutPlan selectedPlan)
                {
                    vm.Member.AssignWorkoutPlan(selectedPlan);
                }

                // Güncelleme sırasında hoca değişmişse yansıt
                else if (TrainerComboBox.SelectedItem is Trainer selectedTrainer)
                {
                    var personalPlan = new WorkoutPlan(0, "Personal Training", selectedTrainer, 0);
                    vm.Member.AssignWorkoutPlan(personalPlan);
                }
                vm.Refresh();
                MembersListBox.Items.Refresh();
                MessageBox.Show("Member updated successfully!");
            }
        }

        private void DeleteMember_Click(object sender, RoutedEventArgs e)
        {
            // Listeden bir üyenin seçili olup olmadığını kontrol ediyoruz
            if (MembersListBox.SelectedItem is MemberViewModel vm)
            {
                // Silinecek üyenin ismini log için saklıyoruz
                string deletedMemberName = vm.Member.FullName;

                // Üyeyi ana listeden siliyoruz
                memberViewModels.Remove(vm);

                // --- YENİ EKLENEN KISIMLAR ---

                // 1. Sol taraftaki doluluk oranını (Gym Occupancy Rate) yeniden hesapla
                // Bu sayede çubuk ve rakam anında azalacaktır.
                UpdateOccupancy();

                // 2. Alt taraftaki log şeridinde silme işlemini saatle birlikte göster
                UpdateStatusLog($"{deletedMemberName} has been removed from the system.");

                // 3. Eğer silinen üye 'lastSelectedMember' ise referansı temizle
                if (_lastSelectedMember == vm)
                {
                    _lastSelectedMember = null;
                }
            }
            else
            {
                // Eğer kimse seçilmeden butona basılırsa kullanıcıyı uyarabilirsin
                UpdateStatusLog("Please select a member to remove.", true);
            }
        }
        private void FreezeMember_Click(object sender, RoutedEventArgs e)
        {
            if (MembersListBox.SelectedItem is MemberViewModel vm)
            {
                // Durum kontrolü yapıyoruz
                if (vm.Member.MembershipType == "Frozen")
                {
                    // Eğer zaten dondurulmuşsa, Standard üyeliğe geri döndür
                    vm.Member.MembershipType = "Standard";

                    // Log şeridini güncelle
                    UpdateStatusLog($"{vm.Member.FullName} üyeliği tekrar aktif edildi.");
                }
                else
                {
                    // Üyeliği dondur
                    vm.Member.MembershipType = "Frozen";

                    // KRİTİK: Üyelik dondurulunca online durumunu kapatıyoruz
                    vm.Member.IsOnline = false;

                    // Log şeridini güncelle
                    UpdateStatusLog($"{vm.Member.FullName} üyeliği donduruldu. Giriş izni kapatıldı.");
                }

                // Değişikliklerin kart üzerinde (renk, yazı, ışık) anında görünmesi için yenile
                vm.Refresh();

                // Listenin genel görünümünü tazele
                MembersListBox.Items.Refresh();
            }
        }
        // WorkoutPlanComboBox'ın seçimi değiştiğinde çalışacak bir olay (Event) ekleyelim
        private void WorkoutPlanComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (WorkoutPlanComboBox.SelectedItem is WorkoutPlan selectedPlan)
            {
                // Alttaki hoca listesinde, planın hocasını bul ve seç
                TrainerComboBox.SelectedItem = selectedPlan.Trainer;
            }
        }
        private void AssignPlan_Click(object sender, RoutedEventArgs e)
        {
            // Seçili bir üye ve seçili bir plan var mı kontrol et
            if (MembersListBox.SelectedItem is MemberViewModel selectedVM && WorkoutPlanComboBox.SelectedItem is WorkoutPlan selectedPlan)
            {
                // 1. Planı üyeye ata
                selectedVM.Member.AssignWorkoutPlan(selectedPlan);

                // 2. ViewModel içindeki OnPropertyChanged tetikleyicilerini çalıştır
                selectedVM.Refresh();

                // 3. KRİTİK: ListBox'ın görsellerini tamamen yenile
                // Bu satır olmazsa kartın rengi veya hoca ismi bazen eski haliyle kalır.
                MembersListBox.Items.Refresh();

                // Kullanıcıya bilgi ver (İsteğe bağlı)
                // MessageBox.Show("Plan başarıyla atandı!"); 
            }
            else
            {
                MessageBox.Show("Lütfen önce sağdan bir üye, sonra soldan bir plan seçiniz!");
            }
        }
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filter = SearchTextBox.Text.ToLower();

            // Hem üye isminde hem de o üyenin hocasının isminde arama yapar
            var filteredList = memberViewModels.Where(v =>
                v.Member.FullName.ToLower().Contains(filter) ||
                (v.Member.WorkoutPlan?.Trainer?.FullName?.ToLower().Contains(filter) ?? false)
            ).ToList();

            MembersListBox.ItemsSource = filteredList;

            // Alt şeritte (Toros Gym Log) kaç sonuç bulunduğunu göster
            if (!string.IsNullOrEmpty(filter))
            {
                UpdateStatusLog($"Search results for '{filter}': {filteredList.Count} found.");
            }
            else
            {
                UpdateStatusLog("Toros Gym Management System Active...");
            }
        }
        private void MembersListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateOccupancy();
            if (MembersListBox.SelectedItem is MemberViewModel vm)
            {
                // --- DURUM KONTROLÜ VE LOG GÜNCELLEME ---
                if (vm.Member.MembershipType == "Frozen")
                {
                    vm.Member.IsOnline = false; // Dondurulmuş üye online olamaz
                    UpdateStatusLog($"{vm.Member.FullName} üyeliği şu an DONDURULMUŞ durumda.");
                }
                else
                {
                    vm.Member.IsOnline = true; // Aktif üyeyi giriş yapmış göster
                    UpdateStatusLog($"{vm.Member.FullName} şu an aktif ve antrenmanda.");
                }
                vm.Member.IsOnline = true; // Seçilen kişiyi online yap (Simülasyon)
                UpdateStatusLog($"{vm.Member.FullName} şu an aktif.");
                vm.Refresh();
                NameTextBox.Text = vm.Member.FullName;
                BirthDatePicker.SelectedDate = vm.Member.BirthDate;
                foreach (ComboBoxItem item in MembershipComboBox.Items)
                {
                    if (item.Content.ToString() == vm.Member.MembershipType)
                    {
                        MembershipComboBox.SelectedItem = item;
                        break;
                    }
                }
            }
        }
        private void UpdateOccupancy()
        {
            // Online olan üyeleri say
            int onlineCount = memberViewModels.Count(v => v.Member.IsOnline);

            // ProgressBar ve Yazıyı güncelle
            OccupancyBar.Value = onlineCount;
            CapacityText.Text = $"{onlineCount}/50";

            // Renk uyarısı: Salon %80 dolarsa çubuğu turuncu yap
            if (onlineCount >= 40)
                OccupancyBar.Foreground = new SolidColorBrush(Color.FromRgb(230, 126, 34)); // Turuncu
            else
                OccupancyBar.Foreground = new SolidColorBrush(Color.FromRgb(46, 204, 113)); // Yeşil
        }
    }
}