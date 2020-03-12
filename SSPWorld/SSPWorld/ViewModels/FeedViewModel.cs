using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SSPWorld.Models;
using SSPWorld.Repositories;
using SSPWorld.Views;
using Xamarin.Forms;

namespace SSPWorld.ViewModels
{
    public class FeedViewModel : BaseViewModel
    {
        private readonly INavigation _navigation;

        private readonly IUpdateRepository _updatesRepository = new UpdatesRepository();

        private Update _selectedUpdate;

        public Update SelectedUpdate
        {
            get => _selectedUpdate;
            set
            {
                if (_selectedUpdate == value) return;

                _selectedUpdate = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Update> _updates = new ObservableCollection<Update>();

        public ObservableCollection<Update> Updates
        {
            get => _updates;
            set
            {
                if (_updates == value)
                    return;
                _updates = value;
                OnPropertyChanged();
            }
        }

        public ICommand ItemTappedCommand { get; private set; }

        public FeedViewModel(INavigation navigation)
        {
            _navigation = navigation;
            BindCommands();
            GetUpdates();
            CheckForChanges();
        }

        private void CheckForChanges()
        {
            MessagingCenter.Subscribe<CourseDetailsViewModel, string>
            (this, "EnrollmentsChanged",
             (obj, args) =>
            {
                GetUpdates();
            });

        }

        private void BindCommands()
        {
            ItemTappedCommand = new Command(ItemTapped);
        }

        private void GetUpdates()
        {
            _updates.Clear();
            var updates = new List<Update>();

            Task.Run(async () =>
            {
                updates = await _updatesRepository.GetEnrolledUpdates();
            }).Wait();
             
            if (updates == null) return;
            
            foreach (var update in updates)
            {
                _updates.Add(update);
            }
            
        }

        private async void ItemTapped()
        {
            await _navigation.PushModalAsync(new UpdateDetailsPage(SelectedUpdate.Id));
            SelectedUpdate = null;
        }
    }
}
