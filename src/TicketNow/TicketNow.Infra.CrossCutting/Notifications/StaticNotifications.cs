﻿namespace TicketNow.Infra.CrossCutting.Notifications
{
    public static class StaticNotifications
    {
        #region [Users]
        public static Notification InvalidCredentials = new Notification("InvalidCredentials", "Credenciais invalidas!");
        public static Notification UserAlreadyExists = new Notification("UserAlreadyExists", "Usuario já cadastrado!");
        public static Notification UserNotFound = new Notification("InvalidUser", "Usuario não encontrado!");
        public static Notification RevokeToken = new Notification("RevokeToken", "Token revogado com sucesso!");
        public static Notification InvalidToken = new Notification("InvalidToken", "Token invalido!");
        public static Notification UserCreated = new Notification("UserCreated", "Usuario criado com sucesso!");
        #endregion

        #region [Event]
        public static Notification EventAlreadyExists = new Notification("EventAlreadyExists", "Evento já cadastrado!");
        public static Notification EventUpdated = new Notification("EventUpdated", "Evento editado com sucesso!");
        public static Notification EventNotFound = new Notification("EventNotFound", "Evento não encontrado!");
        public static Notification EventCreated = new Notification("EventCreated", "Evento criado com sucesso!");
        public static Notification InvalidPromoter = new Notification("InvalidPromoter", "Id do promoter inválido");
        public static Notification EventAlreadyActiveOrInactive = new Notification("EventAlreadyActiveOrInactive", "Evento já está {0}");
        public static Notification EventState = new Notification("EventState", "Evento {0} com sucesso!");
        public static Notification EventDeleted = new Notification("EventDeleted", "Evento deletado com sucesso!");
        #endregion
    }
}
