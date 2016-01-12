using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;
using System.Linq;

namespace SecondHandBusinessPlatform.Models
{
    [Table("User")]
    public class User
    {

        public enum UserStatus { Normal, Suspended }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string UserName { get; set; }
        [DataType(DataType.Text)]
        public string StudentId { get; set; }
        [EnumDataType(typeof(UserStatus))]
        public UserStatus Status { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string Tel { get; set; }
        [Required]      
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string QQ { get; set; }
        public string Gender { get; set; }
        [Range(0, 200)]
        public int Age { get; set; }
        [Range(0, 5)]
        public UInt32 Credit { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [StringLength(45, MinimumLength = 6)]
        public string Password { get; set; }

        public string EncryptedPassword
        {
            get
            {
                return Password;
            }
            set
            {
                string nonceStr = "";
                string method = "MD5";
                string alphabet = "qwertyuiopasdfghjklzxcbnmQWERTYUIOPASDFGHJKLZXCVBNM1234567890";
                Random r = new Random();
                for (int i = 0; i < 8; i++)
                {
                    nonceStr += alphabet[r.Next(0, alphabet.Length - 1)];
                }
                string encrptedString = FormsAuthentication.HashPasswordForStoringInConfigFile(value + nonceStr, method);
                Password = string.Format("{0}${1}${2}", method, nonceStr, encrptedString);
            }
        }

        public Boolean IsPasswordValid(string password)
        {
            string[] keys = this.Password.Split('$');
            string method = keys[0];
            string nonceStr = keys[1];
            string standard = keys[2];
            string encrptedString = FormsAuthentication.HashPasswordForStoringInConfigFile(password + nonceStr, method);
            return (encrptedString == standard);
        }
    }
    
}