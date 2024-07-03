using DevCoffeeManagerApp.ViewModels;
using System;

namespace DevCoffeeManagerApp.Store
{
    public class NavigationStore
    {
        public event Action CurrentViewModelChaged;

        private BaseViewModel _currentViewModel;
        public BaseViewModel CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnCurrentViewModelChanged();
            }
        }

        private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChaged?.Invoke();
        }
    }
}
