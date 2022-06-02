﻿using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganiserApp.ViewModels
{
    public class ShopTicketsGroupViewModel: ObservableCollection<ShopTicketsViewModel>
    {
        public string Name { get; set; }

        public ShopTicketsGroupViewModel(string name, IEnumerable<ShopTicketsViewModel> items)
            : base(items)
        {
            Name = name;
        }
    }
    public class ShopTicketsViewModel : ObservableObject
    {
        public string Category { get; set; }
        public string Title { get; set; }

        private bool _isBeingDragged;
        public bool IsBeingDragged
        {
            get { return _isBeingDragged; }
            set { SetProperty(ref _isBeingDragged, value); }
        }

        private bool _isBeingDraggedOver;
        public bool IsBeingDraggedOver
        {
            get { return _isBeingDraggedOver; }
            set { SetProperty(ref _isBeingDraggedOver, value); }
        }
    }
}