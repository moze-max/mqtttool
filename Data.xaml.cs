using System.Diagnostics.Metrics;

namespace mqtttool;

public partial class Data : ContentPage
{
    public Data()
    {
        InitializeComponent();
    }
    public static class EntryData
    {
        public static string? Ebroker { get; set; } 
        public static string? Eport { get; set; }
        public static string? Etopic { get; set; }
        public static string? Eusername { get; set; }
        public static string? Epassword { get; set; }
        public static string? Crt { get; set; }
    }
    //public void Entry_broker_TextChanged(object sender, TextChangedEventArgs e)
    //{
    //    string oldText = e.OldTextValue;
    //    string newText = e.NewTextValue;
    //    EntryData.Ebroker = Entry_broker.Text;


    //}


    //private void Entry_port_TextChanged(object sender, TextChangedEventArgs e)
    //{
    //    string oldText = e.OldTextValue;
    //    string newText = e.NewTextValue;
    //    EntryData.Eport = Entry_port.Text;
    //}

    //private void Entry_topic_TextChanged(object sender, TextChangedEventArgs e)
    //{
    //    string oldText = e.OldTextValue;
    //    string newText = e.NewTextValue;
    //    EntryData.Etopic = Entry_topic.Text;
    //}

    //private void Entry_username_TextChanged(object sender, TextChangedEventArgs e)
    //{
    //    string oldText = e.OldTextValue;
    //    string newText = e.NewTextValue;
    //    EntryData.Eusername = Entry_username.Text;
    //}

    //private void Entry_password_TextChanged(object sender, TextChangedEventArgs e)
    //{
    //    string oldText = e.OldTextValue;
    //    string newText = e.NewTextValue;
    //    EntryData.Epassword = Entry_password.Text;
    //}

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        if (Data.EntryData.Ebroker != null)
            Preferences.Default.Set("broker", Data.EntryData.Ebroker);
        if (Data.EntryData.Eport != null)
            Preferences.Default.Set("port", Data.EntryData.Eport);
        if (Data.EntryData.Etopic != null)
            Preferences.Default.Set("topic", Data.EntryData.Etopic);
        if (Data.EntryData.Eusername != null)
            Preferences.Default.Set("username", Data.EntryData.Eusername);
        if (Data.EntryData.Epassword != null)
            Preferences.Default.Set("password", Data.EntryData.Epassword);
        if (Data.EntryData.Crt != null)
            Preferences.Default.Set("Crt", Data.EntryData.Crt);
    }


    private void Entry_broker_Completed(object sender, EventArgs e)
    {
        Data.EntryData.Ebroker = ((Entry)sender).Text;
        // Set a string value:
        Preferences.Default.Set("broker", Data.EntryData.Ebroker);

    }

    private void Entry_port_Completed(object sender, EventArgs e)
    {
        Data.EntryData.Eport = ((Entry)sender).Text;
        Preferences.Default.Set("port", Data.EntryData.Eport);
    }

    private void Entry_topic_Completed(object sender, EventArgs e)
    {
        Data.EntryData.Etopic = ((Entry)sender).Text;
        Preferences.Default.Set("topic", Data.EntryData.Etopic);
    }

    private void Entry_username_Completed(object sender, EventArgs e)
    {

        Data.EntryData.Eusername = ((Entry)sender).Text;
        Preferences.Default.Set("username", Data.EntryData.Eusername);
    }

    private void Entry_password_Completed(object sender, EventArgs e)
    {
        Data.EntryData.Epassword = ((Entry)sender).Text;
        Preferences.Default.Set("password", Data.EntryData.Epassword);
    }

    private void Entry_crt_Completed(object sender, EventArgs e)
    {
        Data.EntryData.Crt = ((Entry)sender).Text;
        Preferences.Default.Set("Crt", Data.EntryData.Crt);
    }
    private async void Back(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//MainPage");
        //await Navigation.PopModalAsync();
    }
}