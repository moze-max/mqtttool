namespace mqtttool;

public partial class About : ContentPage
{

    public string intriducemessage = "这个应用用于接受MQTT传输来的信息，你需要在这个程序的数据页面中输入你所需要的网址、用户名、密码、证书密钥（如果你需要的话）";
    public About()
    {
        InitializeComponent();
        intriduce.Text = intriducemessage;
    }
    private async void Back(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//MainPage");
        //await Navigation.PopModalAsync();
    }
}