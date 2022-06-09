using Telegram.Bot;

var token = "TOKEN";

TelegramBotClient botClient = new TelegramBotClient(token);

botClient.StartReceiving();
botClient.OnMessage += DoOnMessage;

Console.ReadLine();

async void DoOnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
{
    var inputMessage = e.Message.Text;

    if (e.Message.Type == Telegram.Bot.Types.Enums.MessageType.Text)
        await botClient.SendTextMessageAsync(e.Message.Chat.Id, Bionizer(inputMessage), Telegram.Bot.Types.Enums.ParseMode.Html);

    else
        await botClient.SendTextMessageAsync(e.Message.Chat.Id, "Something Went Wrong!");
}

string Bionizer(string input)
{
    var words = input.Split(' ').ToList();

    for (int i = 0; i < words.Count; i++)
    {
        if (!words[i].Any(char.IsDigit))
            words[i] = words[i].Insert(words[i].Length / 2, "</strong>").Insert(0, "<strong>");

        else
            words[i] = words[i].Insert(words[i].Length, "</u>").Insert(0, "<u>");
    }

    return string.Join(" ", words);
}