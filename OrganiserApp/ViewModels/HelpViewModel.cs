using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganiserApp.ViewModels
{
    public partial class HelpViewModel : BaseViewModel
    {
        private readonly IEmail email;
        public HelpViewModel(IEmail email)
        {
            Title = "Need help?";
            this.email = email; 
        }

        [ICommand]
        async Task GoToHelpCenterAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                Uri uri = new(Config.HelpcenterUrl);
                await Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Unable to open browser: {e}");
                await Shell.Current.DisplayAlert("Error!", e.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [ICommand]
        async Task GoToVisitorFaqAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                Uri uri = new(Config.VisitorUrl);
                await Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Unable to open browser: {e}");
                await Shell.Current.DisplayAlert("Error!", e.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [ICommand]
        async Task EmailSupportAsync()
        {
            IsBusy = true;

            if (email.IsComposeSupported) 
            { 
                string subject = "I need help!";
                string body = "Dear Support, I really could use some help configuring my event!";
                string[] recipients = new[] { "support@cmtickets.com" };

                var message = new EmailMessage
                {
                    Subject = subject,
                    Body = body,
                    BodyFormat = EmailBodyFormat.PlainText,
                    To = new List<string>(recipients)
                };

                await email.ComposeAsync(message);
            } 
            else
            {
                await Shell.Current.DisplayAlert("We're Sorry!", "Unfortunately sending e-mail is not supported on your device.", "OK");
            }

            IsBusy = false;
        }

        [ICommand]
        async Task StartWhatsAppChatAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                Uri uri = new($"{Config.WhatsAppUrl}?phone={Config.WhatsAppPhoneNumber}&text=Hello%20CM.com%20Ticketing,%20I%20have%20a%20question%20about%20my%20order");
                await Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Unable to open browser: {e}");
                await Shell.Current.DisplayAlert("Error!", e.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
