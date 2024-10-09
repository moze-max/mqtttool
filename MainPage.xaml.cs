using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Server;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace mqtttool
{
    public partial class MainPage : ContentPage
    {
        string broker = Preferences.Default.Get("borker", "na99bf48.ala.cn-hangzhou.emqxsl.cn");
        //string broker = "d90c1610.ala.cn-hangzhou.emqxsl.cn";
        int port = Preferences.Default.Get("port",8883);
        string topic = Preferences.Default.Get("topic", "mqtt/test");
        string username = Preferences.Default.Get("username", "iottest");
        string password = Preferences.Default.Get("password", "qwe12340");
        string ca_crt = Preferences.Default.Get("crt", @"-----BEGIN CERTIFICATE-----
MIIDrzCCApegAwIBAgIQCDvgVpBCRrGhdWrJWZHHSjANBgkqhkiG9w0BAQUFADBh
MQswCQYDVQQGEwJVUzEVMBMGA1UEChMMRGlnaUNlcnQgSW5jMRkwFwYDVQQLExB3
d3cuZGlnaWNlcnQuY29tMSAwHgYDVQQDExdEaWdpQ2VydCBHbG9iYWwgUm9vdCBD
QTAeFw0wNjExMTAwMDAwMDBaFw0zMTExMTAwMDAwMDBaMGExCzAJBgNVBAYTAlVT
MRUwEwYDVQQKEwxEaWdpQ2VydCBJbmMxGTAXBgNVBAsTEHd3dy5kaWdpY2VydC5j
b20xIDAeBgNVBAMTF0RpZ2lDZXJ0IEdsb2JhbCBSb290IENBMIIBIjANBgkqhkiG
9w0BAQEFAAOCAQ8AMIIBCgKCAQEA4jvhEXLeqKTTo1eqUKKPC3eQyaKl7hLOllsB
CSDMAZOnTjC3U/dDxGkAV53ijSLdhwZAAIEJzs4bg7/fzTtxRuLWZscFs3YnFo97
nh6Vfe63SKMI2tavegw5BmV/Sl0fvBf4q77uKNd0f3p4mVmFaG5cIzJLv07A6Fpt
43C/dxC//AH2hdmoRBBYMql1GNXRor5H4idq9Joz+EkIYIvUX7Q6hL+hqkpMfT7P
T19sdl6gSzeRntwi5m3OFBqOasv+zbMUZBfHWymeMr/y7vrTC0LUq7dBMtoM1O/4
gdW7jVg/tRvoSSiicNoxBN33shbyTApOB6jtSj1etX+jkMOvJwIDAQABo2MwYTAO
BgNVHQ8BAf8EBAMCAYYwDwYDVR0TAQH/BAUwAwEB/zAdBgNVHQ4EFgQUA95QNVbR
TLtm8KPiGxvDl7I90VUwHwYDVR0jBBgwFoAUA95QNVbRTLtm8KPiGxvDl7I90VUw
DQYJKoZIhvcNAQEFBQADggEBAMucN6pIExIK+t1EnE9SsPTfrgT1eXkIoyQY/Esr
hMAtudXH/vTBH1jLuG2cenTnmCmrEbXjcKChzUyImZOMkXDiqw8cvpOp/2PV5Adg
06O/nVsJ8dWO41P0jmP6P6fbtGbfYmbW0W5BjfIttep3Sp+dWOIrWcBAI+0tKIJF
PnlUkiaY4IBIqDfv8NZ5YBberOgOzW6sRBc4L0na4UU+Krk2U886UAb3LujEV0ls
YSEY1QSteDwsOoBrp+uvFRTp2InBuThs4pFsiv9kuXclVzDAGySj4dzp30d8tbQk
CAUw7C29C79Fv1C5qfPrmAESrciIxpg0X40KPMbp1ZWVbd4=
-----END CERTIFICATE-----");

        string clientId = Guid.NewGuid().ToString();

        public MainPage()
        {
            InitializeComponent();
            
            _=MQTTClientTest();
            
        }

        protected override void OnAppearing()
        {
            
            base.OnAppearing();
            // 在这里执行刷新数据的操作，比如重新绑定数据到视图
            GetData();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        private void GetData()
        {
            this.broker = Preferences.Default.Get("broker", "na99bf48.ala.cn-hangzhou.emqxsl.cn"); 
            this.port = Preferences.Default.Get("port", 8883);
            this.topic = Preferences.Default.Get("topic", "mqtt/test");
            this.username = Preferences.Default.Get("username", "iottest");
            this.password = Preferences.Default.Get("password", "qwe12340");
            this.ca_crt = Preferences.Default.Get("crt", @"-----BEGIN CERTIFICATE-----
MIIDrzCCApegAwIBAgIQCDvgVpBCRrGhdWrJWZHHSjANBgkqhkiG9w0BAQUFADBh
MQswCQYDVQQGEwJVUzEVMBMGA1UEChMMRGlnaUNlcnQgSW5jMRkwFwYDVQQLExB3
d3cuZGlnaWNlcnQuY29tMSAwHgYDVQQDExdEaWdpQ2VydCBHbG9iYWwgUm9vdCBD
QTAeFw0wNjExMTAwMDAwMDBaFw0zMTExMTAwMDAwMDBaMGExCzAJBgNVBAYTAlVT
MRUwEwYDVQQKEwxEaWdpQ2VydCBJbmMxGTAXBgNVBAsTEHd3dy5kaWdpY2VydC5j
b20xIDAeBgNVBAMTF0RpZ2lDZXJ0IEdsb2JhbCBSb290IENBMIIBIjANBgkqhkiG
9w0BAQEFAAOCAQ8AMIIBCgKCAQEA4jvhEXLeqKTTo1eqUKKPC3eQyaKl7hLOllsB
CSDMAZOnTjC3U/dDxGkAV53ijSLdhwZAAIEJzs4bg7/fzTtxRuLWZscFs3YnFo97
nh6Vfe63SKMI2tavegw5BmV/Sl0fvBf4q77uKNd0f3p4mVmFaG5cIzJLv07A6Fpt
43C/dxC//AH2hdmoRBBYMql1GNXRor5H4idq9Joz+EkIYIvUX7Q6hL+hqkpMfT7P
T19sdl6gSzeRntwi5m3OFBqOasv+zbMUZBfHWymeMr/y7vrTC0LUq7dBMtoM1O/4
gdW7jVg/tRvoSSiicNoxBN33shbyTApOB6jtSj1etX+jkMOvJwIDAQABo2MwYTAO
BgNVHQ8BAf8EBAMCAYYwDwYDVR0TAQH/BAUwAwEB/zAdBgNVHQ4EFgQUA95QNVbR
TLtm8KPiGxvDl7I90VUwHwYDVR0jBBgwFoAUA95QNVbRTLtm8KPiGxvDl7I90VUw
DQYJKoZIhvcNAQEFBQADggEBAMucN6pIExIK+t1EnE9SsPTfrgT1eXkIoyQY/Esr
hMAtudXH/vTBH1jLuG2cenTnmCmrEbXjcKChzUyImZOMkXDiqw8cvpOp/2PV5Adg
06O/nVsJ8dWO41P0jmP6P6fbtGbfYmbW0W5BjfIttep3Sp+dWOIrWcBAI+0tKIJF
PnlUkiaY4IBIqDfv8NZ5YBberOgOzW6sRBc4L0na4UU+Krk2U886UAb3LujEV0ls
YSEY1QSteDwsOoBrp+uvFRTp2InBuThs4pFsiv9kuXclVzDAGySj4dzp30d8tbQk
CAUw7C29C79Fv1C5qfPrmAESrciIxpg0X40KPMbp1ZWVbd4=
-----END CERTIFICATE-----");
            _= MQTTClientTest();
        }

        public async Task MQTTClientTest()
        {
            
            message.Text = "Topic = " + topic + "\n" +"uername = "+username + "\npassword = " + password + "\nport = " + port;
            MainThread.BeginInvokeOnMainThread(() =>
            {
                // 在这里更新UI，比如修改标签的文本  
                UserHello.Text = username + "   WELCOME YOU!";
            });
            
            var factory = new MqttFactory();
            var client = factory.CreateMqttClient();
            var options = new MqttClientOptionsBuilder()
                .WithTcpServer(broker, port)
                .WithCredentials(username, password)
                .WithClientId(clientId)
                .WithCleanSession()
                .WithTlsOptions(
                o =>
                {
                    o.WithCertificateValidationHandler(_ => true);
                    o.WithSslProtocols(SslProtocols.Tls12);
                    X509Certificate2Collection caChain = new X509Certificate2Collection();
                    caChain.ImportFromPem(ca_crt);
                    try
                    {
                    new MqttClientTlsOptionsBuilder()
                    .WithTrustChain(caChain)
                    .Build();
                    }
                    catch (Exception ex)
                    {

                        MainThread.BeginInvokeOnMainThread(() =>
                        {
                            connetmg.Text = ex.Message;
                        });
                    }
                }
                )
                .Build();
            var connectResult = await client.ConnectAsync(options);

            // 设置消息接收的事件处理程序  


            if (connectResult.ResultCode == MqttClientConnectResultCode.Success)
            {

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    connetmg.Text = "成功连接到服务器！";
                });
            }
            else
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    connetmg.Text = $"Failed to connect to MQTT broker: {connectResult.ResultCode}";
                });

            }

            await client.SubscribeAsync(topic);



            // Callback function when a message is received
            client.ApplicationMessageReceivedAsync += e =>
            {
                MainThread.BeginInvokeOnMainThread(() => { ShowData.Text =($"{Encoding.UTF8.GetString(e.ApplicationMessage.PayloadSegment)}"); });
                return Task.CompletedTask;
            };

        }

        private async void GoToData(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//Data");
            //await Navigation.PushModalAsync(new Data());
        }
        private async void GotoAbout(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//About");
            //await Navigation.PushModalAsync(new About());
        }
        private async void GotoTCP(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//TCP");
            //await Navigation.PushModalAsync(new About());
        }

    }

}
