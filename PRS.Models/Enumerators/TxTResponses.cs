using System;
using System.Collections.Generic;
using System.Text;

namespace PRS.Models.Enumerators
{
    public enum TxTResponse
    {
        validId,
        validEmail,
        Registered,
        Unregistered,
        RegisterOk,
        RegisteredEmail,
        UnregisteredContact,
        UnregisteredUser,
        DeleteOk,
        Failure_Delete,
        Failure_GetIndicator
    }

    public static class TxTResponses
    {
        public static string GetTxTResponse(this TxTResponse res)
        {
            switch (res)
            {
                case TxTResponse.validId:
                    return "Informe um ID válido!";

                case TxTResponse.validEmail:
                    return "Informe um e-mail válido!";

                case TxTResponse.Registered:
                    return "O cadastro já consta na base de dados!";

                case TxTResponse.Unregistered:
                    return "Não cadastrado!";

                case TxTResponse.RegisterOk:
                    return "Cadastro realizado com sucesso!";

                case TxTResponse.RegisteredEmail:
                    return "E-mail já cadastrado!";

                case TxTResponse.UnregisteredContact:
                    return "Contato não cadastrado!";

                case TxTResponse.UnregisteredUser:
                    return "Usuário não cadastrado!";

                case TxTResponse.DeleteOk:
                    return "Exclusão realizada com sucesso!";

                case TxTResponse.Failure_Delete:
                    return "Falha na exclusão!";

                case TxTResponse.Failure_GetIndicator:
                    return "Falha na obtenção do indicador desejado!";

                default:
                    return "";
            }
        }
    }
}
