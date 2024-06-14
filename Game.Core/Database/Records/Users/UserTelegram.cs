﻿namespace Game.Core.Database.Records.Users
{
    public class UserTelegram
    { 
        public Guid UserId { get; set; }
        public string TelegramId { get; set; } = string.Empty;

        public string? Username { get; set; } = string.Empty;
        public string? FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public string? Phone { get; set; } = string.Empty;
        public string? Language { get; set; } = string.Empty;
    }
}
