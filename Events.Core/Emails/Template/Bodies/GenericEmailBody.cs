namespace Events.Core.Emails.Template.Bodies
{
    public class GenericEmailBody : IEmailBody
    {
        public string Text { get; init; }

        public GenericEmailBody(string text)
        {
            Text = text;
        }
    }
}
