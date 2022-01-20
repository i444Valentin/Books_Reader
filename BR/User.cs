using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace BooksReader.App
{
    class User
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Patronymic { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        [NotMapped]
        public int[] UserFavoritesBooks
        {
            get
            {
                return Array.ConvertAll(InternalData.Split(';'), int.Parse);
            }
            set
            {
                InternalData = string.Format("{0},{1}", value[0], value[1]);
            }
        }

        public string InternalData { get; set; }

    }
}
