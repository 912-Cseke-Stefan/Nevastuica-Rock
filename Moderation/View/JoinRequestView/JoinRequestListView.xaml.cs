using Moderation.Entities;
using Moderation.Model;
using Backend.Service;

namespace Moderation.JoinRequestView;

public partial class JoinRequestListView : ContentPage
{
    private IService service;
	private IEnumerable<JoinRequest> joinRequests;
	public JoinRequestListView(IService service, Group group)
	{
        this.service = service;
		this.joinRequests = service.GetJoinRequestsForGivenGroup(group);
		// InitializeComponeknt();
		CreateList();
	}
	private void CreateList()
	{
        var stackLayout = new StackLayout();

        foreach (var request in joinRequests)
        {
            var requestControl = new JoinRequestDisplay(service, request);
            stackLayout.Children.Add(requestControl);
        }
        var backButton = BackButton();
        stackLayout.Children.Add(backButton);

        Content = new ScrollView { Content = stackLayout };
    }
    private Button BackButton()
    {
        var button = new Button { Text = "Back", Margin = 4 };
        button.Clicked += (sender, e) => { Navigation.PopAsync(); };
        return button;
    }
}