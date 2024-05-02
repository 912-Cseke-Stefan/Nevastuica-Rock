using Moderation.CurrentSessionNamespace;
using Moderation.Model;
using Backend.Service;

namespace Moderation;

public partial class GroupsView : ContentPage
{
    private Service service;

    public GroupsView(Service service)
    {
        this.service = service;
        Content = new StackLayout { HorizontalOptions = LayoutOptions.Fill };
        MakeKids();
    }

    private void MakeKids()
    {
        foreach (Group group in service.GetAllGroups())
        {
            ((StackLayout)Content).Children.Add(new View.SingleGroupView(group, CurrentSession.GetInstance().User));
        }
        Button backButton = new ()
        {
            Text = "Back",
            HorizontalOptions = LayoutOptions.Fill,
        };
        backButton.Clicked += (s, e) =>
        {
            CurrentSession.GetInstance().LogOut();
            Navigation.PopAsync();
        };
        ((StackLayout)Content).Children.Add(backButton);
    }
}