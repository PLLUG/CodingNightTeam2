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
            emotionFilesDB.Add("AngryMood", @"../../samples/Angry.mp3");
            emotionFilesDB.Add("BadMood", @"../../samples/Bad.mp3");
            emotionFilesDB.Add("GoodMood", @"../../samples/Good.mp3");
            emotionFilesDB.Add("TrueNeutralMood", @"../../samples/TrueNeutralMood.mp3");
            emotionFilesDB.Add("LovestruckMood", @"../../samples/Lovestruck.mp3");
            emotionFilesDB.Add("OnDrugsMood", @"../../samples/OnDrugs.mp3");

            tbc = new TelegramBotClient("341492989:AAHJxIs7mf52lhqlilUuAlIdFhP1qt-iipA");

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

            string mood = emotionFilesDB[GetMood(response)];
            SendAudioFile(mood, e.Message.Chat.Id, mood);
        }

        private async void SendAudioFile(string url, long chatId, string title)
        {
            Telegram.Bot.Types.FileToSend audioFile = new Telegram.Bot.Types.FileToSend();

            using (var stream = System.IO.File.Open(url, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read))
            {
                audioFile.Content = stream;
                audioFile.Filename = url;
                Telegram.Bot.Types.Message message = await tbc.SendAudioAsync(chatId, audioFile, 10, "performer", title);
            }       

        }

        private string GetMood(AIResponse response)
        {
            string mood = "";

            foreach(var param in response.Result.Parameters)
            {
                if(!String.IsNullOrEmpty(param.Value as string))
                {
                    mood = param.Key as string;
                }
            }

            return mood;
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
