using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telegram.Bot;
using ApiAiSDK;
using ApiAiSDK.Model;

namespace BotTest1
{
    public partial class Main : Form
    {
        string clientMsgLog = "";
        string botMsgLog = "";
        TelegramBotClient tbc;
        ApiAi apiai;

        Dictionary<string, string> emotionFilesDB;

        public Main()
        {

            InitializeComponent();

            emotionFilesDB = new Dictionary<string, string>();
            emotionFilesDB.Add("Angry", @"../../samples/Angry.mp3");
            emotionFilesDB.Add("Bad", @"../../samples/Bad.mp3");
            emotionFilesDB.Add("Good", @"../../samples/Good.mp3");
            emotionFilesDB.Add("Indifferent", @"../../samples/Indifferent.mp3");
            emotionFilesDB.Add("Lovestruck", @"../../samples/Lovestruck.mp3");
            emotionFilesDB.Add("OnDrugs", @"../../samples/OnDrugs.mp3");
            emotionFilesDB.Add("unit-temperature", @"../../samples/unit-temperature.mp3");

            tbc = new TelegramBotClient("334375345:AAFZmig5TDYRgGM586kFTqtd7CRG5IyEjDg");

            tbTimer.Start();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            var config = new AIConfiguration("90861bda9ce9466791b4b49321460248", SupportedLanguage.English);
            apiai = new ApiAi(config);

            tbc.OnMessage += Tbc_OnMessage;
            tbc.StartReceiving();
        }

        private void Tbc_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {

            var response = apiai.TextRequest(e.Message.Text);

            clientMsgLog = e.Message.Chat.FirstName + ": " + e.Message.Text + "\r\n";
            botMsgLog = "Bot: " + response.Result.Fulfillment.Speech + "\r\n";

            SendAudioFile(@"../../samples/2.mp3", e.Message.Chat.Id);
        }

        private async void SendAudioFile(string url, long chatId)
        {
            Telegram.Bot.Types.FileToSend audioFile = new Telegram.Bot.Types.FileToSend();

            using (var stream = System.IO.File.Open(url, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read))
            {
                audioFile.Content = stream;
                audioFile.Filename = url;
                Telegram.Bot.Types.Message message = await tbc.SendAudioAsync(chatId, audioFile, 10, "performer", "Title");
            }       

        }

        private void tbTimer_Tick(object sender, EventArgs e)
        {
            if (clientMsgLog.Length > 0)
            {
                messageBox.Text += clientMsgLog;
                clientMsgLog = "";
            }

            if (botMsgLog.Length > 0)
            {
                messageBox.Text += botMsgLog;
                botMsgLog = "";
            }
        }
    }
}
