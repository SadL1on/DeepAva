using System;
using System.Collections.Generic;
using System.Text;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model;
using VkNet.Model.RequestParams;
using VkNet.AudioBypassService.Extensions;
using Microsoft.Extensions.DependencyInjection;
using VkNet.Utils;
using System.Linq;

namespace KursAuth.Models
{
    public class AutorizationVk
    {

        //public MessageGetHistoryObject GetHistory(long userid)
        //{
        //    var getHistory = api.Messages.GetHistory(new MessagesGetHistoryParams
        //    {
        //        Count = 200,
        //        UserId = userid
        //    });
        //    return getHistory;
        //}

        //public void SendMessage(AutorizationVk vk, userid)
        //{

        //    api.Messages.Send(new MessagesSendParams
        //    {
        //        UserId = userid, //Id получателя
        //        Message = "Message", //Сообщение
        //        RandomId = new Random().Next(999999) //ужасный уникальный идентификатор
        //    });
        //}


    }

}