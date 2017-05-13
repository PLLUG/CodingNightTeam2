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
        TelegramBotClient tbc;
        ApiAi apiai;

        Dictionary<string, string> emotionFilesDB;
        Dictionary<string, string> emotionMsgsDB;

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

            emotionMsgsDB = new Dictionary<string, string>();
            emotionMsgsDB.Add("AngryMood", "This should calm you down");
            emotionMsgsDB.Add("BadMood", "Cheer up");
            emotionMsgsDB.Add("GoodMood", "Remember these happy times");
            emotionMsgsDB.Add("TrueNeutralMood", "Not much is going on, huh?");
            emotionMsgsDB.Add("LovestruckMood", "This will ignite your passion");
            emotionMsgsDB.Add("OnDrugsMood", "WOAH THATS DEEEEEEEEEEP DUDE");
        }

        private void Main_Load(object sender, EventArgs e)
        {
            var config = new AIConfiguration("90861bda9ce9466791b4b49321460248", SupportedLanguage.English);
            apiai = new ApiAi(config);
        }

        private void tokenButton_Click(object sender, EventArgs e)
        {
            tbc = new TelegramBotClient(tokenTextBox.Text);
            tbTimer.Start();

            tbc.OnMessage += Tbc_OnMessage;
            tbc.StartReceiving();
            messageBox.Text = "Bot started\r\n";
        }

        private void Tbc_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {

            var response = apiai.TextRequest(e.Message.Text);

            clientMsgLog = e.Message.Chat.FirstName + ": " + e.Message.Text + "\r\n";
            

            //if(response.Result.Action == "input.welcome")
            //{
                
            //}

            //else
            //{
                string moodIdx = GetMood(response);
                if (moodIdx != null)
                {
                    clientMsgLog += "Bot(mood message): " + moodIdx + "\r\n";
                    tbc.SendTextMessageAsync(e.Message.Chat.Id, emotionMsgsDB[moodIdx]);
                    SendAudioFile(emotionFilesDB[moodIdx], e.Message.Chat.Id, emotionFilesDB[moodIdx]);
                }
                else
                {
                    tbc.SendTextMessageAsync(e.Message.Chat.Id, response.Result.Fulfillment.Speech);
                    clientMsgLog += "Bot: " + response.Result.Fulfillment.Speech + "\r\n";
                }
            //}
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
            string mood = null;

            foreach(var param in response.Result.Parameters)
            {
                if(!String.IsNullOrEmpty(param.Value as string))
                {
                    mood = param.Key as string;
                    break;
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
        }

    }
}
