namespace mqtttool;

using System.Net;
using System.Net.Sockets;
public partial class TCP : ContentPage
{
	public TCP()
	{
		InitializeComponent();
	}

    public async void GetTcpConnect(string host, int port)
    {
        TcpClient tcpClient = new TcpClient();
        tcpClient.Connect(host, port);
        if (tcpClient.Connected)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                Textboard.Text = "Connected";
            });
        }
        else
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                Textboard.Text = "Failed to Connect";
            });
        }
    }

  

}