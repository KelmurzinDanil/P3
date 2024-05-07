﻿namespace DB_993.Classes
{
    public class Compilation
    {
        /// <summary>
        /// Класс, используемый для хранения информации о подборках пользователя.
        /// </summary>
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<Realty> Realtys { get; set; } = new();
        public int? UserId { get; set; }
        public User User { get; set; }
    }
}
