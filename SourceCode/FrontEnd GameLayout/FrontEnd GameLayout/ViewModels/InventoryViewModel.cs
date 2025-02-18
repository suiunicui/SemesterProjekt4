﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using System.Windows.Input;
using FrontEnd_GameLayout.Helper_classes;
using Prism.Commands;
using GameEngine.Implementations;
using GameEngine.Abstract_Class;
using GameEngine.Interfaces;

namespace FrontEnd_GameLayout.ViewModels
{
    public class InventoryViewModel : BaseViewModel, IPageViewModel
    {
        ViewInfo Res = ViewInfo.Instance;
        IGameController game = GameController.Instance;

        public InventoryViewModel()
        {
            Items = game.CurrentPlayer.Inventory;
            if(game.CurrentPlayer.EquippedWeapon != null)
            {
                EquippedWeapon = game.CurrentPlayer.EquippedWeapon.ItemType;
            }
            else
            {
                EquippedWeapon = "";
            }
            if(game.CurrentPlayer.EquippedShield != null)
            {
                EquippedShield = game.CurrentPlayer.EquippedShield.ItemType;
            }
            else
            {
                EquippedShield = "";
            }
            
            Window_Width = Res.Width;
            Window_Height = Res.Height;
        }

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


        #region Properties

        private List<Item> _items = new List<Item>();

        public List<Item> Items {
            get { return _items; }
            set
            {
                if (value != _items)
                {
                    _items = value;
                    OnPropertyChanged("Items");
                }
            }
        }

        private Item _selectedItem;

        public Item SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (value != _selectedItem)
                {
                    _selectedItem = value;
                    OnPropertyChanged("SelectedItem");
                }
            }
        }

        private String _equippedWeapon;

        public String EquippedWeapon
        {
            get { return _equippedWeapon; }
            set
            {
                if (value != _equippedWeapon)
                {
                    _equippedWeapon = "Weapon: " + value;
                    OnPropertyChanged("EquippedWeapon");
                }
            }
        }

        private String _equippedShield;

        public String EquippedShield
        {
            get { return _equippedShield; }
            set
            {
                if (value != _equippedShield)
                {
                    _equippedShield = "Shield: " + value;
                    OnPropertyChanged("EquippedShield");
                }
            }
        }

        

        #endregion

        #region Commands

        private ICommand _backCommand;

        public ICommand BackCommand
        {
            get
            {
                return _backCommand ?? (_backCommand = new RelayCommand(x =>
                {
                    Mediator.Notify("GameStart", "");
                }));
            }
        }

        private ICommand _characterCommand;

        public ICommand CharacterCommand
        {
            get
            {
                return _characterCommand ?? (_characterCommand = new RelayCommand(x =>
                {
                    Mediator.Notify("GoToCharacterScreen", "");
                }));
            }
        }

        private DelegateCommand _equipCommand;
        
        public DelegateCommand EquipCommand => _equipCommand ?? (_equipCommand = new DelegateCommand(ExecuteEquipCommand));

        void ExecuteEquipCommand()
        {
            if(SelectedItem != null)
            {
                switch (SelectedItem)
                {
                    case Weapon weapon:
                        game.CurrentPlayer.EquippedWeapon = weapon;
                        EquippedWeapon = weapon.ItemType;
                        break;
                    case Shield shield:
                        game.CurrentPlayer.EquippedShield = shield;
                        EquippedShield = shield.ItemType;
                        break;
                }
            }
        }


        #endregion
    }
}
