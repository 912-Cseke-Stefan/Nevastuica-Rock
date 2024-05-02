using Moderation.CurrentSessionNamespace;
using Moderation.Entities;
using Backend.Service;
namespace Moderation;

public partial class LoginPage : ContentPage
{
    private IService service;
    public LoginPage(IService service)
    {
        this.service = service;
        InitializeComponent();
    }

    private async void OnLoginClicked(object? sender, EventArgs e)
    {
        string username = usernameEntry.Text.Trim();
        string password = passwordEntry.Text.Trim();

        try
        {
            // CurrentApp.Authenticator.AuthMethod(username, password); <-- this got temporarily replaced by this:|
            //                                                                                                   v
            Guid userId = service.GetUserGuidByName(username)
                ?? throw new ArgumentException("No account with that username");
            User currentUser = service.GetUserByGuid(userId)
                ?? throw new ArgumentException("Could not find user");
            string pass = currentUser.Password;

            if (pass != password)
            {
                throw new ArgumentException("Incorrect password");
            }
            usernameEntry.Text = string.Empty;
            passwordEntry.Text = string.Empty;
            CurrentSession.GetInstance().LogIn(currentUser);
            await Navigation.PushAsync(new GroupsView(service));
        }
        catch (ArgumentException argEx)
        {
            await DisplayAlert("Error", argEx.Message, "OK");
        }
    }
    private void OnQuitClicked(object sender, EventArgs e) => Application.Current?.Quit();
}