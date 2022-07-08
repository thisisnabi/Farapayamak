﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farapayamak.Models.Responses
{
    public class SendMessageResponse : BaseResponse
    {
        public string Message => Value switch { 
            "0"  => SendMessageEnum.InvalidUsernameOrPassword.ToPersian(),
            "1" => SendMessageEnum.TheRequestWasMadeSuccessfully.ToPersian(),
            "2" => SendMessageEnum.NotEnoughCredit.ToPersian(),
            "3" => SendMessageEnum.DailySubmissionLimit.ToPersian(),
            "4" => SendMessageEnum.LimitationOnSendingVolume.ToPersian(),
            "5" => SendMessageEnum.TheSenderNumberIsNotValid.ToPersian(),
            "6" => SendMessageEnum.TheSystemIsBeingUpdated.ToPersian(),
            "7" => SendMessageEnum.TheTextContainsTheFilteredWord.ToPersian(),
            "11" => SendMessageEnum.FailedToSend.ToPersian(),
            _  => Value
        };

    }

    public enum SendMessageEnum 
    {
        [PersianTitle("نام کاربری یا رمز عبور اشتباه می باشد.")]
        InvalidUsernameOrPassword = 0,

        [PersianTitle("درخواست با موفقیت انجام شد.")]
        TheRequestWasMadeSuccessfully,

        [PersianTitle("اعتبار کافی نمی باشد.")]
        NotEnoughCredit,

        [PersianTitle("محدودیت در ارسال روزانه.")]
        DailySubmissionLimit,

        [PersianTitle("محدودیت در حجم ارسال.")]
        LimitationOnSendingVolume,

        [PersianTitle("شماره فرستنده معتبر نمی باشد.")]
        TheSenderNumberIsNotValid,
 
        [PersianTitle("سامانه در حال بروزرسانی می باشد.")]
        TheSystemIsBeingUpdated,

        [PersianTitle("متن حاوی کلمه فیلتر شده می باشد.")]
        TheTextContainsTheFilteredWord,

        [PersianTitle("ارسال ناموفق بود.")]
        FailedToSend = 11
    }

}
