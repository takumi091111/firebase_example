using System.Threading.Tasks;
using System.Windows;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;

namespace ChatApp
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public static ViewModel viewModel = new ViewModel();

        public static IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "secret",
            BasePath = "https://example.firebaseio.com/"
        };
        public static IFirebaseClient client = new FirebaseClient(config);

        public MainWindow()
        {
            InitializeComponent();
            DataContext = viewModel;

            // 非同期でメッセージを取得し続ける
            var t = GetMessageAsync();
        }

        private async void SendButton_Click(object sender, RoutedEventArgs e)
        {
            // メッセージをFireBaseへ送信する
            await PushMessageAsync(InputBox.Text);
            InputBox.Text = "";
        }

        private async Task PushMessageAsync(string text)
        {
            Message message = new Message(text);
            PushResponse response = await client.PushAsync("messages/push", message);
        }

        private async Task GetMessageAsync()
        {
            EventStreamResponse response = await client.OnAsync("messages", (sender, args, context) =>
            {
                // メッセージリストに取得したメッセージを追加する
                Message message = new Message(args.Data);
                viewModel.MessageList.Add(message);
            });
        }
    }
}
