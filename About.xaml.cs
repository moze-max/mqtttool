namespace mqtttool;

public partial class About : ContentPage
{

    public string intriducemessage = "���Ӧ�����ڽ���MQTT����������Ϣ������Ҫ��������������ҳ��������������Ҫ����ַ���û��������롢֤����Կ���������Ҫ�Ļ���";
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