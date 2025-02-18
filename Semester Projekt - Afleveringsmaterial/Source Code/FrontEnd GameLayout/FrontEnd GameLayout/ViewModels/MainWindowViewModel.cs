﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.ComponentModel;
using FrontEnd_GameLayout.Helper_classes;
using GameEngine.Implementations;
using GameEngine.Interfaces;
using System.Windows.Media;
using System.Windows;
using System.Windows.Input;
using FrontEnd_GameLayout.Views;

namespace FrontEnd_GameLayout.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        ViewInfo Res = ViewInfo.Instance;
        IGameController game = GameController.Instance;
        private IPageViewModel _currentPageViewModel;
        private List<IPageViewModel> _pageViewModels;


        static int window_Width;
        public int Window_Width
        {
            get { return window_Width; }
            set
            {
                window_Width = value;
                OnPropertyChanged("Window_Width");
            }
        }

        static int window_Height = 1080;
        public int Window_Height
        {
            get { return window_Height; }
            set
            {
                window_Height = value;
                OnPropertyChanged("Window_Height");
            }
        }

        public List<IPageViewModel> PageViewModels
        {
            get
            {
                if (_pageViewModels == null)
                    _pageViewModels = new List<IPageViewModel>();

                return _pageViewModels;
            }
        }

        public IPageViewModel CurrentPageViewModel
        {
            get
            {
                return _currentPageViewModel;
            }
            set
            {
                _currentPageViewModel = value;
                OnPropertyChanged("CurrentPageViewModel");
            }
        }

        private void ChangeViewModel(IPageViewModel viewModel)
        {
            if (!PageViewModels.Contains(viewModel))
                PageViewModels.Add(viewModel);

            CurrentPageViewModel = PageViewModels
                .FirstOrDefault(vm => vm == viewModel);
        }

        private void OnGoToMainMenu(object obj)
        {

            Res.MusicUri = new Uri(String.Format("{0}\\Music\\Music.mp3", AppDomain.CurrentDomain.BaseDirectory));
            Res.Toggle_Music();
            ChangeViewModel(PageViewModels[0]);
        }

        private void OnGameStart(object obj)
        {
            Res.MusicUri = new Uri(String.Format("{0}\\Music\\Room.mp3", AppDomain.CurrentDomain.BaseDirectory));
            Res.Toggle_Music();
            ChangeViewModel(PageViewModels[1]);
        }

        private void OnGoToLoadMenu(object obj)
        {
            ChangeViewModel(PageViewModels[2]);
        }

        private void OnGoToSaveMenu(object obj)
        {
            ChangeViewModel(PageViewModels[3]);
        }

        private void OnGoToInGameMenu(object obj)
        {
            Res.MusicUri = new Uri(String.Format("{0}\\Music\\Music.mp3", AppDomain.CurrentDomain.BaseDirectory));
            Res.Toggle_Music();
            ChangeViewModel(PageViewModels[4]);
        }
        private void OnGoToSettingsMenu(object obj)
        {
            ChangeViewModel(PageViewModels[5]);
        }

        private void OnGoToRegisterMenu(object obj)
        {
            ChangeViewModel(PageViewModels[6]);
        }

        private void OnGoToLoginMenu(object obj)
        {
            ChangeViewModel(PageViewModels[7]);
        }
        private void OnGoToCombat(object obj)
        {
            Res.MusicUri = new Uri(String.Format("{0}\\Music\\Battle.mp3", AppDomain.CurrentDomain.BaseDirectory));
            Res.Toggle_Music();
            ChangeViewModel(PageViewModels[8]);
        }
        private void OnGoToDeathScreen(object obj)
        {
            Res.MusicUri = new Uri(String.Format("{0}\\Music\\Music.mp3", AppDomain.CurrentDomain.BaseDirectory));
            Res.Toggle_Music();
            ChangeViewModel(PageViewModels[9]);
        }
        private void OnGoToVictoryScreen(object obj)
        {
            Res.MusicUri = new Uri(String.Format("{0}\\Music\\Music.mp3", AppDomain.CurrentDomain.BaseDirectory));
            Res.Toggle_Music();
            ChangeViewModel(PageViewModels[10]);
        }
        private void OnGoToCharacterScreen(object obj)
        {
            ChangeViewModel(PageViewModels[11]);
        }
        private void OnGoToInventory(object obj)
        {
            ChangeViewModel(PageViewModels[12]);
        }

        public MainWindowViewModel()
        {
            Res.Toggle_Music();
            //Set Up the resolution of the views
            Window_Height = Res.Height;
            Window_Width = Res.Width;

            // Add available pages and set page
            PageViewModels.Add(new MainMenuViewModel());
            PageViewModels.Add(new RoomViewModel());
            PageViewModels.Add(new LoadMenuViewModel());
            PageViewModels.Add(new SaveMenuViewModel());
            PageViewModels.Add(new InGameMenuViewModel());
            PageViewModels.Add(new OptionsViewModel());
            PageViewModels.Add(new RegisterMenuViewModel());
            PageViewModels.Add(new LoginMenuViewModel());
            PageViewModels.Add(new CombatViewModel());
            PageViewModels.Add(new DeathScreenViewModel());
            PageViewModels.Add(new VictoryScreenViewModel());
            PageViewModels.Add(new CharacterScreenViewModel());
            PageViewModels.Add(new InventoryViewModel());
            CurrentPageViewModel = PageViewModels[7];

            Mediator.Subscribe("GoToMainMenu", OnGoToMainMenu);
            Mediator.Subscribe("GameStart", OnGameStart);
            Mediator.Subscribe("GoToLoadMenu", OnGoToLoadMenu);
            Mediator.Subscribe("GoToSaveMenu", OnGoToSaveMenu);
            Mediator.Subscribe("GoToInGameMenu", OnGoToInGameMenu);
            Mediator.Subscribe("GoToSettingsMenu", OnGoToSettingsMenu);
            Mediator.Subscribe("GoToRegisterMenu", OnGoToRegisterMenu);
            Mediator.Subscribe("GoToLoginMenu", OnGoToLoginMenu);
            Mediator.Subscribe("GoToCombat", OnGoToCombat);
            Mediator.Subscribe("GoToDeathScreen", OnGoToDeathScreen);
            Mediator.Subscribe("GoToVictoryScreen", OnGoToVictoryScreen);
            Mediator.Subscribe("GoToCharacterScreen", OnGoToCharacterScreen);
            Mediator.Subscribe("GoToInventory", OnGoToInventory);

            game.GetRoomDescriptionAsync();
        }
    }
}
