using FilmApp.Core;
using System;


namespace FilmApp.MVVM.ViewModel
{
    class MainViewModel: ObservableObject
    {
        public DashboardViewModel Dashboard { get; set; }
        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set {
                _currentView = value;
                OnPropertyChanged();
            }
        }
        public MainViewModel()
        {
            Dashboard = new DashboardViewModel();
            CurrentView = Dashboard;
        }
    }
}
