using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Web.UI.HtmlControls;
using System.Net.Mail;

/// <summary>
/// Summary description for EmailBLLC
/// </summary>
public class EmailBLLC
{
	public EmailBLLC()
	{
	
	}

    public static void SendMailMessage(string from,string fromdisplayname,string recepient,string bcc,string cc, string subject,string body)
    {
        string patternLenient = @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
        Regex reLenient = new Regex(patternLenient);

        bool isLenientMatch = reLenient.IsMatch(recepient);

         // Instantiate a new instance of MailMessage
        MailMessage mMailMessage = new MailMessage();
        // Set the recepient address of the mail message
        mMailMessage.From =new MailAddress(from,fromdisplayname);
        // Set the recepient address of the mail message
        if (isLenientMatch)
        {
            mMailMessage.To.Add(new MailAddress(recepient));
            if (bcc != null)
            {
                isLenientMatch = reLenient.IsMatch(bcc);
                if(isLenientMatch)
                    mMailMessage.To.Add(new MailAddress(bcc));
            }
            if (cc != null)
            {
                isLenientMatch = reLenient.IsMatch(cc);
                if (isLenientMatch)
                    mMailMessage.To.Add(new MailAddress(cc));
            }

            mMailMessage.Subject = subject;

            mMailMessage.Body = body;

            mMailMessage.IsBodyHtml = true;

            mMailMessage.Priority = MailPriority.Normal;

            SmtpClient mSmtpClient = new SmtpClient();

            mSmtpClient.Send(mMailMessage);
        }

    }

    public static void SendMailMessage(string from,string recepient,string bcc,string cc, string subject,string body)
    {
        string patternLenient = @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
        Regex reLenient = new Regex(patternLenient);

        bool isLenientMatch = reLenient.IsMatch(recepient);

         // Instantiate a new instance of MailMessage
        MailMessage mMailMessage = new MailMessage();
        if (isLenientMatch)
        {
            // Set the recepient address of the mail message
            mMailMessage.From = new MailAddress(from);
            // Set the recepient address of the mail message
            mMailMessage.To.Add(new MailAddress(recepient));
            if (bcc != null)
            {
                isLenientMatch = reLenient.IsMatch(bcc);
                if (isLenientMatch)
                    mMailMessage.To.Add(new MailAddress(bcc));
            }
            if (cc != null)
            {
                isLenientMatch = reLenient.IsMatch(cc);
                if (isLenientMatch)
                    mMailMessage.To.Add(new MailAddress(cc));
            }

            mMailMessage.Subject = subject;

            mMailMessage.Body = body;

            mMailMessage.IsBodyHtml = true;

            mMailMessage.Priority = MailPriority.Normal;

            SmtpClient mSmtpClient = new SmtpClient();

            mSmtpClient.Send(mMailMessage);
        }

    }

   
}
