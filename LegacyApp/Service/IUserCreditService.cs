namespace LegacyApp.Service;

[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.ServiceContractAttribute(ConfigurationName = "LegacyApp.IUserCreditService")]
public interface IUserCreditService
{
    [System.ServiceModel.OperationContractAttribute(Action = "http://totally-real-service.com/IUserCreditService/GetCreditLimit", ReplyAction = "http://totally-real-service.com/IUserCreditService/GetCreditLimitResponse")]
    int GetCreditLimit(string firstname, string surname, System.DateTime dateOfBirth);
}