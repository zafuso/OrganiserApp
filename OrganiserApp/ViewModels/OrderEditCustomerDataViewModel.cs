using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OrganiserApp.Models;
using OrganiserApp.Services;
using OrganiserApp.Views.Event;
using OrganiserApp.Views.Orders;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganiserApp.ViewModels
{
    [QueryProperty("SelectedOrder", "SelectedOrder")]
    public partial class OrderEditCustomerDataViewModel : BaseViewModel
    {
        public ObservableCollection<Country> CountryList { get; set; } = new();
        public ObservableCollection<Gender> GenderList { get; set; } = new();

        private readonly CountryService countryService;
        private readonly CustomerDataService customerDataService;
        private readonly IConnectivity connectivity;

        [ObservableProperty]
        Order selectedOrder;

        [ObservableProperty]
        Country selectedCountry;

        [ObservableProperty]
        Gender selectedGender;

        [ObservableProperty]
        bool isValidForm;

        [ObservableProperty]
        bool isValidEmail = false;

        [ObservableProperty]
        bool isValidFirstName = false;

        [ObservableProperty]
        bool isValidLastName = false;

        [ObservableProperty]
        bool isValidMobile = false;

        [ObservableProperty]
        bool isValidStreet = false;

        [ObservableProperty]
        bool isValidHouseNumber = false;

        [ObservableProperty]
        bool isValidZipcode = false;

        [ObservableProperty]
        bool isValidCity = false;

        string EventUuid;

        public OrderEditCustomerDataViewModel(CountryService countryService, IConnectivity connectivity, CustomerDataService customerDataService)
        {
            this.countryService = countryService;
            this.connectivity = connectivity;
            this.customerDataService = customerDataService;
        }

        public async void Init()
        {
            Title = "Contact";
            EventUuid = Preferences.Get("EventUuid", null);

            if (EventUuid is null)
                await Shell.Current.GoToAsync($"//{nameof(TabBar)}/{nameof(EventOverviewPage)}");

            await GetCountriesAsync();
            SetCustomerDataProperties();
        }

        [ICommand]
        async Task UpdateCustomerDataAsync()
        {
            if (IsBusy)
                return;

            try
            {
                if (connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    await Shell.Current.DisplayAlert("No connectivity!",
                        $"Please check internet and try again.", "OK");
                    return;
                }

                IsBusy = true;

                SelectedOrder.CustomerData.CountryId = SelectedCountry.Id;
                SelectedOrder.CustomerData.Gender = SelectedGender.Id;

                SelectedOrder.CustomerData = await customerDataService.PutCustomerDataAsync(SelectedOrder.CustomerData);
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Unable to save customer data: {e}");
                await Shell.Current.DisplayAlert("Error!", e.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        void SetCustomerDataProperties()
        {
            GenderList.Add(new Gender
            {
                Id = "M",
                Name = "Male"
            });

            GenderList.Add(new Gender
            {
                Id = "F",
                Name = "Female"
            });

            GenderList.Add(new Gender
            {
                Id = null,
                Name = "Other"
            });

            SelectedGender = GenderList.Where(g => g.Id == SelectedOrder.CustomerData.Gender).FirstOrDefault();
            SelectedCountry = CountryList.Where(c => c.Id == SelectedOrder.CustomerData.CountryId).FirstOrDefault();
        }

        [ICommand]
        async Task GetCountriesAsync()
        {
            if (IsBusy)
                return;

            try
            {
                if (connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    await Shell.Current.DisplayAlert("No connectivity!",
                        $"Please check internet and try again.", "OK");
                    return;
                }

                IsBusy = true;
                var countryList = await countryService.GetCountriesAsync();

                foreach (var country in countryList)
                {
                    CountryList.Add(country);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Unable to load page: {e}");
                await Shell.Current.DisplayAlert("Error!", e.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [ICommand]
        async Task BackToOrderDetailsAsync()
        {
            if (SelectedOrder is null)
                return;

            await Shell.Current.GoToAsync($"//{nameof(TabBar)}/{nameof(OrderDetailsPage)}", true, new Dictionary<string, object>
            {
                {"SelectedOrder", SelectedOrder }
            });
        }

        public void ValidateForm()
        {
            IsValidForm = false;

            if (IsValidEmail && IsValidFirstName && IsValidLastName)
            {
                IsValidForm = true;
            }

            if (SelectedOrder.CustomerData.Mobile.Length > 0 && !IsValidMobile)
            {
                isValidForm = false;
            }

            if (SelectedOrder.CustomerData.AddressLine1.Length > 0 && !IsValidStreet)
            {
                isValidForm = false;
            }

            if (SelectedOrder.CustomerData.AddressLine1BuildingNumber.Length > 0 && !IsValidHouseNumber)
            {
                isValidForm = false;
            }

            if (SelectedOrder.CustomerData.Zipcode.Length > 0 && !IsValidZipcode)
            {
                isValidForm = false;
            }

            if (SelectedOrder.CustomerData.City.Length > 0 && !IsValidCity)
            {
                isValidForm = false;
            }
        }
    }
}