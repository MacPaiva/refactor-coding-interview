using LegacyApp.Service;

namespace LegacyApp.Service;

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
public partial class UserCreditServiceClient : System.ServiceModel.ClientBase<IUserCreditService>, IUserCreditService
{

    public UserCreditServiceClient()
    {
    }

    public UserCreditServiceClient(string endpointConfigurationName) :
        base(endpointConfigurationName)
    {
    }

    public UserCreditServiceClient(string endpointConfigurationName, string remoteAddress) :
        base(endpointConfigurationName, remoteAddress)
    {
    }

    public UserCreditServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
        base(endpointConfigurationName, remoteAddress)
    {
    }

    public UserCreditServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
        base(binding, remoteAddress)
    {
    }

    public int GetCreditLimit(string firstname, string surname, System.DateTime dateOfBirth)
    {
        return base.Channel.GetCreditLimit(firstname, surname, dateOfBirth);
    }
}
