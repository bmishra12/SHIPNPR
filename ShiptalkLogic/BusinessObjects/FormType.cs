using System.ComponentModel;
namespace ShiptalkLogic.BusinessObjects
{
    public enum FormType
    {
        [Description("Client Contact Form")]
        ClientContact = 1,
        [Description("Public Media Event Form")]
        PublicMediaActivity = 2
    }
}
