using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using CurrencyConverter.Bot.Disctionary;
using CurrencyConverter.Bot.Integration;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using Enum = CurrencyConverter.Bot.Models.Enumerations.Enum;

namespace CurrencyConverter.Bot.Controllers
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        private static readonly NLog.ILogger Logger = NLog.LogManager.GetLogger("CurrencyConverter");

        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            var connector = new ConnectorClient(new Uri(activity.ServiceUrl));

            Logger.Info($"User Post an activity, ChannelId : {activity.ChannelId}" +
                        $", Conversation {JsonConvert.SerializeObject(activity.Conversation)}" +
                        $", From {JsonConvert.SerializeObject(activity.From)}" +
                        $", Recipient {JsonConvert.SerializeObject(activity.Recipient)}");

            var currencyConverterBloomberg = new CurrecnyConverterBloomberg();
            //var currencyConverterFixerIo= new CurrencyConverterFixerIo();

            if (activity.Type == ActivityTypes.Message)
            {
                Logger.Debug($"User post a text, activity.Text : {activity.Text}");

                var currency = StringToCurrency.Dictionary.FirstOrDefault(x => activity.Text.ToLower().Contains(x.Key)).Value;
                var currencyResponse = currencyConverterBloomberg.FindRate(currency);
                //var currencyResponse = currencyConverterFixerIo.FindRate(currency);

                var reply = activity.CreateReply($"TL karşılığını görmek istediğiniz para birimini giriniz ? (💵) (💶)");

                if (currencyResponse.Status != Enum.Status.Success)
                {
                    Logger.Error($"Currency response not created succesfulyl, Detail : {JsonConvert.SerializeObject(currencyResponse)}");
                }
                else
                {
                    reply = activity.CreateReply($"1 {currency.Name} = {currencyResponse.Rate} TL");
                    Logger.Debug($"Bot answered, Detail : 1 {currency.Name} = {currencyResponse.Rate} TL");
                }

                await connector.Conversations.ReplyToActivityAsync(reply);

            }
            else
            {
                HandleSystemMessage(activity);
            }



            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        private Activity HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels
                //return message.CreateReply($"TL karşılığını görmek istediğiniz para birimini giriniz ? (💵) (💶)");

            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }

            return null;
        }



    }


}