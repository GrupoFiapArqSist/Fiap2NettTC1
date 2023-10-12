namespace TicketNow.Infra.CrossCutting.Notifications
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
    }
}
